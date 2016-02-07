using System;
using System.Data;
using System.Data.SqlClient;

namespace Ebla.Models
{
    public class User
    {
        public String user_id { get; set; }
        public String user_name { get; set; }
        public String user_password { get; set; }



        public static void createUser(User u) {
           string connStr = Configuration.CONNECTION_STRING;
           
            using (SqlConnection db = new SqlConnection(connStr)) {
                using (SqlCommand cmd = new SqlCommand("InsertUser", db))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@user_name", SqlDbType.VarChar).Value=u.user_name;
                    cmd.Parameters.Add("@user_password", SqlDbType.VarChar).Value = u.user_password;

                    db.Open();
                    cmd.ExecuteNonQuery();

                }
            }
        }
        public static Boolean LoginUser(User u)
        {
            string connStr = Configuration.CONNECTION_STRING;

            

            using (SqlConnection db = new SqlConnection(connStr))
            {
                using (SqlCommand cmd = new SqlCommand("LoginUser", db))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@user_name", SqlDbType.VarChar).Value = u.user_name;
                    cmd.Parameters.Add("@user_password", SqlDbType.VarChar).Value = u.user_password;

                    db.Open();
                    SqlDataReader dataReader= cmd.ExecuteReader();
                    dataReader.Read();

                    if ((int)dataReader.GetInt32(0) == 1)
                    {
                        return true;
                    }

                }
            }
            return false;
        }

        public static Boolean userExists(User u)
        {
            string connStr = Configuration.CONNECTION_STRING;



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


    }
}