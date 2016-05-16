using System;
using ServicesService.Contracts;

namespace ServicesService.Models
{
    public class LoggingService : ServiceBase, IService
    {
        public LoggingService() : base("Logging")
        {
        }

        public bool CheckHealth()
        {
            throw new NotImplementedException();
        }
    }
}