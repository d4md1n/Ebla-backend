using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

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
    }
}