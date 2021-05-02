using Newtonsoft.Json.Linq;
using System.Linq;
using BuffettCodeCommon.Exception;
using BuffettCodeAPIClient.Config;

namespace BuffettCodeAPIClient
{
    /// <summary>
    /// バフェットコードWebAPIのレスポンスのバリデーションロジック
    /// </summary>
    public class ApiResponseValidator
    {
        /// <summary>
        /// APIのレスポンスをチェックし、問題があれば<see cref="BuffettCodeException"/>をthrowします。
        /// </summary>
        /// <param name="json">レスポンスのパース結果</param>
        public static void Validate(JObject json)
        {
            var message = json.Children().Where(t => t is JToken).Cast<JToken>().Where(t => t.Path.Equals("message")).ToArray();
            if (message.Count() > 0 && message.First().ToString().Contains(ApiErrorMessageConfig.TEST_API_CONSTRAINT))
            {
                throw new TestAPIConstraintException();
            }
        }

    }
}
