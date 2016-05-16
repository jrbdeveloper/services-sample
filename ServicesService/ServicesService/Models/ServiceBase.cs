using System.Collections;
using System.Configuration;
using System.Text;

namespace ServicesService.Models
{
    public abstract class ServiceBase
    {
        private Hashtable _configuration;

        public ServiceBase() {
        }

        public ServiceBase(string section)
        {
            _configuration = (Hashtable)ConfigurationManager.GetSection(section);
        }

        public string Base
        {
            get { return _configuration["base"].ToString(); }
        }

        public string Port
        {
            get { return _configuration["port"].ToString(); }
        }

        public string Api
        {
            get { return _configuration["api"].ToString(); }
        }

        public string BaseUri()
        {
            var returnValue = new StringBuilder();
            returnValue.Append(Base);
            returnValue.Append(":");
            returnValue.Append(Port);
            returnValue.Append(Api);

            return returnValue.ToString();
        }
    }
}