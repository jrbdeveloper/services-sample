using System;
using ServicesService.Contracts;

namespace ServicesService.Models
{
    public class AddressService : IService
    {
        public bool CheckHealth()
        {
            throw new NotImplementedException();
        }

        public string BaseUri()
        {
            return "http://localhost:8082/api/Addresses/";
        }
    }
}