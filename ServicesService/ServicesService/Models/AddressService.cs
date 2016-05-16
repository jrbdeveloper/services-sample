using System;
using ServicesService.Contracts;

namespace ServicesService.Models
{
    public class AddressService : ServiceBase, IService
    {
        public AddressService() : base("Address")
        {
        }

        public bool CheckHealth()
        {
            throw new NotImplementedException();
        }        
    }
}