using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace DatabaseWarehouse_Ex2
{
    public partial class Form2 : Form
    {
        public Form1 form1 { get; set; }
        public string Action { get; set; }
        public Form2()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Article a = new Article();
            a.ArticleName = textBox1.Text;
            a.ArticleNumber = textBox2.Text;
            a.Storage = textBox3.Text;
            a.ShelfNumber = textBox4.Text;
            a.BrandOfManufacturer = textBox5.Text;
            if (Action.Equals("add"))
            {
                ArticleDAO.Add(a);
                form1.DataView("select * from articles");
            }
            else if (Action.Equals("edit"))
            {
                ArticleDAO.Edit(a);
                form1.DataView("select * from articles");
                this.Close();
            }
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
