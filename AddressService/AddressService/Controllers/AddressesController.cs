using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Web.Http;
using System.Web.Http.Description;
using AddressService.Models;
using System;
using Newtonsoft.Json;
using AddressService.Helpers;

namespace AddressService.Controllers
{
    public class AddressesController : ApiController
    {
        private AcmeUnlimitedAddress db = new AcmeUnlimitedAddress();

        // GET: api/Addresses
        public IQueryable<Address> GetAddresses()
        {
            Service.LogAudit<Address>("api/Addresses/", JsonConvert.SerializeObject(db.Addresses), "GetAddresses");
            return db.Addresses;
        }

        // GET: api/Addresses/5
        [ResponseType(typeof(Address))]
        public IHttpActionResult GetAddress(int id)
        {
            try
            {
                Address address = db.Addresses.Find(id);
                if (address == null)
                {
                    return NotFound();
                }

                Service.LogAudit<Address>(string.Format("api/Addresses/{0}", id), JsonConvert.SerializeObject(address), "GetAddress");

                return Ok(address);
            }
            catch(Exception ex)
            {
                Service.LogException(ex);
            }

            return NotFound();
        }

        // PUT: api/Addresses/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutAddress(int id, Address address)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                if (id != address.AddressId)
                {
                    return BadRequest();
                }

                db.Entry(address).State = EntityState.Modified;

                try
                {
                    db.SaveChanges();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AddressExists(id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }

                Service.LogAudit<Address>(string.Format("api/Addresses/{0}", id), JsonConvert.SerializeObject(address), "PutAddress");
                return StatusCode(HttpStatusCode.NoContent);
            }
            catch(Exception ex)
            {
                Service.LogException(ex);
            }

            return NotFound();
        }

        // POST: api/Addresses
        [ResponseType(typeof(Address))]
        public IHttpActionResult PostAddress(Address address)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                db.Addresses.Add(address);
                db.SaveChanges();

                Service.LogAudit<Address>("api/Addresses", JsonConvert.SerializeObject(address), "PutAddress");

                return CreatedAtRoute("DefaultApi", new { id = address.AddressId }, address);
            }
            catch(Exception ex)
            {
                Service.LogException(ex);
            }

            return NotFound();            
        }

        // DELETE: api/Addresses/5
        [ResponseType(typeof(Address))]
        public IHttpActionResult DeleteAddress(int id)
        {
            try
            {
                Address address = db.Addresses.Find(id);
                if (address == null)
                {
                    return NotFound();
                }

                db.Addresses.Remove(address);
                db.SaveChanges();
                Service.LogAudit<Address>(string.Format("api/Addresses/{0}", id), JsonConvert.SerializeObject(address), "DeleteAddress");

                return Ok(address);
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

        private bool AddressExists(int id)
        {
            return db.Addresses.Count(e => e.AddressId == id) > 0;
        }
    }
}