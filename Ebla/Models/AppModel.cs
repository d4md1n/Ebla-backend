using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Ebla.Models
{
    public class AppModel
    {
        public static void addBookToUser(User user, Book book)
        {
            string connStr = Configuration.CONNECTION_STRING;

            using (SqlConnection db = new SqlConnection(connStr))
            {
                using (SqlCommand cmd = new SqlCommand("AddBookToUser", db))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@isbn", SqlDbType.VarChar).Value = book.isbn;
                    cmd.Parameters.Add("@user_name", SqlDbType.VarChar).Value = user.user_name;
                    db.Open();
                    cmd.ExecuteNonQuery();

                }
            }
        }

        public static void removeBookFromUser(User user, Book book)
        {
            string connStr = Configuration.CONNECTION_STRING;

            using (SqlConnection db = new SqlConnection(connStr))
            {
                using (SqlCommand cmd = new SqlCommand("RemoveBookFromUser", db))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@isbn", SqlDbType.VarChar).Value = book.isbn;
                    cmd.Parameters.Add("@user_name", SqlDbType.VarChar).Value = user.user_name;
                    db.Open();
                    cmd.ExecuteNonQuery();

                }
            }
        }

        public static Boolean userHasBook(User user, Book book)
        {
            string connStr = Configuration.CONNECTION_STRING;

            using (SqlConnection db = new SqlConnection(connStr))
            {
                using (SqlCommand cmd = new SqlCommand("UserHasBook", db))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@isbn", SqlDbType.VarChar).Value = book.isbn;
                    cmd.Parameters.Add("@user_name", SqlDbType.VarChar).Value = user.user_name;
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
    }
}