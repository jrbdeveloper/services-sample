using LoggingService.Models;
using System.Web.Http;

namespace LoggingService.Controllers
{
    public class BaseApiController : ApiController
    {
        protected LoggingModels db = new LoggingModels();

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }

            base.Dispose(disposing);
        }
    }
}
