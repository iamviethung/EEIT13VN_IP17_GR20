using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ArticleApp.Entities;
using ArticleApp.DAO;
using System.Data.SqlClient;
using System.Data;
using System.Web.Configuration;

namespace ArticleApp
{
    public partial class Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                DataView("SELECT * FROM Articles");
            }
        }
        public string ConnectionString = WebConfigurationManager.
            ConnectionStrings["ArticleDBConnectionString"].ConnectionString;

        void DataView(string sql)
        {
            SqlDataAdapter da = new SqlDataAdapter(sql, ConnectionString);
            DataTable dt = new DataTable();
            da.Fill(dt);
            GridView1.DataSource = dt;
            GridView1.DataBind();
        }

        Article GetArticle()
        {

            Article a = new Article();
            a.ArticleName = TextBox1.Text;
            a.ArticleNumber = TextBox2.Text;
            a.Storage = TextBox3.Text;
            a.ShelfNumber = TextBox4.Text;
            a.BrandOfManufacturer = TextBox5.Text;
            return a;
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            Label1.Text = "";
            Article a = GetArticle();
            if (a == null) return;
            ArticleDAO.Add(a);
            DataView("SELECT * FROM Articles");
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            Label1.Text = "";
            Article a = GetArticle();
            if (a == null) return;
            ArticleDAO.Edit(a);
            DataView("SELECT * FROM Articles");
        }

        protected void Button3_Click(object sender, EventArgs e)
        {
            Label1.Text = "";
            Article a = new Article();
            a.ArticleName = TextBox1.Text;
            ArticleDAO.Delete(a);
            DataView("SELECT * FROM Articles");
        }

        protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        protected void Button4_Click(object sender, EventArgs e)
        {
            string name =TextBox6.Text;
            string sql = "select * from Articles where ArticleName like '%" + name + "%'";
            DataView(sql);
        }
    }
}