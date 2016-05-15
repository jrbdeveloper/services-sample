using System;
using ServicesService.Contracts;

namespace ServicesService.Models
{
    public class ClientService : IService
    {
        public bool CheckHealth()
        {
            throw new NotImplementedException();
        }

        public string BaseUri()
        {
            return "http://localhost:8081/api/Clients/";
        }
    }
}