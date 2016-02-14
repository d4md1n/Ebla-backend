using Ebla.Models;
using Newtonsoft.Json.Linq;
using System.Web.Http;

namespace Ebla.Controllers
{
    public class AppDomainController : ApiController
    {
        AppUtilController appUtil;
        BookDomainController bookDomain;
        UserController userController;

        protected void init()
        {
            appUtil = new AppUtilController();
            bookDomain = new BookDomainController();
            userController = new UserController();
        }

        [HttpPost]
        public string AddBookToUser([FromBody]JObject userBook)
        {
            init();
            var user = userBook["user"].ToObject<User>();
            var book = userBook["book"].ToObject<Book>();

            if (userController.Login(user).Equals("You have been successfully logged in!"))
            {
                if (appUtil.UserHasBook(user, book))
                {
                    return "You already own this book!";
                }
                else
                {
                    appUtil.AddBookToUser(user, book);
                    return "The book has been added to the user successfully!";
                }

            }
            else
            {
                return "This user does not exist!";
            }
        }

        [HttpPost]
        public string RemoveBookFromUser([FromBody]JObject userBook)
        {
            init();
            var user = userBook["user"].ToObject<User>();
            var book = userBook["book"].ToObject<Book>();

            if (userController.Login(user).Equals("You have been successfully logged in!"))
            {
                if (bookDomain.BookExists(book))
                {
                    if (appUtil.UserHasBook(user, book))
                    {
                        appUtil.RemoveBookFromUser(user, book);
                        return "The book has been removed from the user successfully!";
                    }
                    else
                    {
                        return "You do not own this book";
                    }
                }
                else
                {
                    return "This book does not exist!";
                }

            }
            else
            {
                return "This user does not exist!";
            }
        }

    }
}