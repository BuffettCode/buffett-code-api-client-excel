using BuffettCodeCommon.Exception;
using System.Text.RegularExpressions;
namespace BuffettCodeCommon.Validator
{
    public static class ApiKeyValidator
    {
        // api token: half-width alphanumeric, length = 40
        /// <summary>
        /// Defines the API_KEY_PATTTERN.
        /// </summary>
        private readonly static Regex ApiKeyPattern = new Regex(@"^([a-zA-Z0-9]{40})$", RegexOptions.Compiled);

        public static void Validate(string maybeApiKey)
        {
            if (!ApiKeyPattern.IsMatch(maybeApiKey))
            {
                throw new ValidationError($"api key must be half-width alphanumeric, length = 40");
            }
        }

    }
}