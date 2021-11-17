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
                case BuffettCodeApiVersion.Version2:
                    return new ApiV2ResponseParser();
                case BuffettCodeApiVersion.Version3:
                    return new ApiV3ResponseParser();
                default:
                    throw new NotSupportedDataTypeException($"api version={version} is not supported.");
            }

        }
    }
}