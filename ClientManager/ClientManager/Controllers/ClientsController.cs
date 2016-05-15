using ClientManager.Helpers;
using ClientManager.Models;
using System.Web.Mvc;

namespace ClientManager.Controllers
{
    public class ClientsController : BaseController
    {
        public ActionResult Index()
        {
            return View(Service.GetList<ClientModel>(Service.Get(Services.Client).Uri()));
        }

        public ActionResult Details(int id)
        {
            return View(GetClientById(id));
        }

        public ActionResult Edit(int id)
        {
            return View(GetClientById(id));
        }

        public ActionResult Create()
        {
            return View();
        }

        public ActionResult Save(ClientModel model)
        {
            AddressModel address = null;
            ClientModel client = null;

            if(model.AddressId > 0)
            {
                var serviceUri = Service.Get(Services.Address).Uri(model.AddressId);

                if (Service.Update(serviceUri, model.Address))
                {
                    address = Service.GetItem<AddressModel>(serviceUri);
                }
            }
            else
            {
                address = Service.Post(Service.Get(Services.Address).Uri(), model.Address);
            }
            
            if(model.ClientId > 0)
            {
                var serviceUri = Service.Get(Services.Client).Uri(model.ClientId);

                if (Service.Update(serviceUri, model))
                {
                    client = Service.GetItem<ClientModel>(serviceUri);
                }
            }
            else
            {
                client = Service.Post(Service.Get(Services.Client).Uri(), model);
            }
            
            return RedirectToAction("Index", "Clients");
        }

        [HttpGet]
        public JsonResult Delete(int clientId, int addressId)
        {
            var clientServiceUri = Service.Get(Services.Client).Uri(clientId);
            var addressServiceUri = Service.Get(Services.Address).Uri(addressId);

            if (Service.Delete(addressServiceUri))
            {
                if (Service.Delete(clientServiceUri))
                {
                    return Json(true, JsonRequestBehavior.AllowGet);
                }
            }

            return Json(false, JsonRequestBehavior.AllowGet);
        }
    }
}