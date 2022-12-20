using System.Collections.Generic;

namespace BuffettCodeAPIClient
{
    public class ApiGetRequest
    {
        public string EndPoint { get; }
        public Dictionary<string, string> Parameters { get; }

        public ApiGetRequest(string endPoint, Dictionary<string, string> parameters)
        {
            EndPoint = endPoint;
            Parameters = parameters;
        }

        public override string ToString() => $"EndPoint::{EndPoint}, Parameters::{string.Join(", ", Parameters)}";
    }
}