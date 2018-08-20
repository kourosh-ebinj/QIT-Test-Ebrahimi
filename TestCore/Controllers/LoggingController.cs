using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TestAPI.Filters;

namespace TestAPI.Controllers
{
    [ValidateModel]
    [Route("api/[controller]")]
    public class LoggingController : Controller
    {
        //public static Common.Logger logger = new Common.Logger();

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost("LogError")]
        // GET: Classes/5
        //string message, NLog.LogLevel type, object logData = null, Dictionary<string, string> parameters = null
        public IActionResult LogError([FromBody] string message, [FromBody] object logData)
        {
            if (string.IsNullOrWhiteSpace(message)) return BadRequest("message parameter is empty.");


            Common.Logger.Log(message, NLog.LogLevel.Error, logData);

            return NoContent();
        }
    }
}