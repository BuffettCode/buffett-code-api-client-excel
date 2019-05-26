using Microsoft.Win32;

namespace BuffettCodeAddin
{
    public class Configuration
    {
        private static string apiKey;
        private static bool clearCache;
        private static string REGISTRY_KEY = @"Software\BuffettCode";

        public static string ApiKey
        {
            get { return apiKey; }
            set
            {
                SaveRegistry("ApiKey", value);
                apiKey = value;
            }
        }

        public static bool ClearCache
        {
            get { return clearCache; }
            set
            {
                SaveRegistry("ClearCache", value ? 1 : 0);
                clearCache = value;
            }
        }

        public static void Reload()
        {
            ReloadAllValuesFromRegistry();
        }

        private static void ReloadAllValuesFromRegistry()
        {
            RegistryKey registryKey = Registry.CurrentUser.OpenSubKey(REGISTRY_KEY, false);
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
            registryKey.Close();
        }

        private static void SaveRegistry(string key, object value)
        {
            RegistryKey registryKey = Registry.CurrentUser.CreateSubKey(REGISTRY_KEY);
            registryKey.SetValue(key, value);
            registryKey.Close();

        }

        public static RegistryKey GetMonitoringRegistryKey()
        {
            return Registry.CurrentUser.CreateSubKey(REGISTRY_KEY);

        }
    }
}
