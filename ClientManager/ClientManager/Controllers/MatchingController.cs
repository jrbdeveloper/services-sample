﻿using ClientManager.Helpers;
using ClientManager.Models;
using System;
using System.Web.Mvc;

namespace ClientManager.Controllers
{
    public class MatchingController : BaseController
    {
        public ActionResult Index()
        {
            var uri = Service.Get(Services.Matching).Uri();
            var list = Service.GetList<MatchingModel>(uri);
            return View(list);
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