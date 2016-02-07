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
        public string Login(Ebla.Models.User u)
        {
            if (Ebla.Models.User.LoginUser(u))
            {
                return "You have been successfully logged in!";
            }
            else
            {
                return "Your username or password are not valid!";
            }
        }
    }
}
