using BuffettCodeCommon.Config;
using BuffettCodeCommon.Exception;
using Newtonsoft.Json.Linq;
using System.Linq;

namespace BuffettCodeAPIClient
{
    public class ApiGetResponseValidator
    {
        public static void Validate(JObject json)
        {
            var message = json.Children().Where(t => t is JToken).Cast<JToken>().Where(t => t.Path.Equals("message")).ToArray();
            if (message.Count() > 0 && message.First().ToString().Contains(ApiErrorMessageConfig.TEST_API_CONSTRAINT))
            {
                throw new TestAPIConstraintException(message.First().ToString());
            }
        }

    }
}