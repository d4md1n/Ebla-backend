using Ebla.Models;
using System.Web.Http.Results;
using System.Web.Http;

namespace Ebla.Controllers
{
    public class UserController : ApiController
    {
        // GET: api/User/5
        [HttpGet]
        public string Get(int id)
        {
            return "value"+id;
        }

        // POST: api/User
        [HttpPost]
        public JsonResult<User> PostUser(Ebla.Models.User u)
        {
            Ebla.Models.User.createUser(u);
            return Json(u);
        }

    }
}
