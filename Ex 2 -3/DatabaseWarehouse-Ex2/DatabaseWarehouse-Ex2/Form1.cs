using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace DatabaseWarehouse_Ex2
{
    public partial class Form1 : Form
    {
        string ConnectionString = ConfigurationManager.ConnectionStrings["ArticleDBConnectionString"].ConnectionString;

        public void DataView(string sql)
        {
            
            SqlDataAdapter da = new SqlDataAdapter(sql, ConnectionString);
            DataSet ds = new DataSet();
            da.Fill(ds);
            dataGridView1.DataSource = ds.Tables[0];
        }

        public Form1()
        {
            InitializeComponent();
            string sql = "select * from Articles";
            DataView(sql);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string name = textBox1.Text;
            string sql = "select * from Articles where ArticleName like '%" + name + "%'";
            DataView(sql);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form2 f2 = new Form2();
            f2.form1 = this;
            f2.Action = "add";
            f2.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Form2 f2 = new Form2();
            f2.form1 = this;
            f2.Action = "edit";
            int i = dataGridView1.CurrentCell.RowIndex;
            DataGridViewRow dr = dataGridView1.Rows[i];
            f2.textBox1.Text = dr.Cells[0].Value.ToString();
            f2.textBox2.Text = dr.Cells[1].Value.ToString();
            f2.textBox3.Text = dr.Cells[2].Value.ToString();
            f2.textBox4.Text = dr.Cells[3].Value.ToString();
            f2.textBox5.Text = dr.Cells[4].Value.ToString();
            f2.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            DialogResult result =MessageBox.Show("Delete selected row?", "Confirm", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
            if (result == DialogResult.OK)
            {
                int i = dataGridView1.CurrentCell.RowIndex;
                DataGridViewRow dr = dataGridView1.Rows[i];
                Article a = new Article();
                a.ArticleName = dr.Cells[0].Value.ToString();
                ArticleDAO.Delete(a);
                DataView("select * from Articles");
            }
        }
    }
}
