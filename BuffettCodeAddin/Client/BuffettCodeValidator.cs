using Newtonsoft.Json.Linq;
using System.Linq;

namespace BuffettCodeAddin.Client
{
    /// <summary>
    /// バフェットコードWebAPIのレスポンスのバリデーションロジック
    /// </summary>
    class BuffettCodeValidator
    {
        /// <summary>
        /// APIのレスポンスをチェックし、問題があれば<see cref="BuffettCodeException"/>をthrowします。
        /// </summary>
        /// <param name="json">レスポンスのパース結果</param>
        public static void Validate(JObject json)
        {
            var message = json.Children().Where(t => t is JToken).Cast<JToken>().Where(t => t.Path.Equals("message")).ToArray();
            if (message.Count() > 0 && message.First().ToString().Contains("Testing Apikey is only allowed to ticker ending with"))
            {
                throw new TestAPIConstraintException();
            }
        }

    }
}
