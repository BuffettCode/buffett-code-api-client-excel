using BuffettCodeCommon.Config;
using BuffettCodeCommon.Exception;
namespace BuffettCodeIO.Parser
{
    public class ApiResponseParserFactory
    {
        public static IApiResponseParser Create(BuffettCodeApiVersion version)
        {
            switch (version)
            {
                case BuffettCodeApiVersion.Version3:
                    return new ApiV3ResponseParser();
                default:
                    throw new NonSupportedApiVersionException($"api version={version} is not supported.");
            }

        }
    }
}