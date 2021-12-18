using BuffettCodeCommon;
using BuffettCodeCommon.Exception;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BuffettCodeExcelFunctions.Tests
{
    [TestClass()]
    public class UserDefinedFunctionsTests
    {
        [TestMethod()]
        public void IsLegacyModeTest()
        {
            var udf = new PrivateType(typeof(UserDefinedFunctions));
            Assert.IsTrue((bool)udf.InvokeStatic("IsLegacyMode", ""));
            Assert.IsTrue((bool)udf.InvokeStatic("IsLegacyMode", "2020"));
            Assert.IsFalse((bool)udf.InvokeStatic("IsLegacyMode", "2020-01-01"));
            Assert.IsFalse((bool)udf.InvokeStatic("IsLegacyMode", "2020Q1"));
        }


        [TestMethod()]
        public void ParseBoolParameterTest()
        {
            var udf = new PrivateType(typeof(UserDefinedFunctions));
            Assert.ThrowsException<ValidationError>(
                () => udf.InvokeStatic("ParseBoolParameter", "dummy", true));
            Assert.IsTrue((bool)udf.InvokeStatic("ParseBoolParameter", "", true));
            Assert.IsFalse((bool)udf.InvokeStatic("ParseBoolParameter", null, false));
            Assert.IsTrue((bool)udf.InvokeStatic("ParseBoolParameter", "TRUE", false));
            Assert.IsTrue((bool)udf.InvokeStatic("ParseBoolParameter", "true", false));
            Assert.IsFalse((bool)udf.InvokeStatic("ParseBoolParameter", "FALSE", true));
            Assert.IsFalse((bool)udf.InvokeStatic("ParseBoolParameter", "false", true));
        }



        [TestMethod()]
        public void PrintApiKeyInRegistryTest()
        {
            Assert.AreEqual(Configuration.GetInstance().ApiKey, UserDefinedFunctions.PrintApiKeyInRegistry());
        }

        [TestMethod()]
        public void PrintRandomIntegerTest()
        {
            Assert.IsNotNull(UserDefinedFunctions.PrintRandomInteger());
        }
    }
}