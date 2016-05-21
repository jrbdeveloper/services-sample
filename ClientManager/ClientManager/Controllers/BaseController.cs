using ClientManager.Helpers;
using ClientManager.Models;
using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;

namespace ClientManager.Controllers
{
    public class BaseController : Controller
    {
        protected ClientModel GetClientById(int id)
        {
            try
            {
                var client = Service.GetItem<ClientModel>(Service.Get(Services.Client).Uri(id));
                client.Address = Service.GetItem<AddressModel>(Service.Get(Services.Address).Uri(client.AddressId));

                return client;
            }
            catch(Exception ex)
            {
                Service.LogException(ex);
            }

            return null;          
        }

        protected PropertyModel GetPropertyById(int id)
        {
            try
            {
                var property = Service.GetItem<PropertyModel>(Service.Get(Services.Property).Uri(id));

                if(property.AddressId != null)
                {
                    property.Address = Service.GetItem<AddressModel>(Service.Get(Services.Address).Uri(property.AddressId));
                }                

                return property;
            }
            catch (Exception ex)
            {
                Service.LogException(ex);
            }

            return null;
        }
    }
}