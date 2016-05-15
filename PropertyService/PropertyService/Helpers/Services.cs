using Newtonsoft.Json;
using PropertyService.Models;
using System;
using System.Net.Http;
using System.Text;

namespace PropertyService.Helpers
{
    public class Service
    {
        private static string _service;

        public string Uri(int? value = null)
        {
            return getServicesPath(value, null);
        }

        public string Call(string value = null)
        {
            return getServicesPath(null, value);
        }

        private string getServicesPath(int? intVal = null, string strVal = null)
        {
            var returnValue = string.Empty;

            using (var client = new HttpClient())
            {
                var responseMessage = client.GetAsync("http://localhost:8086/api/Services/" + _service).GetAwaiter().GetResult();
                if (responseMessage.IsSuccessStatusCode)
                {
                    var responseData = responseMessage.Content.ReadAsStringAsync().GetAwaiter().GetResult();
                    var item = JsonConvert.DeserializeObject<string>(responseData);

                    if (intVal != null)
                    {
                        returnValue = (intVal.HasValue) ? item + intVal : item;
                    }
                    else
                    {
                        returnValue = (!string.IsNullOrEmpty(strVal)) ? item + strVal : item;
                    }
                }
            }

            return returnValue;
        }

        public static Service Make(Services service)
        {
            return Get(service);
        }

        public static Service Get(Services service)
        {
            switch (service)
            {
                case Services.Logger:
                    _service = "logger";
                    break;
            }

            return new Service();
        }

        public static T Post<T>(string uri, T model) where T : new()
        {
            using (var client = new HttpClient())
            {
                var content = JsonConvert.SerializeObject(model);
                var responseMessage = client.PostAsync(uri, new StringContent(content, Encoding.UTF8, "application/json")).GetAwaiter().GetResult();

                if (responseMessage.IsSuccessStatusCode)
                {
                    var responseData = responseMessage.Content.ReadAsStringAsync().GetAwaiter().GetResult();
                    var returnValue = JsonConvert.DeserializeObject<T>(responseData);

                    return returnValue;
                }
            }

            return new T();
        }

        public static void LogException(Exception ex)
        {
            var exception = Post(Make(Services.Logger).Call("postException"), new ExceptionModel
            {
                Date = DateTime.Now,
                Message = ex.Message,
                InnerException = (ex.InnerException != null) ? ex.InnerException.Message : string.Empty,
                Source = ex.Source,
                StackTrace = ex.StackTrace,
                TargetSite = ex.TargetSite.Name
            });
        }

        public static void LogAudit<T>(string uri, string responseData, string method) where T : new()
        {
            var audit = Post(Make(Services.Logger).Call("postAudit"), new AuditModel
            {
                Object = typeof(T).ToString(),
                Method = method,
                Action = uri,
                Value = responseData,
                Date = DateTime.Now
            });
        }
    }

    public enum Services
    {
        Logger
    }
}