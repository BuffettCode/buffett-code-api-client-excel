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

        private AddinSettings(string apiKey, bool useOndemandEndpoint, uint maxDegreeOfParallelism, bool debugMode)
        {
            this.apiKey = apiKey;
            this.enabledOndemandEndpoint = useOndemandEndpoint;
            this.maxDegreeOfParallelism = maxDegreeOfParallelism;
            this.debugMode = debugMode;
        }

        public static AddinSettings Create(string apiKey, bool useOndemandEndpoint, uint maxDegreeOfParallelism, bool debugMode)
        {
            ApiKeyValidator.Validate(apiKey);
            return new AddinSettings(apiKey, useOndemandEndpoint, maxDegreeOfParallelism, debugMode);
        }

        public static AddinSettings Create(Configuration config)
        {
            return new AddinSettings(config.ApiKey, config.IsOndemandEndpointEnabled, config.MaxDegreeOfParallelism, config.DebugMode);
        }

        public void SaveToConfiguration(Configuration config)
        {
            config.ApiKey = apiKey;
            config.IsOndemandEndpointEnabled = enabledOndemandEndpoint;
            config.MaxDegreeOfParallelism = maxDegreeOfParallelism;
            config.DebugMode = debugMode;
        }


    }
}
