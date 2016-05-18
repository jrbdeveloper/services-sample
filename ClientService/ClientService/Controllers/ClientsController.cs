using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Web.Http;
using System.Web.Http.Description;
using ClientService.Models;
using System;
using ClientService.Helpers;
using Newtonsoft.Json;

namespace ClientService.Controllers
{
    public class ClientsController : ApiController
    {
        private AcmeUnlimitedClients db = new AcmeUnlimitedClients();

        // GET: api/Clients
        public IQueryable<Client> GetClients()
        {
            var list = db.Clients;
            return list;
        }

        // GET: api/Clients/5
        [ResponseType(typeof(Client))]
        public IHttpActionResult GetClient(int id)
        {
            try
            {
                Client client = db.Clients.Find(id);
                if (client == null)
                {
                    return NotFound();
                }

                Service.LogAudit<Client>(string.Format("api/Clients/{0}", id), JsonConvert.SerializeObject(client), "GetClient");

                return Ok(client);
            }
            catch(Exception ex)
            {
                Service.LogException(ex);
            }

            return NotFound();
        }

        // PUT: api/Clients/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutClient(int id, Client client)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                if (id != client.ClientId)
                {
                    return BadRequest();
                }

                db.Entry(client).State = EntityState.Modified;

                try
                {
                    db.SaveChanges();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ClientExists(id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }

                Service.LogAudit<Client>(string.Format("api/Clients/{0}", id), JsonConvert.SerializeObject(client), "PutClient");

                return StatusCode(HttpStatusCode.NoContent);
            }
            catch(Exception ex)
            {
                Service.LogException(ex);
            }

            return NotFound();            
        }

        // POST: api/Clients
        [ResponseType(typeof(Client))]
        public IHttpActionResult PostClient(Client client)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                db.Clients.Add(client);
                db.SaveChanges();

                Service.LogAudit<Client>("api/Clients", JsonConvert.SerializeObject(client), "PostClient");

                return CreatedAtRoute("DefaultApi", new { id = client.ClientId }, client);
            }
            catch(Exception ex)
            {
                Service.LogException(ex);
            }

            return NotFound();
        }

        // DELETE: api/Clients/5
        [ResponseType(typeof(Client))]
        public IHttpActionResult DeleteClient(int id)
        {
            try
            {
                Client client = db.Clients.Find(id);
                if (client == null)
                {
                    return NotFound();
                }

                db.Clients.Remove(client);
                db.SaveChanges();

                Service.LogAudit<Client>(string.Format("api/Clients/{0}", id), JsonConvert.SerializeObject(client), "DeleteClient");

                return Ok(client);
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

        private bool ClientExists(int id)
        {
            return db.Clients.Count(e => e.ClientId == id) > 0;
        }
    }
}