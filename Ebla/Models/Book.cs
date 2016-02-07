using System;
using System.Collections.Generic;
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

       public static Boolean bookExists(Book b)
        {
            string connStr = Configuration.CONNECTION_STRING;

            using (SqlConnection db = new SqlConnection(connStr))
            {
                using (SqlCommand cmd = new SqlCommand("CheckBookExistence", db))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@isbn", SqlDbType.VarChar).Value = b.isbn;
                    db.Open();
                    SqlDataReader dataReader = cmd.ExecuteReader();
                    dataReader.Read();

                    if (dataReader.GetInt32(0) > 0)
                    {
                        return true;
                    }

                }
            }
            return false;
        }

       public static List<Book> getUserBooks(User user)
       {
            string connStr = Ebla.Models.Configuration.CONNECTION_STRING;
            List<Book> books = new List<Book>();


            using (SqlConnection db = new SqlConnection(connStr))
            {
                using (SqlCommand cmd = new SqlCommand("GetUserBooks", db))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@user_name", SqlDbType.VarChar).Value = user.user_name;
                    db.Open();
                    SqlDataReader dataReader = cmd.ExecuteReader();
                    while (dataReader.Read())
                    {
                        Book book = new Book();
                        book.isbn = dataReader.GetString(0);
                        book.title = dataReader.GetString(1);
                        book.author = dataReader.GetString(2);
                        book.genre = dataReader.GetString(3);
                        books.Add(book);
                    }
                }
            }

            return books;
        }
    }
}