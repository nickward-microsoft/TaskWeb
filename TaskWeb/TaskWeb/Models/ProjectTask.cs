using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TaskWeb.Models
{
    public class ProjectTask
    {
        public Project project { get; set; }
        public List<Task> allTaskList { get; set; }
    }
}