using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Sagu.DTO;

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

        public static string GetUri(string controller, string id)
        {
            return string.Format("{0}{1}/{2}", ConfigurationManager.AppSettings["Sagu.API.Uri"], controller, id);
        }

        public static string GetImage(AreaImage image)
        {
            return GetUri("images", image.Id + "." + image.FileName.Split('.').Last());
        }
    }
}
