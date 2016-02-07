using Ebla.Models;
using System.Web.Http.Results;
using System.Web.Http;
using System;

namespace Ebla.Controllers
{
    public class UserController : ApiController
    {
        // GET: api/User/5
        [HttpGet]
        public string GetUser(int id)
        {
            return "value"+id;
        }

        [HttpPost]
        public string CreateUser(Ebla.Models.User u)
        {
            if (Ebla.Models.User.userExists(u))
            {
                return "User exists already!";
            }
            else
            {
                Ebla.Models.User.createUser(u);
                return "User created successfully!";
            }
        }

        [HttpPost]
        public JsonResult<bool> Login(Ebla.Models.User u)
        {
            bool temp = Ebla.Models.User.LoginUser(u);
            return Json(temp);
        }
    }
}
