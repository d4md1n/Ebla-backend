using Ebla.Models;
using Newtonsoft.Json.Linq;
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
    }
}