using Ebla.Models;
using System.Data;
using System.Data.SqlClient;
using System.Web.Http;

namespace Ebla.Controllers
{
    public class AppUtilController : ApiController
    {
        BookDomainController bookDomain;

        protected void init()
        {
            bookDomain = new BookDomainController();
        }

        [HttpPost]
        public void AddBookToUser(User user, Book book)
        {
            init();
            string connStr = Ebla.Models.Configuration.CONNECTION_STRING;

            
            if (!bookDomain.BookExists(book))
            {
                bookDomain.CreateBook(user, book);
            }

            using (SqlConnection db = new SqlConnection(connStr))
            {
                using (SqlCommand cmd = new SqlCommand("AddBookToUser", db))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@isbn", SqlDbType.VarChar).Value = book.isbn;
                    cmd.Parameters.Add("@user_name", SqlDbType.VarChar).Value = user.user_name;
                    db.Open();
                    cmd.ExecuteNonQuery();

                }
            }
        }

        [HttpPost]
        public void RemoveBookFromUser(User user, Book book)
        {
            init();
            string connStr = Ebla.Models.Configuration.CONNECTION_STRING;

            using (SqlConnection db = new SqlConnection(connStr))
            {
                using (SqlCommand cmd = new SqlCommand("RemoveBookFromUser", db))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@isbn", SqlDbType.VarChar).Value = book.isbn;
                    cmd.Parameters.Add("@user_name", SqlDbType.VarChar).Value = user.user_name;
                    db.Open();
                    cmd.ExecuteNonQuery();

                }
            }
        }

        public bool UserHasBook(User user, Book book)
        {
            init();
            string connStr = Ebla.Models.Configuration.CONNECTION_STRING;

            using (SqlConnection db = new SqlConnection(connStr))
            {
                using (SqlCommand cmd = new SqlCommand("UserHasBook", db))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@isbn", SqlDbType.VarChar).Value = book.isbn;
                    cmd.Parameters.Add("@user_name", SqlDbType.VarChar).Value = user.user_name;
                    db.Open();
                    SqlDataReader dataReader = cmd.ExecuteReader();
                    dataReader.Read();

                    if (dataReader.GetInt32(0) > 0)
                    {
                        return true;
                    }

                }
            }
            return false;
        }
    }
}