using BuffettCodeCommon.Config;
using BuffettCodeCommon.Exception;
using BuffettCodeCommon.Registry;
using BuffettCodeCommon.Validator;

namespace BuffettCodeCommon
{
    /// <summary>
    /// Excelアドイン設定
    /// </summary>
    /// <remarks>
    /// バフェットコードのExcelアドインでは設定をレジストリで管理します。
    /// レジストリへの入出力をこのクラスで行います。
    /// </remarks>
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
        /// 最大同時実行数のデフォルト値
        /// </summary>
        private static readonly int MaxDegreeOfParallelismDefault = 8;


        /// <summary>
        /// デフォルトでは Ondemand Endpoint は利用不可
        /// </summary>
        private static readonly bool UseOndemandEndpointDefault = false;

        /// <summary>
        /// デフォルトでは DebugMode は false
        /// </summary>
        private static readonly bool DebugModeDefault = false;


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

        /// <summary>
        /// APIコールの最大同時実行数
        /// </summary>
        public int MaxDegreeOfParallelism
        {
            get => (int)registryAccessor.GetRegistryValue(BuffettCodeRegistryConfig.NameMaxDegreeOfParallelism, MaxDegreeOfParallelismDefault);
            set => registryAccessor.SaveRegistryValue(BuffettCodeRegistryConfig.NameMaxDegreeOfParallelism, value);
        }

        // Registryには"True" "False" の文字列で書き込まれる
        private bool IsTrue(string name, bool defaultValue) => registryAccessor.GetRegistryValue(name, defaultValue).ToString().Equals(true.ToString());

        /// <summary>
        /// Ondemand Endpoint の利用可否
        /// </summary>
        public bool UseOndemandEndpoint
        {
            get => IsTrue(BuffettCodeRegistryConfig.NameUseOndemandEndpoint, UseOndemandEndpointDefault);
            set => registryAccessor.SaveRegistryValue(BuffettCodeRegistryConfig.NameUseOndemandEndpoint, value);
        }


        /// <summary>
        /// デバッグモード
        /// </summary>
        public bool DebugMode
        {
            get => IsTrue(BuffettCodeRegistryConfig.NameDebugMode, DebugModeDefault);
            set => registryAccessor.SaveRegistryValue(BuffettCodeRegistryConfig.NameDebugMode, value);
        }

        public string KeyName => registryAccessor.KeyName;
    }
}