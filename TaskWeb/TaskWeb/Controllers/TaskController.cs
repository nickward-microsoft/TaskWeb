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
    }
}