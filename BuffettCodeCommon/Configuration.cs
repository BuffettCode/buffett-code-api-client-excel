using BuffettCodeCommon.Config;
using BuffettCodeCommon.Exception;
using BuffettCodeCommon.Registry;
using BuffettCodeCommon.Validator;

namespace BuffettCodeCommon
{
    public class Configuration
    {
        private readonly BuffettCodeRegistryAccessor registryAccessor;

        // todo refactoring: build の context (release/debug) でハンドリングするべき
        private static readonly Configuration configration = new Configuration(BuffettCodeRegistryConfig.SubKeyBuffettCodeExcelAddinRelease);
        private static readonly Configuration devConfigration = new Configuration(BuffettCodeRegistryConfig.SubKeyBuffettCodeExcelAddinDev);

        protected Configuration(string keyName)
        {
            registryAccessor = BuffettCodeRegistryAccessor.Create(keyName);
            // delete old unsupported values 
            registryAccessor.DeleteUnSupportedValues();
        }

        public static Configuration GetInstance(bool isDev = false)
        {
            return isDev ? devConfigration : configration;
        }


        /// <summary>
        /// デフォルトでは Test 用のAPI KEY
        /// </summary>
        private static readonly string ApiKeyDefault = BuffettCodeApiKeyConfig.TestApiKey;

        /// <summary>
        /// デフォルトでは Ondemand Endpoint は利用不可
        /// </summary>
        private static readonly bool IsOndemandEndpointEnabledDefault = false;

        /// <summary>
        /// デフォルトでは DebugMode は false
        /// </summary>
        private static readonly bool IsDebugModeDefault = false;

        /// <summary>
        /// デフォルトでは ForceOndemandApiEnabled は false
        /// </summary>
        private static readonly bool IsForceOndemandApiEnabledDefault = false;

        /// <summary>
        /// バフェットコードのAPIキー
        /// </summary>
        public string ApiKey
        {
            get => (string)registryAccessor.GetRegistryValue(BuffettCodeRegistryConfig.NameApiKey, ApiKeyDefault);

            set
            {
                try
                {
                    ApiKeyValidator.Validate(value);
                }
                catch (ValidationError e)
                {
                    throw new AddinConfigurationException(e.Message);
                }
                registryAccessor.SaveRegistryValue(BuffettCodeRegistryConfig.NameApiKey, value);
            }

        }

        // Registryには"True" "False" の文字列で書き込まれる
        private bool IsTrue(string name, bool defaultValue) => registryAccessor.GetRegistryValue(name, defaultValue).ToString().Equals(true.ToString());

        /// <summary>
        /// Ondemand Endpoint の利用可否
        /// </summary>
        public bool IsOndemandEndpointEnabled
        {
            get => IsTrue(BuffettCodeRegistryConfig.NameIsOndemandEndpointEnabled, IsOndemandEndpointEnabledDefault);
            set
            {
                if (value is false && IsForceOndemandApiEnabled)
                {
                    throw new AddinConfigurationException("set ForceOndemandApiEnabled as false at first.");
                }
                registryAccessor.SaveRegistryValue(BuffettCodeRegistryConfig.NameIsOndemandEndpointEnabled, value);
            }
        }

        /// <summary>
        /// デバッグモード
        /// </summary>
        public bool DebugMode
        {
            get => IsTrue(BuffettCodeRegistryConfig.NameDebugMode, IsDebugModeDefault);
            set => registryAccessor.SaveRegistryValue(BuffettCodeRegistryConfig.NameDebugMode, value);
        }

        /// <summary>
        /// オンデマンドモードを強制する
        /// </summary>
        public bool IsForceOndemandApiEnabled
        {
            get => IsTrue(BuffettCodeRegistryConfig.NameForceOndemandApiEnabled, IsForceOndemandApiEnabledDefault);
            set
            {
                if (value is true && !IsOndemandEndpointEnabled)
                {
                    throw new AddinConfigurationException("set IsOndemandApiEnabled as true at first.");
                }
                registryAccessor.SaveRegistryValue(BuffettCodeRegistryConfig.NameForceOndemandApiEnabled, value);
            }
        }

        public string KeyName => registryAccessor.KeyName;

    }
}