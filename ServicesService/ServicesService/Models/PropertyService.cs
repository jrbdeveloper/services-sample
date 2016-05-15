using System;
using ServicesService.Contracts;

namespace ServicesService.Models
{
    public class PropertyService : IService
    {
        public string BaseUri()
        {
            return "http://localhost:8084/api/Property/";
        }

        public bool CheckHealth()
        {
            throw new NotImplementedException();
        }
    }
}