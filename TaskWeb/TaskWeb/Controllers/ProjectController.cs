using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;
using TaskWeb.Helpers;
using TaskWeb.Models;

namespace TaskWeb.Controllers
{
    public class ProjectController : Controller
    {
        private List<Project> projectList = new List<Project>();

        // GET: Project
        public async System.Threading.Tasks.Task<ActionResult> Index()
        {
            await GetProjects();
            return View(projectList);
        }

        private async System.Threading.Tasks.Task<List<Project>> GetProjects()
        {
            using (var client = APIMHelper.NewAPIMHttpClient())
            {
                HttpResponseMessage response = await client.GetAsync("project");
                if (response.IsSuccessStatusCode)
                {
                    projectList = await response.Content.ReadAsAsync<List<Project>>();
                }
            }

            return projectList;
        }

        public async System.Threading.Tasks.Task<ActionResult> Complete(string projectName)
        {
            using (var client = APIMHelper.NewAPIMHttpClient())
            {
                var method = new HttpMethod("PATCH");
                string apiOperationString = String.Concat("project/", projectName);
                var request = new HttpRequestMessage(method, apiOperationString);
                var response = await client.SendAsync(request);
            }

            await GetProjects();
            return View("Index", projectList);
        }

        public async System.Threading.Tasks.Task<ActionResult> Delete(string projectName)
        {
            using (var client = APIMHelper.NewAPIMHttpClient())
            {
                var method = new HttpMethod("DELETE");
                string apiOperationString = String.Concat("project/", projectName);
                var request = new HttpRequestMessage(method, apiOperationString);
                var response = await client.SendAsync(request);
            }

            await GetProjects();
            return View("Index", projectList);
        }

        // GET: AddTask
        public async System.Threading.Tasks.Task<ActionResult> AddProject()
        {
            return PartialView("AddProject", new TaskWeb.Models.Project());
        }

        // POST: AddTask
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async System.Threading.Tasks.Task<ActionResult> AddProject(TaskWeb.Models.Project ProjectToAdd)
        {
            if (ModelState.IsValid)
            {
                using (var client = APIMHelper.NewAPIMHttpClient())
                {
                    HttpResponseMessage response = await client.PostAsJsonAsync(String.Concat("project/", ProjectToAdd.Name), ProjectToAdd);
                }
                return RedirectToAction("Index");
            }

            return PartialView("AddProject", ProjectToAdd);
        }

        
    }
}