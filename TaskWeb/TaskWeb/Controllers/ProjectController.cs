using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TaskWeb.Models;

namespace TaskWeb.Controllers
{
    public class ProjectController : Controller
    {
        ProjectManager _projectManager = new ProjectManager();

        // GET: Project
        public async System.Threading.Tasks.Task<ActionResult> Index()
        {
            await _projectManager.RefreshProjectList();
            return View(_projectManager.ProjectList);
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
                await _projectManager.AddProjectAsync(ProjectToAdd);
                return RedirectToAction("Index");
            }

            return PartialView("AddProject", ProjectToAdd);
        }
    }
}