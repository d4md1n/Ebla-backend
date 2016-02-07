using Ebla.Models;
using System.Web.Http.Results;
using System.Web.Http;
using System;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;

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

        [HttpPost]
        public JsonResult<List<User>> GetUsers(User user)
        {
            if (Ebla.Models.User.LoginUser(user))
            {
                return Json(Ebla.Models.User.getUsers(user));
            }
            else
            {
                return Json(new List<User>());
            }
        }

        [HttpPost]
        public string LendBook([FromBody]JObject lendBook)
        {
            var owner = lendBook["owner"].ToObject<User>();
            var borrower = lendBook["borrower"].ToObject<User>();
            var book = lendBook["book"].ToObject<Book>();
            var lendDate = lendBook["lendDate"].ToObject<String>();
            var returnDate = lendBook["returnDate"].ToObject<String>();

            if (Ebla.Models.User.LoginUser(owner))
            {
                if (AppModel.userHasBook(owner, book))
                {
                    Ebla.Models.User.LendBook(owner, borrower, book, lendDate, returnDate);
                    return "lending complete";
                }
                else
                {
                    return "lending failed";
                }
            }
            else
            {
                return "lending failed";
            }
        }
    }
}
