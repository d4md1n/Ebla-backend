using Ebla.Models;
using System.Data;
using System.Data.SqlClient;
using System.Web.Http;
using System.Web.Http.Results;

namespace Ebla.Controllers
{
    public class DatabaseController : ApiController
    {

        string connStr = Ebla.Models.Configuration.CONNECTION_STRING;

        // GET: api/User/5
        [HttpGet]
        public string Get(int id)
        {
            return "value" + id;
        }

        // POST: api/User
        [HttpPost]
        public JsonResult<User> PostUser(Ebla.Models.User u)
        {
            using (SqlConnection db = new SqlConnection(connStr))
            {
                using (SqlCommand cmd = new SqlCommand("insert into user (user_name,user_password)values(@username,@user_password);", db))
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.Add("@user_name", SqlDbType.VarChar).Value = u.user_name;
                    cmd.Parameters.Add("@user_password", SqlDbType.VarChar).Value = u.user_password;

                    db.Open();
                    cmd.ExecuteNonQuery();

                }
            }
            return Json(u);
        }

     }
}
