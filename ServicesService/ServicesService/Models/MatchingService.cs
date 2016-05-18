using System;
using ServicesService.Contracts;

namespace ServicesService.Models
{
    public class MatchingService : ServiceBase, IService
    {
        public MatchingService() : base("Matching")
        {
        }

        public bool CheckHealth()
        {
            throw new NotImplementedException();
        }
    }
}