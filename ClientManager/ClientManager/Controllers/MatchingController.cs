using ClientManager.Helpers;
using ClientManager.Models;
using System;
using System.Linq;
using System.Web.Mvc;

namespace ClientManager.Controllers
{
    public class MatchingController : BaseController
    {
        public ActionResult Index()
        {
            var list = from item in Service.GetList<MatchingModel>(Service.Get(Services.Matching).Uri())
                       select new MatchingModel
                       {
                           MatchingId = item.MatchingId,
                           Client = GetClientById(item.ClientId),
                           Property = GetPropertyById(item.PropertyId)
                       };

            return View(list);
        }

        public ActionResult Create()
        {
            return View();
        }

        public ActionResult Details(int id)
        {
            return View(GetMatchingById(id));
        }

        private MatchingModel GetMatchingById(int id)
        {
            try
            {
                var matching = Service.GetItem<MatchingModel>(Service.Get(Services.Matching).Uri(id));
                matching.Client = GetClientById(matching.ClientId);
                matching.Property = GetPropertyById(matching.PropertyId);
                
                return matching;
            }
            catch (Exception ex)
            {
                Service.LogException(ex);
            }

            return new MatchingModel();
        }
    }
}