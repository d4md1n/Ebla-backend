using System;
using System.Data;
using System.Data.SqlClient;


namespace Ebla.Models
{
    public class Book
    {
       public String isbn { get; set; }
       public String title { get; set; }
       public String author { get; set; }
       public String genre { get; set; }

       public static void createBook(Book b)
        {
            string connStr = Configuration.CONNECTION_STRING;

            using (SqlConnection db = new SqlConnection(connStr))
            {
                using (SqlCommand cmd = new SqlCommand("InsertBook", db))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@isbn", SqlDbType.VarChar).Value = b.isbn;
                    cmd.Parameters.Add("@title", SqlDbType.VarChar).Value = b.title;
                    cmd.Parameters.Add("@author", SqlDbType.VarChar).Value = b.author;
                    cmd.Parameters.Add("@genre", SqlDbType.VarChar).Value = b.genre;

                    db.Open();
                    cmd.ExecuteNonQuery();

                }
            }
        }

    }
}