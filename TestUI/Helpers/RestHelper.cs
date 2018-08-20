using System.Collections.Generic;
using System.Net;
using Newtonsoft.Json;
using RestSharp;
using RestResponse = TestUI.Rest.Models.RestResponse;
using TestUI.Rest.Models;

namespace TestUI.Rest
{
    public class RestHelper
    {
        private string Url { get; }
        private string EndPoint { get; }
        private Method Method { get; }
        private List<RestParameter> Parameters { get; }
        private List<RestParameter> UrlParameters { get; }
        private List<RestParameter> HeaderParameters { get; }
        public object Body { get; set; }

        public RestHelper(string url, string endPoint, Method method)
        {
            Url = url;
            EndPoint = endPoint;
            Method = method;

            Parameters = new List<RestParameter>();
            UrlParameters = new List<RestParameter>();
            HeaderParameters = new List<RestParameter>();
        }

        public void AddParameter(string name, object value)
        {
            Parameters.Add(new RestParameter(name, value));
        }

        public void AddParameters(List<RestParameter> parameters)
        {
            Parameters.AddRange(parameters);
        }

        public void AddUrlParameter(string name, object value)
        {
            UrlParameters.Add(new RestParameter(name, value));
        }

        public void AddUrlParameters(List<RestParameter> parameters)
        {
            UrlParameters.AddRange(parameters);
        }

        public void AddHeaderParameter(string name, object value)
        {
            HeaderParameters.Add(new RestParameter(name, value));
        }

        public void AddHeaderParameters(List<RestParameter> parameters)
        {
            HeaderParameters.AddRange(parameters);
        }

        public RestResponse Send()
        {
            ServicePointManager.ServerCertificateValidationCallback += (sender, certificate, chain, sslPolicyErrors) => true;
            var client = new RestClient(Url);
            var request = new RestRequest(EndPoint, Method)
            {
                RequestFormat = DataFormat.Json,

            };

            request.AddBody(Body);

            foreach (var parameter in Parameters)
                request.AddParameter(parameter.Name, parameter.Value);

            foreach (var parameter in UrlParameters)
                request.AddUrlSegment(parameter.Name, parameter.Value);

            foreach (var parameter in HeaderParameters)
                request.AddHeader(parameter.Name, parameter.Value.ToString());

            IRestResponse response = client.Execute(request);

            return new RestResponse
            {
                StatusCode = response.StatusCode,
                IsSuccessful = response.IsSuccessful,
                Content = response.Content // JsonConvert.DeserializeObject<dynamic>(response.Content)
            };
        }
    }
}