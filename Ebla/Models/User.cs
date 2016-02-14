using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace Ebla.Models
{
    public class User
    {
        public String user_id { get; set; }
        public String user_name { get; set; }
        public String user_password { get; set; }

    }
}