using MatchingService.Models;
using System.Web.Http;
using System.Web.Mvc;

namespace MatchingService.Controllers
{
    public class HomeController : ApiController
    {
        public JsonResult Index()
        {
            return null;
            //return Json<Matching>(new Matching());
        }
    }
}
