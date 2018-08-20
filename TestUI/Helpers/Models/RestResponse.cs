using System.Net;

namespace TestUI.Rest.Models
{
    public class RestResponse
    {
        public RestResponse()
        {
            StatusCode = 0;
            IsSuccessful = false;
            Content = ""; // null;
        }
        
        public HttpStatusCode StatusCode { get; set; }
        public bool IsSuccessful { get; set; }
        public string Content { get; set; }
    }
}