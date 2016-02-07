using Ebla.Models;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Web.Http;
using System.Web.Http.Results;

namespace Ebla.Controllers
{
    public class BookController : ApiController
    {
        [HttpPost]
        public string CreateBook([FromBody]JObject userBook)
        {

            var user = userBook["user"].ToObject<User>();
            var book = userBook["book"].ToObject<Book>();

            if (Ebla.Models.User.LoginUser(user))
            {

                if (Book.bookExists(book))
                {
                    return "This book already exists";
                }
                else
                {
                    Book.createBook(book);
                    return "The book has been successfully created";
                }
            }
            else
            {
                return "The user is invalid";
            }
        }

        [HttpPost]
        public JsonResult<List<Book>> GetUserBooks(User user)
        {
            if (Ebla.Models.User.LoginUser(user))
            {
                List<Book> books = Book.getUserBooks(user);
                return Json(books);
            }
            else
            {
                return Json(new List<Book>());
            }
        }
    }
}