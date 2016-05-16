using System;
using ServicesService.Contracts;

namespace ServicesService.Models
{
    public class PropertyService : ServiceBase, IService
    {
        public PropertyService() : base("Property")
        {
        }

        public bool CheckHealth()
        {
            throw new NotImplementedException();
        }
    }
}