using Ebla.Models;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Web.Http;
using System.Web.Http.Results;

namespace Ebla.Controllers
{
    public class BookController : ApiController
    {
        BookDomainController bookDomain;
        protected void init()
        {
            bookDomain = new BookDomainController();
        }

        [HttpPost]
        public JsonResult<List<Book>> GetUserBooks(User user)
        {
            init();
            return Json(bookDomain.GetUserBooks(user));

        }


    }
}