using BuffettCodeCommon.Exception;
using BuffettCodeCommon.Registry;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;

namespace BuffettCodeCommon.Tests
{


    [TestClass()]
    public class ConfigurationTests
    {
        // create private class for an unit test 
        private class ConfigurationForTest : Configuration
        {
            public ConfigurationForTest() : base(BuffettCodeRegistryConfig.SubKeyBuffettCodeExcelAddinTest)
            {
            }

        }

        private readonly static ConfigurationForTest configForTest = new ConfigurationForTest();

        private readonly static BuffettCodeRegistryAccessor accessor = BuffettCodeRegistryAccessor.Create(BuffettCodeRegistryConfig.SubKeyBuffettCodeExcelAddinTest);

        private readonly Random randomizer = new Random();

        [TestInitialize()]
        [TestCleanup()]
        public void CleanUpRegistry()
        {
            BuffettCodeRegistryConfig.SupportedValueNames.ToList().ForEach(
                    name => accessor.SaveRegistryValue(name, null));
        }

        [TestMethod()]
        public void GetInstanceTest()
        {
            Assert.AreEqual(BuffettCodeRegistryConfig.SubKeyBuffettCodeExcelAddinRelease, Configuration.GetInstance().KeyName);
            Assert.AreEqual(BuffettCodeRegistryConfig.SubKeyBuffettCodeExcelAddinDev, Configuration.GetInstance(true).KeyName);
            Assert.AreEqual(BuffettCodeRegistryConfig.SubKeyBuffettCodeExcelAddinTest, configForTest.KeyName);
        }


        [TestMethod()]
        public void ApiKeyTest()
        {
            // check default value
            Assert.AreEqual("sAJGq9JH193KiwnF947v74KnDYkO7z634LWQQfPY", configForTest.ApiKey);

            // set and get
            var apiKey = randomizer.Next().ToString().PadLeft(40, '0');
            configForTest.ApiKey = apiKey;
            Assert.AreEqual(apiKey, configForTest.ApiKey);

            // validation
            Assert.ThrowsException<AddinConfigurationException>(() => configForTest.ApiKey = "dummy"
            );
        }


        [TestMethod()]
        public void UseOndemandEndpointTest()
        {
            // check default value
            Assert.IsFalse(configForTest.IsOndemandEndpointEnabled);

            // false => true
            configForTest.IsOndemandEndpointEnabled = true;
            Assert.IsTrue(configForTest.IsOndemandEndpointEnabled);

            // true => false
            configForTest.IsOndemandEndpointEnabled = false;
            Assert.IsFalse(configForTest.IsOndemandEndpointEnabled);
        }

        [TestMethod()]
        public void DebugModeTest()
        {
            // check default value
            Assert.IsFalse(configForTest.DebugMode);

            // false => true
            configForTest.DebugMode = true;
            Assert.IsTrue(configForTest.DebugMode);

            // true => false
            configForTest.DebugMode = false;
            Assert.IsFalse(configForTest.DebugMode);
        }

        [TestMethod()]
        public void TestIsForceOndemandApiEnabled()
        {
            // check default value
            Assert.IsFalse(configForTest.IsForceOndemandApiEnabled);

            // false => true
            configForTest.IsForceOndemandApiEnabled = true;
            Assert.IsTrue(configForTest.IsForceOndemandApiEnabled);

            // true => false
            configForTest.IsForceOndemandApiEnabled = false;
            Assert.IsFalse(configForTest.IsForceOndemandApiEnabled);
        }
    }
}