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
            using (var client = APIMHelper.NewAPIMHttpClient())
            {
                HttpResponseMessage response = await client.GetAsync("project");
                if (response.IsSuccessStatusCode)
                {
                    projectList = await response.Content.ReadAsAsync<List<Project>>();
                }
            }
            return View(projectList);
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