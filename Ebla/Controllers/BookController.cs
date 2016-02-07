using Ebla.Models;
using System.Web.Http;
using System.Web.Http.Results;

namespace Ebla.Controllers
{
    public class BookController : ApiController
    {
        [HttpPost]
        public JsonResult<Book> CreateBook(Book b)
        {
            Book.createBook(b);
            return Json(b);
        }
    }
}