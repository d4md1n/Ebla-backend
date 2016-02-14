using Ebla.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Web.Http;

namespace Ebla.Controllers
{
    public class UserUtilController : ApiController
    {
        [HttpPost]
        public bool LoginUser(User u)
        {
            string connStr = Ebla.Models.Configuration.CONNECTION_STRING;

            using (SqlConnection db = new SqlConnection(connStr))
            {
                using (SqlCommand cmd = new SqlCommand("LoginUser", db))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@user_name", SqlDbType.VarChar).Value = u.user_name;
                    cmd.Parameters.Add("@user_password", SqlDbType.VarChar).Value = u.user_password;

                    db.Open();
                    SqlDataReader dataReader = cmd.ExecuteReader();
                    dataReader.Read();

                    if ((int)dataReader.GetInt32(0) == 1)
                    {
                        return true;
                    }

                }
            }
            return false;
        }

        [HttpPost]
        public void CreateUser(User u)
        {
            string connStr = Ebla.Models.Configuration.CONNECTION_STRING;

            using (SqlConnection db = new SqlConnection(connStr))
            {
                using (SqlCommand cmd = new SqlCommand("InsertUser", db))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@user_name", SqlDbType.VarChar).Value = u.user_name;
                    cmd.Parameters.Add("@user_password", SqlDbType.VarChar).Value = u.user_password;

                    db.Open();
                    cmd.ExecuteNonQuery();

                }
            }
        }

        [HttpPost]
        public bool UserExists(User u)
        {
            string connStr = Ebla.Models.Configuration.CONNECTION_STRING;



            using (SqlConnection db = new SqlConnection(connStr))
            {
                using (SqlCommand cmd = new SqlCommand("CheckUserExistence", db))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@user_name", SqlDbType.VarChar).Value = u.user_name;
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

        [HttpPost]
        public List<User> GetUsers(User user)
        {
            string connStr = Ebla.Models.Configuration.CONNECTION_STRING;
            List<User> users = new List<User>();


            using (SqlConnection db = new SqlConnection(connStr))
            {
                using (SqlCommand cmd = new SqlCommand("GetUsers", db))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@user_name", SqlDbType.VarChar).Value = user.user_name;
                    db.Open();
                    SqlDataReader dataReader = cmd.ExecuteReader();
                    while (dataReader.Read())
                    {
                        User tmp = new User();
                        tmp.user_name = dataReader.GetString(0);

                        users.Add(tmp);
                    }
                }
            }

            return users;
        }

        [HttpPost]
        public void LendBook(User owner, User borrower, Book book, String lendDate, String returnDate)
        {
            string connStr = Ebla.Models.Configuration.CONNECTION_STRING;

            using (SqlConnection db = new SqlConnection(connStr))
            {
                using (SqlCommand cmd = new SqlCommand("LendBook", db))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@owner_name", SqlDbType.VarChar).Value = owner.user_name;
                    cmd.Parameters.Add("@borrower_name", SqlDbType.VarChar).Value = borrower.user_name;
                    cmd.Parameters.Add("@lend_date", SqlDbType.VarChar).Value = lendDate;
                    cmd.Parameters.Add("@return_date", SqlDbType.VarChar).Value = returnDate;
                    cmd.Parameters.Add("@book_isbn", SqlDbType.VarChar).Value = book.isbn;

                    db.Open();
                    cmd.ExecuteNonQuery();

                }
            }
        }
    }
}