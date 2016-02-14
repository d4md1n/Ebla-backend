using Ebla.Models;
using System.Web.Http.Results;
using System.Web.Http;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;

namespace Ebla.Controllers
{
    public class UserController : ApiController
    {
        UserDomainController userDomain;
        protected void init()
        {
            userDomain = new UserDomainController();
        }

        [HttpPost]
        public string CreateUser(User user)
        {
            init();
            return userDomain.CreateUser(user);
        }

        [HttpPost]
        public string Login(User user)
        {
            init();
            return userDomain.Login(user);
        }

        [HttpPost]
        public JsonResult<List<User>> GetUsers(User user)
        {
            init();
            return Json(userDomain.GetUsers(user));
        }

        [HttpPost]
        public string LendBook([FromBody]JObject lendBook)
        {
            init();
            return userDomain.LendBook(lendBook);
        }
    }
}
