using System;
using System.Collections.Generic;
using System.Web.Http;
using Ebla.Models;
using Newtonsoft.Json.Linq;

namespace Ebla.Controllers
{
    public class UserDomainController : ApiController
    {
        AppUtilController appUtil;
        UserUtilController userUtil;

        protected void init()
        {
            appUtil = new AppUtilController();
            userUtil = new UserUtilController();
        }

        [HttpPost]
        public string CreateUser(User user)
        {
            init();
            if (userUtil.UserExists(user))
            {
                return "User exists already!";
            }
            else
            {
                userUtil.CreateUser(user);
                return "User created successfully!";
            }
        }

        [HttpPost]
        public string Login(User user)
        {
            init();
            if (userUtil.LoginUser(user))
            {
                return "You have been successfully logged in!";
            }
            else
            {
                return "Your username or password are not valid!";
            }
        }

        [HttpPost]
        public List<User> GetUsers(User user)
        {
            init();
            if (userUtil.LoginUser(user))
            {
                return userUtil.GetUsers(user);
            }
            else
            {
                return new List<User>();
            }
        }

        [HttpPost]
        public string LendBook([FromBody]JObject lendBook)
        {
            init();
            var owner = lendBook["owner"].ToObject<User>();
            var borrower = lendBook["borrower"].ToObject<User>();
            var book = lendBook["book"].ToObject<Book>();
            var lendDate = lendBook["lendDate"].ToObject<String>();
            var returnDate = lendBook["returnDate"].ToObject<String>();

            if (userUtil.LoginUser(owner))
            {
                if (appUtil.UserHasBook(owner, book))
                {
                    userUtil.LendBook(owner, borrower, book, lendDate, returnDate);
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