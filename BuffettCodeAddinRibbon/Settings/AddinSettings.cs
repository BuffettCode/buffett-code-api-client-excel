using BuffettCodeCommon;
using BuffettCodeCommon.Validator;

namespace BuffettCodeAddinRibbon.Settings
{
    public class AddinSettings
    {
        private readonly string apiKey;
        private readonly bool enabledOndemandEndpoint;
        private readonly uint maxDegreeOfParallelism;
        private readonly bool debugMode;

        public string ApiKey => apiKey;
        public bool IsOndemandEndpointEnabled => enabledOndemandEndpoint;
        public uint MaxDegreeOfParallelism => maxDegreeOfParallelism;
        public bool DebugMode => debugMode;

        private AddinSettings(string apiKey, bool useOndemandEndpoint, bool debugMode)
        {
            this.apiKey = apiKey;
            this.enabledOndemandEndpoint = useOndemandEndpoint;
            this.debugMode = debugMode;
        }

        public static AddinSettings Create(string apiKey, bool useOndemandEndpoint, uint maxDegreeOfParallelism, bool debugMode)
        {
            ApiKeyValidator.Validate(apiKey);
            return new AddinSettings(apiKey, useOndemandEndpoint, debugMode);
        }

        public static AddinSettings Create(Configuration config)
        {
            return new AddinSettings(config.ApiKey, config.IsOndemandEndpointEnabled, config.DebugMode);
        }

        public void SaveToConfiguration(Configuration config)
        {
            config.ApiKey = apiKey;
            config.IsOndemandEndpointEnabled = enabledOndemandEndpoint;
            config.DebugMode = debugMode;
        }


    }
}
