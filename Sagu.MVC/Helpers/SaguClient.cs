using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Sagu.MVC
{
    public static class SaguClient
    {
        public static HttpClient GetClient()
        {
            var client = new HttpClient();

            client.BaseAddress = new Uri(ConfigurationManager.AppSettings["Sagu.API.Uri"]);

            return client;
        }
    }
}
