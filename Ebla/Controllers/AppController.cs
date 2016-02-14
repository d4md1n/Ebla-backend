using Newtonsoft.Json.Linq;
using System.Web.Http;

namespace Ebla.Controllers
{
    public class AppController : ApiController
    {
        AppDomainController appDomain;

        protected void init()
        {
            appDomain = new AppDomainController();
        }


        [HttpPost]
        public string AddBookToUser([FromBody]JObject userBook)
        {
            init();
            return appDomain.AddBookToUser(userBook);
        }

        [HttpPost]
        public string RemoveBookFromUser([FromBody]JObject userBook)
        {
            init();
            return appDomain.RemoveBookFromUser(userBook);
        }


    }
}