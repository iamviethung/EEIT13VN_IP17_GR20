using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Ex4___Asynchronous_communication
{
    public partial class Form1 : Form
    {
        private string _url = "";
        private string _username = "";
        private string _password = "";
        private string[] _list = null; 

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void btGetDirectory_Click(object sender, EventArgs e)
        {
            try
            {
                _url = txtURL.Text;
                if (!_url.EndsWith("/"))
                {
                    _url = _url + "/";
                }
                _username = txtUser.Text;
                _password = txtPass.Text;

                FtpWebRequest request = (FtpWebRequest)WebRequest.Create(_url);
                request.Method = WebRequestMethods.Ftp.ListDirectory;
                if (!_username.Equals(""))
                {
                    request.Credentials = new NetworkCredential(_username, _password);
                }
                FtpWebResponse response = (FtpWebResponse)request.GetResponse();

                Stream responseStream = response.GetResponseStream();
                StreamReader reader = new StreamReader(responseStream);

                _list = reader.ReadToEnd().Split(new string[] { "\r\n" }, StringSplitOptions.None);
                listFile.DataSource = _list;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void listFile_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selected = listFile.GetItemText(listFile.SelectedValue);
            var urlFile = _url + selected;

            try
            {
                FtpWebRequest request = (FtpWebRequest)WebRequest.Create(urlFile);
                request.Method = WebRequestMethods.Ftp.GetFileSize;
                if (!_username.Equals(""))
                {
                    request.Credentials = new NetworkCredential(_username, _password);
                }
                FtpWebResponse response = (FtpWebResponse)request.GetResponse();

                long size = response.ContentLength;
                labelSize.Text = size + " B ";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Directory --> Cannot Process");
            }

        }

        private void btDownload_Click(object sender, EventArgs e)
        {
            try
            {
                backgroundWorker1.WorkerReportsProgress = true;
                backgroundWorker1_DoWork(sender, null);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {

            
            string selected = listFile.GetItemText(listFile.SelectedValue);
            var urlFile = _url + selected;

            saveFileDialog1.FileName = selected;
            saveFileDialog1.ShowDialog();
            var filePath = saveFileDialog1.FileName;

            FtpWebRequest request = (FtpWebRequest)WebRequest.Create(urlFile);
            request.UseBinary = true;
            request.Method = WebRequestMethods.Ftp.DownloadFile;
            if (!_username.Equals(""))
            {
                request.Credentials = new NetworkCredential(_username, _password);
            }

            FtpWebResponse response = (FtpWebResponse)request.GetResponse();
            Stream responseStream = response.GetResponseStream();

            if (filePath == null)
            {
                filePath = selected;
            }

            FileStream writer = new FileStream(filePath, FileMode.Create);

            long length = response.ContentLength;
            int bufferSize = 2048;
            int readCount;
            byte[] buffer = new byte[2048];

            var fileLength = Int32.Parse(labelSize.Text.Replace("B","").Trim());            
            readCount = responseStream.Read(buffer, 0, bufferSize);
            var totalCount = readCount;
            while (readCount > 0)
            {
                writer.Write(buffer, 0, readCount);
                readCount = responseStream.Read(buffer, 0, bufferSize);
                totalCount = totalCount + readCount;

                var progress = totalCount * 100.0 / fileLength;
                backgroundWorker1.ReportProgress((int)progress);
            }

            responseStream.Close();
            response.Close();
            writer.Close();

            MessageBox.Show("Finished");
        }

        private void backgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            progressBar1.Value = e.ProgressPercentage;
        }
    }
}
