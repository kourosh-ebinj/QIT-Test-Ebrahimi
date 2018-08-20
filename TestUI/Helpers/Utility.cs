using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace TestUI.Helpers
{
    public enum MessageType
    {
        Success,
        Info,
        Warning,
        Error
    }

    public class PageMessage
    {
        public string Message { get; set; }
        public MessageType MessageType { get; set; }
    }

    public class Utility
    {
        //public const string const_UnSpecifiedItem = "نامعلوم";
        public const string const_MessagesKey = "Messages";
        public const string const_ClassesData = "classes";
        //public const string const_PagingObject = "Paging";
        public const int const_PagingSize = 15;
        public const string const_TimeFormat = "H:mm:ss";
        public const string const_DateFormat = "yyyy-MM-dd";

        public static void AddMessageToPage(Controller controller, string message, MessageType messageType = MessageType.Info)
        {
            List<PageMessage> messages = null;
            if (controller.TempData.ContainsKey(const_MessagesKey))
                messages = (List<PageMessage>)controller.TempData[const_MessagesKey];
            else
                messages = new List<PageMessage>();

            messages.Add(new PageMessage() { Message = message.Trim(), MessageType = messageType });
            if (controller.TempData.ContainsKey(const_MessagesKey))
                controller.TempData[const_MessagesKey] = JsonConvert.SerializeObject( messages);
            else
                controller.TempData.Add(const_MessagesKey, JsonConvert.SerializeObject(messages));

        }

        //public static string GetBaseUrl(HttpContext context, bool isAbsolute = false)
        //{
        //    var url = UrlHelper.GenerateContentUrl("~", context);
        //    if (isAbsolute) url = context.Request.Url.Scheme + "://" + context.Request.Url.DnsSafeHost + url;
        //    return url;
        //}

        public static string GetCurrentActionName(ControllerBase controller)
        {
            return controller.ControllerContext.RouteData.Values["action"].ToString();
        }
        public static string GetCurrentControllerName(ControllerBase controller)
        {
            return controller.ControllerContext.RouteData.Values["controller"].ToString();
        }

        public static string GetAPIUrl()
        {
            var config = InitConfiguration();
            return config["APIUrl"].Trim('/');
        }

        public static IConfiguration InitConfiguration()
        {
            var config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();
            return config;
        }
    }
}
