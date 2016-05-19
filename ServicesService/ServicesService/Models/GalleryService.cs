using System;
using ServicesService.Contracts;

namespace ServicesService.Models
{
    public class GalleryService : ServiceBase, IService
    {
        public GalleryService() : base("Gallery")
        {
        }

        public bool CheckHealth()
        {
            throw new NotImplementedException();
        }
    }
}