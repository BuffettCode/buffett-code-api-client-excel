using BuffettCodeCommon;
using BuffettCodeCommon.Validator;

namespace BuffettCodeAddinRibbon.Settings
{
    public class AddinSettings
    {
        private readonly string apiKey;
        private readonly bool enabledOndemandEndpoint;
        private readonly bool debugMode;
        private readonly bool forceOndemandEndpoint;

        public string ApiKey => apiKey;
        public bool IsOndemandEndpointEnabled => enabledOndemandEndpoint;
        public bool DebugMode => debugMode;

        public bool IsForceOndemandEndpoint => forceOndemandEndpoint;

        private AddinSettings(string apiKey, bool useOndemandEndpoint, bool debugMode, bool forceOndemandEndpoint)
        {
            this.apiKey = apiKey;
            this.enabledOndemandEndpoint = useOndemandEndpoint;
            this.debugMode = debugMode;
            this.forceOndemandEndpoint = forceOndemandEndpoint;
        }

        public static AddinSettings Create(string apiKey, bool useOndemandEndpoint, bool debugMode, bool forceOndemandEndpoint)
        {
            ApiKeyValidator.Validate(apiKey);
            return new AddinSettings(apiKey, useOndemandEndpoint, debugMode, forceOndemandEndpoint);
        }

        public static AddinSettings Create(Configuration config)
        {
            return new AddinSettings(config.ApiKey, config.IsOndemandEndpointEnabled, config.DebugMode, config.IsForceOndemandApi);
        }

        public void SaveToConfiguration(Configuration config)
        {
            config.ApiKey = apiKey;
            // if ondemand endpoint is disabled, set force ondemand as false
            if (enabledOndemandEndpoint)
            {
                config.IsOndemandEndpointEnabled = true;
                config.IsForceOndemandApi = forceOndemandEndpoint;
            }
            else
            {
                config.IsOndemandEndpointEnabled = false;
                config.IsForceOndemandApi = false;
            }
            config.DebugMode = debugMode;
        }


    }
}
