using BuffettCodeCommon.Exception;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Win32;
using System;
using System.Linq;
namespace BuffettCodeCommon.Registry.Tests
{
    [TestClass()]
    public class BuffettCodeRegistryAccessorTests
    {
        private static readonly string TestRegistryName = BuffettCodeRegistryConfig.SubKeyBuffettCodeExcelAddinTest;

        private static readonly BuffettCodeRegistryAccessor accessor
            = BuffettCodeRegistryAccessor.Create(TestRegistryName);

        private readonly Random randomizer = new Random();

        private string GetRandomSubKey()
        {
            var subkeys = BuffettCodeRegistryConfig.SupportedValueNames.ToArray();
            return subkeys[randomizer.Next(subkeys.Length)];
        }

        private static RegistryKey Open(bool writable) => Microsoft.Win32
            .Registry.CurrentUser.OpenSubKey(TestRegistryName, writable);

        private static bool IsEmpty(RegistryKey registryKey) => registryKey.GetValueNames().Length.Equals(0);

        [TestInitialize()]
        [TestCleanup()]
        public void CleanUpRegistry()
        {
            using (RegistryKey registryKey = Open(true))
            {
                if (!IsEmpty(registryKey))
                {
                    registryKey.GetValueNames().ToList().ForEach(
                        name => registryKey.DeleteValue(name, false));
                }
            }
        }

        [TestMethod()]
        public void CreateTest()
        {
            Assert.AreEqual(TestRegistryName, BuffettCodeRegistryAccessor.Create(TestRegistryName).KeyName);

            // validation check
            Assert.ThrowsException<AddinConfigurationException>(() => BuffettCodeRegistryAccessor.Create("dummy"));
        }


        [TestMethod()]
        public void ValidValueNameTest()
        {
            // do nothing for supported keys
            BuffettCodeRegistryConfig.SupportedValueNames.ToList().ForEach(name => BuffettCodeRegistryAccessor.ValidValueName(name));

            // validation check
            Assert.ThrowsException<AddinConfigurationException>(() => BuffettCodeRegistryAccessor.ValidValueName("dummy"));
        }

        [TestMethod()]
        public void SaveRegistryValueTest()
        {
            var subKey = GetRandomSubKey();
            using (RegistryKey registryKey = Open(false))
            {
                // at first, subKeys are not set
                Assert.IsTrue(IsEmpty(registryKey));
                var value = randomizer.Next();

                // save
                accessor.SaveRegistryValue(subKey, value);
                // get
                Assert.AreEqual(value, (int)registryKey.GetValue(subKey, null));
                Assert.IsFalse(IsEmpty(registryKey));
            }

        }

        [TestMethod()]
        public void GetRegistryValueTest()
        {
            var subKey = GetRandomSubKey();
            // at first, subKeys are not set
            using (RegistryKey registryKey = Open(true))
            {
                Assert.IsTrue(IsEmpty(registryKey));

                // set
                var value = new Random().NextDouble().ToString();
                registryKey.SetValue(subKey, value);
                registryKey.Flush();

                // get
                Assert.AreEqual(value, accessor.GetRegistryValue(subKey, null));
                Assert.IsFalse(IsEmpty(registryKey));
            }
        }

        [TestMethod()]
        public void DeleteUnSupportedValuesTest()
        {
            using (RegistryKey registryKey = Open(true))
            {
                Assert.IsTrue(IsEmpty(registryKey));
                // append dummy value
                registryKey.SetValue("dummy", "dummy");
                Assert.IsFalse(IsEmpty(registryKey));

                // delete 
                accessor.DeleteUnSupportedValues();
                Assert.IsTrue(IsEmpty(registryKey));
            }

        }

        [TestMethod()]
        public void IsEmptyTest()
        {
            using (RegistryKey registryKey = Open(true))
            {
                var name = randomizer.Next().ToString();
                Assert.IsTrue(IsEmpty(registryKey));
                Assert.IsTrue(accessor.IsEmpty());
                // append dummy value
                registryKey.SetValue(name, "dummy");
                Assert.IsFalse(IsEmpty(registryKey));
                Assert.IsFalse(accessor.IsEmpty());

                // delete 
                registryKey.DeleteValue(name, false);
                Assert.IsTrue(IsEmpty(registryKey));
                Assert.IsTrue(accessor.IsEmpty());
            }

        }

    }
}