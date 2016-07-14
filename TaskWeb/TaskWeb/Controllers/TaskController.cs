using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using TaskWeb.Models;

namespace TaskWeb.Controllers
{
    public class TaskController : Controller
    {

        TaskManager _tm = new TaskManager();

        // GET: Task
        public async Task<ActionResult> Index()
        {

            await _tm.RefreshTasksAsync();
            
            return View(_tm.Tasks);
        }

        // GET: Complete
        public async Task<ActionResult> Complete(int TaskId)
        {
            await _tm.CompleteTaskAsync(TaskId);

            return View();
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
                await _tm.AddTaskAsync(TaskToAdd);
                return RedirectToAction("Index");
            }

            return PartialView("AddTask", TaskToAdd);
        }
    }
}