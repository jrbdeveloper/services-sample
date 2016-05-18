using ServicesService.Models;
using System.Web.Http;

namespace ServicesService.Controllers
{
    public class ServicesController : ApiController
    {
        // GET: api/Services/5
        public string Get(string id)
        {
            return Service.Get(id).Uri();
        }
    }
}