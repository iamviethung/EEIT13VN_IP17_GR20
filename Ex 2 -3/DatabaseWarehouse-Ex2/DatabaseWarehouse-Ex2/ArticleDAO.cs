using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
namespace DatabaseWarehouse_Ex2
{
    class ArticleDAO
    {
        static string ConnectionString = ConfigurationManager.ConnectionStrings["ArticleDBConnectionString"].ConnectionString;
        public static void Add(Article a)
        {
            SqlConnection conn = new SqlConnection(ConnectionString);
            conn.Open();
            string sql = "INSERT INTO Articles VALUES(@name, @number, @storage, @shelf, @brand)";
            SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@name", a.ArticleName);
            cmd.Parameters.AddWithValue("@number", a.ArticleNumber);
            cmd.Parameters.AddWithValue("@storage", a.Storage);
            cmd.Parameters.AddWithValue("@shelf", a.ShelfNumber);
            cmd.Parameters.AddWithValue("@brand", a.BrandOfManufacturer);
            cmd.ExecuteNonQuery();
            conn.Close();
        }

        public static void Delete(Article a)
        {
            SqlConnection conn = new SqlConnection(ConnectionString);
            conn.Open();
            string sql = "DELETE FROM Articles WHERE ArticleName = @name";
            SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@name", a.ArticleName);
            cmd.ExecuteNonQuery();
            conn.Close();
        }

        public static void Edit(Article a)
        {
            SqlConnection conn = new SqlConnection(ConnectionString);
            conn.Open();
            string sql = "UPDATE Articles SET  ArticleNumber =@number , Storage = @storage, ShelfNumber = @shelf, " +
                " BrandOfManufacturer = @brand WHERE ArticleName = @name";
            SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@name", a.ArticleName);
            cmd.Parameters.AddWithValue("@number", a.ArticleNumber);
            cmd.Parameters.AddWithValue("@storage", a.Storage);
            cmd.Parameters.AddWithValue("@shelf", a.ShelfNumber);
            cmd.Parameters.AddWithValue("@brand", a.BrandOfManufacturer);
            cmd.ExecuteNonQuery();
            conn.Close();
        }
    }
}
