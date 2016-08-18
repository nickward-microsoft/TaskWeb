using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using System.Web.Configuration;

namespace TaskWeb.Helpers
{
    internal class APIMHelper
    {
        private static string apimKey = WebConfigurationManager.AppSettings["APIMKey"];
        private static string apimEndpoint = WebConfigurationManager.AppSettings["APIMEndpoint"];

        internal static HttpClient NewAPIMHttpClient()
        {
            var client = new HttpClient();
            client.BaseAddress = new Uri(apimEndpoint);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.Add("Ocp-Apim-Trace", "true");
            client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", apimKey);
            return client;
        }
    }
}