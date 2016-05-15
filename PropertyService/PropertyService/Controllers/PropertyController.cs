using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Web.Http;
using System.Web.Http.Description;
using PropertyService.Models;
using System;
using PropertyService.Helpers;
using Newtonsoft.Json;

namespace PropertyService.Controllers
{
    public class PropertyController : ApiController
    {
        private PropertyModels db = new PropertyModels();

        // GET: api/Property
        public IQueryable<Property> GetProperties()
        {
            return db.Properties;
        }

        // GET: api/Property/5
        [ResponseType(typeof(Property))]
        public IHttpActionResult GetProperty(int id)
        {
            try
            {
                Property property = db.Properties.Find(id);
                if (property == null)
                {
                    return NotFound();
                }

                Service.LogAudit<Property>(string.Format("api/Property/{0}", id), JsonConvert.SerializeObject(property), "GetProperty");

                return Ok(property);
            }
            catch (Exception ex)
            {
                Service.LogException(ex);
            }

            return NotFound();
        }

        // PUT: api/Property/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutProperty(int id, Property property)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                if (id != property.PropertyId)
                {
                    return BadRequest();
                }

                db.Entry(property).State = EntityState.Modified;

                try
                {
                    db.SaveChanges();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PropertyExists(id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }

                Service.LogAudit<Property>(string.Format("api/Property/{0}", id), JsonConvert.SerializeObject(property), "PutProperty");

                return StatusCode(HttpStatusCode.NoContent);
            }
            catch(Exception ex)
            {
                Service.LogException(ex);
            }

            return NotFound();
        }

        // POST: api/Property
        [ResponseType(typeof(Property))]
        public IHttpActionResult PostProperty(Property property)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                db.Properties.Add(property);
                db.SaveChanges();

                Service.LogAudit<Property>("api/Property", JsonConvert.SerializeObject(property), "PostProperty");

                return CreatedAtRoute("DefaultApi", new { id = property.PropertyId }, property);
            }
            catch (Exception ex)
            {
                Service.LogException(ex);
            }

            return NotFound();
        }

        // DELETE: api/Property/5
        [ResponseType(typeof(Property))]
        public IHttpActionResult DeleteProperty(int id)
        {
            try
            {
                Property property = db.Properties.Find(id);
                if (property == null)
                {
                    return NotFound();
                }

                db.Properties.Remove(property);
                db.SaveChanges();

                Service.LogAudit<Property>(string.Format("api/Property/{0}", id), JsonConvert.SerializeObject(property), "DeleteProperty");

                return Ok(property);
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

        private bool PropertyExists(int id)
        {
            return db.Properties.Count(e => e.PropertyId == id) > 0;
        }
    }
}