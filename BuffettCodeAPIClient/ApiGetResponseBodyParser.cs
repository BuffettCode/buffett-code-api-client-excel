using Newtonsoft.Json.Linq;

namespace BuffettCodeAPIClient
{

    public class ApiGetResponseBodyParser
    {
        public static JObject Parse(string contentString)
        {
            var jObject = JObject.Parse(contentString);
            ApiGetResponseValidator.Validate(jObject);
            return jObject;
        }

    }
}