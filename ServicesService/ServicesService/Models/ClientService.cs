using System;
using ServicesService.Contracts;

namespace ServicesService.Models
{
    public class ClientService : ServiceBase, IService
    {
        public ClientService() : base("Client")
        {
        }

        public bool CheckHealth()
        {
            throw new NotImplementedException();
        }
    }
}