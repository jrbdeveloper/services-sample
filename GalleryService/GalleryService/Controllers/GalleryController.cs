using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Web.Http;
using System.Web.Http.Description;
using GalleryService.Models;

namespace GalleryService.Controllers
{
    public class GalleryController : ApiController
    {
        private GalleryModel db = new GalleryModel();

        // GET: api/Gallery
        public IQueryable<PropertyGallery> GetPropertyGalleries()
        {
            return db.PropertyGalleries;
        }

        // GET: api/Gallery/5
        [ResponseType(typeof(PropertyGallery))]
        public IHttpActionResult GetPropertyGallery(int id)
        {
            PropertyGallery propertyGallery = db.PropertyGalleries.Find(id);
            if (propertyGallery == null)
            {
                return NotFound();
            }

            return Ok(propertyGallery);
        }

        // PUT: api/Gallery/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutPropertyGallery(int id, PropertyGallery propertyGallery)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != propertyGallery.PropertyImageId)
            {
                return BadRequest();
            }

            db.Entry(propertyGallery).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PropertyGalleryExists(id))
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

        // POST: api/Gallery
        [ResponseType(typeof(PropertyGallery))]
        public IHttpActionResult PostPropertyGallery(PropertyGallery propertyGallery)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.PropertyGalleries.Add(propertyGallery);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = propertyGallery.PropertyImageId }, propertyGallery);
        }

        // DELETE: api/Gallery/5
        [ResponseType(typeof(PropertyGallery))]
        public IHttpActionResult DeletePropertyGallery(int id)
        {
            PropertyGallery propertyGallery = db.PropertyGalleries.Find(id);
            if (propertyGallery == null)
            {
                return NotFound();
            }

            db.PropertyGalleries.Remove(propertyGallery);
            db.SaveChanges();

            return Ok(propertyGallery);
        }

        [Route("api/Gallery/GetVersion")]
        public string GetVersion()
        {
            return typeof(GalleryController).Assembly.GetName().Version.ToString();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool PropertyGalleryExists(int id)
        {
            return db.PropertyGalleries.Count(e => e.PropertyImageId == id) > 0;
        }
    }
}