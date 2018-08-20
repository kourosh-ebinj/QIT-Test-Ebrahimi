using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Common.Models;
using TestUI.Rest;
using RestSharp;
using Newtonsoft.Json;

namespace TestUI.Controllers
{
    public class HomeController : Controller
    {
        public const string const_Classes = "classes";
        public const string const_FetchingData_Message = "Please wait ...";

        
        public IActionResult Index()
        {
            var rest = new RestHelper(TestUI.Helpers.Utility.GetAPIUrl(), "/classes", Method.GET);
            var response = rest.Send();

            var classes = JsonConvert.DeserializeObject<List<ClassModel>>(response.Content);
            ViewData.Add(const_Classes,classes);

            return View(classes);
        }


        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
