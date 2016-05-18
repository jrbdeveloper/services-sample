using System;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Web.Http;
using System.Web.Http.Description;
using MatchingService.Models;
using MatchingService.Helpers;
using Newtonsoft.Json;

namespace MatchingService.Controllers
{
    public class MatchingController : ApiController
    {
        private MatchingModels db = new MatchingModels();

        // GET: api/Matching
        public IQueryable<Matching> GetMatchings()
        {
            return db.Matchings;
        }

        // GET: api/Matching/5
        [ResponseType(typeof(Matching))]
        public IHttpActionResult GetMatching(int id)
        {
            try
            {
                Matching matching = db.Matchings.Find(id);
                if (matching == null)
                {
                    return NotFound();
                }

                Service.LogAudit<Matching>(string.Format("api/Matching/{0}", id), JsonConvert.SerializeObject(matching), "GetMatching");

                return Ok(matching);
            }
            catch(Exception ex)
            {
                Service.LogException(ex);
            }

            return NotFound();            
        }

        // PUT: api/Matching/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutMatching(int id, Matching matching)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                if (id != matching.MatchingId)
                {
                    return BadRequest();
                }

                db.Entry(matching).State = EntityState.Modified;

                try
                {
                    db.SaveChanges();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MatchingExists(id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }

                Service.LogAudit<Matching>(string.Format("api/Matching/{0}", id), JsonConvert.SerializeObject(matching), "PutMatching");

                return StatusCode(HttpStatusCode.NoContent);
            }
            catch(Exception ex)
            {
                Service.LogException(ex);
            }

            return NotFound();
        }

        // POST: api/Matching
        [ResponseType(typeof(Matching))]
        public IHttpActionResult PostMatching(Matching matching)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                db.Matchings.Add(matching);
                db.SaveChanges();

                Service.LogAudit<Matching>("api/Matching", JsonConvert.SerializeObject(matching), "PostMatching");

                return CreatedAtRoute("DefaultApi", new { id = matching.MatchingId }, matching);
            }
            catch(Exception ex)
            {
                Service.LogException(ex);
            }

            return NotFound();
        }

        // DELETE: api/Matching/5
        [ResponseType(typeof(Matching))]
        public IHttpActionResult DeleteMatching(int id)
        {
            try
            {
                Matching matching = db.Matchings.Find(id);
                if (matching == null)
                {
                    return NotFound();
                }

                db.Matchings.Remove(matching);
                db.SaveChanges();

                Service.LogAudit<Matching>(string.Format("api/Matching/{0}", id), JsonConvert.SerializeObject(matching), "DeleteMatching");

                return Ok(matching);
            }
            catch(Exception ex)
            {
                Service.LogException(ex);
            }

            return NotFound();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool MatchingExists(int id)
        {
            return db.Matchings.Count(e => e.MatchingId == id) > 0;
        }
    }
}