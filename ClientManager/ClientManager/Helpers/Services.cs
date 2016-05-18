using ClientManager.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace ClientManager.Helpers
{
    public enum Services
    {
        Client,
        Address,
        Property,
        Logger,
        Matching
    }

    public class Service
    {
        private static string _service;

        public static Service Get(Services service)
        {
            switch (service)
            {
                case Services.Client:
                    _service = "client";
                    break;

                case Services.Address:
                    _service = "address";
                    break;

                case Services.Property:
                    _service = "property";
                    break;

                case Services.Logger:
                    _service = "logger";
                    break;

                case Services.Matching:
                    _service = "matching";
                    break;

                default:
                    _service = "client";
                    break;
            }

            return new Service();
        }

        public static List<T> GetList<T>(string uri)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    var responseMessage = client.GetAsync(uri).GetAwaiter().GetResult();
                    if (responseMessage.IsSuccessStatusCode)
                    {
                        var responseData = responseMessage.Content.ReadAsStringAsync().GetAwaiter().GetResult();
                        var list = JsonConvert.DeserializeObject<List<T>>(responseData);

                        return list;
                    }
                }
            }
            catch (Exception ex)
            {
                LogException(ex);
            }

            return new List<T>();
        }

        public static T GetItem<T>(string uri) where T : new()
        {
            try
            {
                using (var client = new HttpClient())
                {
                    var responseMessage = client.GetAsync(uri).GetAwaiter().GetResult();
                    if (responseMessage.IsSuccessStatusCode)
                    {
                        var responseData = responseMessage.Content.ReadAsStringAsync().GetAwaiter().GetResult();
                        var item = JsonConvert.DeserializeObject<T>(responseData);

                        return item;
                    }
                }
            }
            catch (Exception ex)
            {
                LogException(ex);
            }

            return new T();
        }

        public static T Post<T>(string uri, T model) where T : new()
        {
            try
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
            }
            catch (Exception ex)
            {
                LogException(ex);
            }

            return new T();
        }

        public static bool Update<T>(string uri, T model) where T : new()
        {
            try
            {
                using (var client = new HttpClient())
                {
                    var content = JsonConvert.SerializeObject(model);
                    var responseMessage = client.PutAsync(uri, new StringContent(content, Encoding.UTF8, "application/json")).GetAwaiter().GetResult();

                    if (responseMessage.StatusCode == System.Net.HttpStatusCode.NoContent)
                    {
                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                LogException(ex);
            }

            return false;
        }

        public static bool Delete(string uri)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    var responseMessage = client.DeleteAsync(uri).GetAwaiter().GetResult();

                    if (responseMessage.StatusCode == System.Net.HttpStatusCode.NoContent)
                    {
                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                LogException(ex);
            }

            return false;
        }

        public string Uri(int? value = null)
        {
            using (var client = new HttpClient())
            {
                var responseMessage = client.GetAsync("http://localhost:8086/api/Services/" + _service).GetAwaiter().GetResult();
                if (responseMessage.IsSuccessStatusCode)
                {
                    var responseData = responseMessage.Content.ReadAsStringAsync().GetAwaiter().GetResult();
                    var item = JsonConvert.DeserializeObject<string>(responseData);

                    return (value.HasValue) ? item + value : item;                  
                }
            }

            return string.Empty;
        }

        public string Call(string value = null)
        {
            using (var client = new HttpClient())
            {
                var responseMessage = client.GetAsync("http://localhost:8086/api/Services/" + _service).GetAwaiter().GetResult();
                if (responseMessage.IsSuccessStatusCode)
                {
                    var responseData = responseMessage.Content.ReadAsStringAsync().GetAwaiter().GetResult();
                    var item = JsonConvert.DeserializeObject<string>(responseData);

                    return (!string.IsNullOrEmpty(value)) ? item + value : item;
                }
            }

            return string.Empty;
        }

        public static Service Make(Services service)
        {
            return Get(service);
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
}