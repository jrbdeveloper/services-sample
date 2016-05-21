using ClientManager.Helpers;
using ClientManager.Models;
using System.IO;
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
            var address = SaveAddress(model);
            var property = SaveProperty(model);
            SaveGallery(model, property);

            return RedirectToAction("Index", "Property");
        }

        private PropertyModel SaveProperty(PropertyModel model)
        {
            PropertyModel property = null;

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

            return property;
        }

        private AddressModel SaveAddress(PropertyModel model)
        {
            AddressModel address = null;

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
                if (model.Address != null)
                {
                    address = Service.Post(Service.Get(Services.Address).Uri(), model.Address);
                }
                else
                {
                    address = new AddressModel();
                }
            }

            return address;
        }

        private void SaveGallery(PropertyModel model, PropertyModel property)
        {
            //if (model.Photo != null && model.Photo.Length > 0)
            //{
            //foreach (var file in model.Photos)
            //{
            if (model.Photo != null && model.Photo.ContentLength > 0)
            {
                var fileName = Path.GetFileName(model.Photo.FileName);
                var path = Path.Combine(Server.MapPath("~/Uploads"), fileName);

                model.Photo.SaveAs(path);

                Service.Post(Service.Get(Services.Gallery).Uri(), new GalleryModel
                {
                    PropertyId = property.PropertyId,
                    ImagePath = path
                });
            }
                //}
            //}
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