using BuffettCodeCommon.Exception;
using Microsoft.Win32;
using System.Linq;
namespace BuffettCodeCommon.Registry
{
    public class BuffettCodeRegistryAccessor
    {
        private readonly string subKey;

        private BuffettCodeRegistryAccessor(string subKey)
        {
            this.subKey = subKey;
        }

        private RegistryKey Open(bool writable) => Microsoft.Win32.Registry.CurrentUser.OpenSubKey(subKey, writable);

        private static void ValidateSubKey(string subKey)
        {
            if (!BuffettCodeRegistryConfig.SubKeys.Contains(subKey))
            {
                throw new AddinConfigurationException($"Unknown Registry SubKey={subKey}");
            }
        }

        private static void CreateSubKeyIfNotExist(string subKey)
        {
            ValidateSubKey(subKey);
            if (!Microsoft.Win32.Registry.CurrentUser.GetSubKeyNames().Contains(subKey))
            {
                Microsoft.Win32.Registry.CurrentUser.CreateSubKey(subKey);
            }
        }

        public static BuffettCodeRegistryAccessor Create(string subKey)
        {
            ValidateSubKey(subKey);
            CreateSubKeyIfNotExist(subKey);
            return new BuffettCodeRegistryAccessor(subKey);
        }

        public static void ValidValueName(string name)
        {
            if (!BuffettCodeRegistryConfig.SupportedValueNames.Contains(name))
            {
                throw new AddinConfigurationException($"unknown key::{name}");
            }
        }

        public void SaveRegistryValue(string name, object value)
        {
            ValidValueName(name);
            using (RegistryKey registryKey = Open(true))
            {
                if (value is null)
                {
                    registryKey.DeleteValue(name, false);
                }
                else
                {
                    registryKey.SetValue(name, value);
                }
            }
        }

        public object GetRegistryValue(string name, object defaultValue = null)
        {
            ValidValueName(name);
            using (RegistryKey registryKey = Open(false))
            {
                return registryKey.GetValue(name, defaultValue);
            }
        }

        public string KeyName => subKey;

        public void DeleteUnSupportedValues()
        {
            if (IsEmpty())
            {
                return;
            }
            using (RegistryKey registryKey = Open(true))
            {
                registryKey.GetValueNames().ToList().FindAll(
                    name => !BuffettCodeRegistryConfig.SupportedValueNames.Contains(name)).ForEach(name => registryKey.DeleteValue(name, false));
            }

        }

        public bool IsEmpty()
        {
            using (RegistryKey registryKey = Open(false))
            {
                return registryKey.GetValueNames().Length.Equals(0);
            }
        }
    }
}