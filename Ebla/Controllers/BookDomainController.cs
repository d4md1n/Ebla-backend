using Ebla.Models;
using System.Collections.Generic;
using System.Web.Http;

namespace Ebla.Controllers
{
    public class BookDomainController : ApiController
    {
        BookUtilController bookUtil;
        UserController userController;
        protected void init()
        {
            bookUtil = new BookUtilController();
            userController = new UserController();
        }

        [HttpPost]
        public string CreateBook(User user, Book book)
        {
            init();
            if (userController.Login(user).Equals("You have been successfully logged in!"))
            {

                if (BookExists(book))
                {
                    return "This book already exists";
                }
                else
                {
                    bookUtil.CreateBook(book);
                    return "The book has been successfully created";
                }
            }
            else
            {
                return "The user is invalid";
            }
        }

        [HttpPost]
        public bool BookExists(Book book)
        {
            init();
            return bookUtil.BookExists(book);
        }

        [HttpPost]
        public List<Book> GetUserBooks(User user)
        {
            init();
            if (userController.Login(user).Equals("You have been successfully logged in!"))
            {
                List<Book> books = bookUtil.GetUserBooks(user);
                return books;
            }
            else
            {
                return new List<Book>();
            }
        }
    }
}