using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web.Configuration;

namespace TaskWeb.Models
{
    public class TaskManager
    {
        private List<Task> taskList = new List<Task>();
        public IEnumerable<Task> Tasks { get { return taskList; } }

        private string hectagonapikey = WebConfigurationManager.AppSettings["hectagonapikey"];

        public async System.Threading.Tasks.Task RefreshTasksAsync()
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://hectagonapi.azure-api.net");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Add("Ocp-Apim-Trace", "true");
                client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", hectagonapikey);

                HttpResponseMessage response = await client.GetAsync("task");
                if (response.IsSuccessStatusCode)
                {
                    taskList = await response.Content.ReadAsAsync<List<Task>>();
                    //taskList.Add(newTask);
                }
            }
        }
    }
}