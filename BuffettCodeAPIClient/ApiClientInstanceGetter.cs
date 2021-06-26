using BuffettCodeCommon.Config;
using BuffettCodeCommon.Validator;
using System;

namespace BuffettCodeAPIClient
{
    public class ApiClientInstanceGetter
    {
        public static IBuffettCodeApiClient Get(BuffettCodeApiVersion version, string apiKey)
        {
            ApiKeyValidator.Validate(apiKey);
            switch (version)
            {
                case BuffettCodeApiVersion.Version2:
                    return BuffettCodeApiV2Client.GetInstance(apiKey);
                case BuffettCodeApiVersion.Version3:
                    return BuffettCodeApiV3Client.GetInstance(apiKey);
                default:
                    throw new ArgumentException($"unknown version is given: {version}");
            }
        }

    }
}