using BuffettCodeCommon.Config;
using BuffettCodeCommon.Exception;
namespace BuffettCodeIO.Parser
{
    class ApiResponseParserFactory
    {
        public static IApiResponseParser Create(BuffettCodeApiVersion version)
        {
            switch (version)
            {
                case BuffettCodeApiVersion.Version2:
                    return new ApiV2ResponseParser();
                default:
                    throw new ApiResponseParserException($"api version={version} is not supported.");
            }

        }
    }
}