using BuffettCodeAddin;

namespace Buffett
{
    class BuffettCodeTestUtils
    {
        public static string GetValidApiKey()
        {
            Configuration.Reload();
            return Configuration.ApiKey;
        }

        public static string GetInvalidApiKey()
        {
            return "invalidApiKey!!!";
        }
    }
}
