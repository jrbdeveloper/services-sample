using ClientManager.Helpers;
using ClientManager.Models;
using System.Web.Mvc;

namespace ClientManager.Controllers
{
    public class PropertyController : BaseController
    {
        // GET: Property
        public ActionResult Index()
        {
            return View(Service.GetList<PropertyModel>(Service.Get(Services.Property).Uri()));
        }

        public ActionResult Create()
        {
            return View();
        }

        public ActionResult Details(int id)
        {
            return View(GetPropertyById(id));
        }

        public ActionResult Edit(int id)
        {
            return View(GetPropertyById(id));
        }

        public ActionResult Save(PropertyModel model)
        {
            AddressModel address = null;
            PropertyModel property = null;

            if (model.AddressId != null && model.AddressId > 0)
            {
                var serviceUri = Service.Get(Services.Address).Uri(model.AddressId);

                if (Service.Update(serviceUri, model.Address))
                {
                    address = Service.GetItem<AddressModel>(serviceUri);
                }
            }
            else
            {
                if(model.Address != null)
                {
                    address = Service.Post(Service.Get(Services.Address).Uri(), model.Address);
                }
                else
                {
                    address = new AddressModel();
                }
            }

            if (model.PropertyId > 0)
            {
                var serviceUri = Service.Get(Services.Property).Uri(model.PropertyId);

                if (Service.Update(serviceUri, model))
                {
                    property = Service.GetItem<PropertyModel>(serviceUri);
                }
            }
            else
            {
                property = Service.Post(Service.Get(Services.Property).Uri(), model);
            }

            return RedirectToAction("Index", "Property");
        }

        [HttpGet]
        public JsonResult Delete(int propertyId, int addressId)
        {
            var propertyServiceUri = Service.Get(Services.Property).Uri(propertyId);
            var addressServiceUri = Service.Get(Services.Address).Uri(addressId);

            if (Service.Delete(addressServiceUri))
            {
                if (Service.Delete(propertyServiceUri))
                {
                    return Json(true, JsonRequestBehavior.AllowGet);
                }
            }

            return Json(false, JsonRequestBehavior.AllowGet);
        }
    }
}