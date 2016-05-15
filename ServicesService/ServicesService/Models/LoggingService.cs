using ServicesService.Contracts;
using System;

namespace ServicesService.Models
{
    public class LoggingService : IService
    {
        public bool CheckHealth()
        {
            throw new NotImplementedException();
        }

        public string BaseUri()
        {
            return "http://localhost:8085/api/Logger/";
        }
    }
}