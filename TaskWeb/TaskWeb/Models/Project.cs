using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using System.Web.Configuration;

namespace TaskWeb.Models
{
    public class Project
    {
        public string Name { get; set; }
        public List<int> TaskIdList { get; set; }
        public string Status { get; set; }
    }

    public class ProjectManager
    {
        private List<Project> projectList = new List<Project>();
        public IEnumerable<Project> ProjectList { get { return projectList; } }

        private string apimKey = WebConfigurationManager.AppSettings["APIMKey"];
        private string apimEndpoint = WebConfigurationManager.AppSettings["APIMEndpoint"];
        
        public async System.Threading.Tasks.Task RefreshProjectList()
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(apimEndpoint);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Add("Ocp-Apim-Trace", "true");
                client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", apimKey);

                HttpResponseMessage response = await client.GetAsync("project");
                if (response.IsSuccessStatusCode)
                {
                    projectList = await response.Content.ReadAsAsync<List<Project>>();
                }
            }
        }

        public async System.Threading.Tasks.Task AddProjectAsync(Project newProject)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(apimEndpoint);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Add("Ocp-Apim-Trace", "true");
                client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", apimKey);

                HttpResponseMessage response = await client.PostAsJsonAsync(String.Concat("project/",newProject.Name), newProject);
                if (response.IsSuccessStatusCode)
                {
                    return;
                    //taskList.Add(newTask);
                }
            }
        }

    }
}