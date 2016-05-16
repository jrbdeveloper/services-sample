using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using MatchingService.Models;

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
            Matching matching = db.Matchings.Find(id);
            if (matching == null)
            {
                return NotFound();
            }

            return Ok(matching);
        }

        // PUT: api/Matching/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutMatching(int id, Matching matching)
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

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Matching
        [ResponseType(typeof(Matching))]
        public IHttpActionResult PostMatching(Matching matching)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Matchings.Add(matching);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = matching.MatchingId }, matching);
        }

        // DELETE: api/Matching/5
        [ResponseType(typeof(Matching))]
        public IHttpActionResult DeleteMatching(int id)
        {
            Matching matching = db.Matchings.Find(id);
            if (matching == null)
            {
                return NotFound();
            }

            db.Matchings.Remove(matching);
            db.SaveChanges();

            return Ok(matching);
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