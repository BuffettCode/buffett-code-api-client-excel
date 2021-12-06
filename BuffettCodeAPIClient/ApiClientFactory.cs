using BuffettCodeCommon;
using BuffettCodeCommon.Config;
using BuffettCodeCommon.Exception;
using BuffettCodeCommon.Validator;


namespace BuffettCodeAPIClient
{
    public class ApiClientFactory
    {
        private static BuffettCodeApiV2Client CreateV2(string apiKey)
        {
            var apiClientCore = ApiClientCoreWithCache.Create(
                apiKey,
                BuffettCodeApiV2Config.BASE_URL,
                BuffettCodeAddinCache.GetInstance()
            );
            return new BuffettCodeApiV2Client(apiClientCore);
        }

        private static BuffettCodeApiV3Client CreateV3(string apiKey)
        {
            var apiClientCore = ApiClientCoreWithCache.Create(
                apiKey,
                BuffettCodeApiV3Config.BASE_URL,
                BuffettCodeAddinCache.GetInstance()
            );
            return new BuffettCodeApiV3Client(apiClientCore);
        }


        public static IBuffettCodeApiClient Create(BuffettCodeApiVersion version, string apiKey)
        {
            ApiKeyValidator.Validate(apiKey);
            switch (version)
            {
                case BuffettCodeApiVersion.Version2:
                    return CreateV2(apiKey);
                case BuffettCodeApiVersion.Version3:
                    return CreateV3(apiKey);
                default:
                    throw new NonSupportedApiVersionException($"unknown version is given: {version}");
            }
        }

    }
}