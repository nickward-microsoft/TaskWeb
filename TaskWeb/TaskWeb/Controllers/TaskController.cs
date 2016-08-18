using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using TaskWeb.Helpers;
using TaskWeb.Models;

namespace TaskWeb.Controllers
{
    public class TaskController : Controller
    {

        // GET: Task
        public async Task<ActionResult> Index()
        {
            var taskList = await GetTasks();
            return View(taskList);
        }

        private async System.Threading.Tasks.Task<List<Models.Task>> GetTasks()
        {
            var taskList = new List<Models.Task>();

            using (var client = APIMHelper.NewAPIMHttpClient())
            {
                HttpResponseMessage response = await client.GetAsync("task");
                if (response.IsSuccessStatusCode)
                {
                    taskList = await response.Content.ReadAsAsync<List<Models.Task>>();
                }
            }

            return taskList;
        }

        // GET: Complete
        public async Task<ActionResult> Complete(int TaskId)
        {
            using (var client = APIMHelper.NewAPIMHttpClient() )
            {
                var method = new HttpMethod("PATCH");
                string apiOperationString = String.Concat("task/complete/", TaskId.ToString());
                var request = new HttpRequestMessage(method, apiOperationString);
                var response = await client.SendAsync(request);
            }

            var taskList = await GetTasks();
            return View("Index", taskList);
        }

        // GET: Delete
        public async Task<ActionResult> Delete(int TaskId)
        {
            using (var client = APIMHelper.NewAPIMHttpClient())
            {
                var method = new HttpMethod("DELETE");
                string apiOperationString = String.Concat("task/", TaskId.ToString());
                var request = new HttpRequestMessage(method, apiOperationString);
                var response = await client.SendAsync(request);
            }

            var taskList = await GetTasks();
            return View("Index", taskList);
        }

        // GET: AddTask
        public async Task<ActionResult> AddTask()
        {
            return PartialView("AddTask", new TaskWeb.Models.Task());
        }

        // POST: AddTask
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> AddTask(TaskWeb.Models.Task TaskToAdd)
        {
            if (ModelState.IsValid)
            {
                using (var client = APIMHelper.NewAPIMHttpClient())
                {
                    HttpResponseMessage response = await client.PostAsJsonAsync("task", TaskToAdd);
                }
                return RedirectToAction("Index");
            }

            return PartialView("AddTask", TaskToAdd);
        }
    }
}