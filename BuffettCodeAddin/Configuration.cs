using Microsoft.Win32;

namespace BuffettCodeAddin
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
        private static string apiKey;

        private static bool clearCache;

        private static bool debugMode;

        /// <summary>
        /// バフェットコードの設定を格納するレジストリのキー名
        /// </summary>
        private static readonly string REGISTRY_KEY_NAME = @"Software\BuffettCode";

        /// <summary>
        /// バフェットコードのAPIキー
        /// </summary>
        public static string ApiKey
        {
            get { return apiKey; }
            set
            {
                SaveRegistry("ApiKey", value);
                apiKey = value;
            }
        }

        /// <summary>
        /// キャッシュクリアフラグ
        /// </summary>
        public static bool ClearCache
        {
            get { return clearCache; }
            set
            {
                SaveRegistry("ClearCache", value ? 1 : 0);
                clearCache = value;
            }
        }

        /// <summary>
        /// デバッグモード
        /// </summary>
        public static bool DebugMode
        {
            get { return debugMode; }
            set
            {
                SaveRegistry("DebugMode", value ? 1 : 0);
                debugMode = value;
            }
        }

        /// <summary>
        /// 全ての設定値をレジストリから読み直して最新化します。
        /// </summary>
        public static void Reload()
        {
            ReloadAllValuesFromRegistry();
        }

        private static void ReloadAllValuesFromRegistry()
        {
            RegistryKey registryKey = Registry.CurrentUser.OpenSubKey(REGISTRY_KEY_NAME, false);
            if (registryKey == null)
            {
                return;
            }

            object value = registryKey.GetValue("ApiKey");
            if (value != null)
            {
                apiKey = (string)value;
            } 
            value = registryKey.GetValue("ClearCache");
            if (value != null)
            {
                clearCache = (int)value == 0 ? false : true;
            }
            value = registryKey.GetValue("DebugMode");
            if (value != null)
            {
                debugMode = (int)value == 0 ? false : true;
            }
            registryKey.Close();
        }

        private static void SaveRegistry(string key, object value)
        {
            RegistryKey registryKey = Registry.CurrentUser.CreateSubKey(REGISTRY_KEY_NAME);
            registryKey.SetValue(key, value);
            registryKey.Close();

        }

        /// <summary>
        /// <see cref="RegistryMonitor"/>の監視対象とするレジストリキーを取得します。
        /// </summary>
        /// <returns></returns>
        public static RegistryKey GetMonitoringRegistryKey()
        {
            return Registry.CurrentUser.CreateSubKey(REGISTRY_KEY_NAME);
        }
    }
}
