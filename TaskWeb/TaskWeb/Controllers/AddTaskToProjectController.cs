using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using TaskWeb.Helpers;
using TaskWeb.Models;

namespace TaskWeb.Controllers
{
    public class AddTaskToProjectController : Controller
    {
        private ProjectTask pt = new ProjectTask();

        // GET: AddTaskToProject
        public async System.Threading.Tasks.Task<ActionResult> Index(string projectName)
        {
            await this.RefreshProjectTask(projectName);
            return View(pt);
        }

        private async System.Threading.Tasks.Task RefreshProjectTask(string projectName)
        {
            using (var client = APIMHelper.NewAPIMHttpClient())
            {
                HttpResponseMessage response = await client.GetAsync(String.Concat("project/", projectName));
                pt.project = await response.Content.ReadAsAsync<Project>();
                response = await client.GetAsync("task");
                pt.allTaskList = await response.Content.ReadAsAsync<List<Task>>();
            }
        }

        public async System.Threading.Tasks.Task<ActionResult> AddTask(string projectName, int TaskId)
        {
            await this.RefreshProjectTask(projectName);
            using (var client = APIMHelper.NewAPIMHttpClient())
            {
                var method = new HttpMethod("PUT");
                string apiOperationString = String.Concat("project/", pt.project.Name, "/", TaskId.ToString());

                var request = new HttpRequestMessage(method, apiOperationString);
                var response = await client.SendAsync(request);
            }
            return View("Index", pt);
        }
    }
}