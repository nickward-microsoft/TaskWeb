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

}