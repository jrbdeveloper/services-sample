using LoggingService.Models;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Description;

namespace LoggingService.Controllers
{
    public class LoggerController : BaseApiController
    {
        [Route("api/logger/getAudits")]
        public IQueryable<Audit> GetAudits()
        {
            return db.Audits;
        }

        [Route("api/logger/getAudit/{id}")]
        [ResponseType(typeof(Audit))]
        public IHttpActionResult GetAudit(int id)
        {
            Audit audit = db.Audits.Find(id);
            if (audit == null)
            {
                return NotFound();
            }

            return Ok(audit);
        }
        
        [HttpPost, Route("api/logger/postAudit"), ResponseType(typeof(Audit))]
        public IHttpActionResult PostAudit(Audit audit)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            using (var connection = db.Database.Connection)
            {
                connection.Open();
                db.Audits.Add(audit);
                db.SaveChanges();
            }                

            return CreatedAtRoute("DefaultApi", new { id = audit.Id }, audit);
        }

        [Route("api/logger/getExceptions")]
        public IQueryable<Exception> GetExceptions()
        {
            return db.Exceptions;
        }

        [Route("api/logger/getException/{id}")]
        [ResponseType(typeof(Exception))]
        public IHttpActionResult GetException(int id)
        {
            Exception exception = db.Exceptions.Find(id);
            if (exception == null)
            {
                return NotFound();
            }

            return Ok(exception);
        }

        [Route("api/logger/postException")]
        [ResponseType(typeof(Exception))]
        public IHttpActionResult PostException(Exception exception)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Exceptions.Add(exception);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = exception.Id }, exception);
        }

        private bool ExceptionExists(int id)
        {
            return db.Exceptions.Count(e => e.Id == id) > 0;
        }

        private bool AuditExists(int id)
        {
            return db.Audits.Count(e => e.Id == id) > 0;
        }
    }
}
