using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BuffettCodeExcelFunctions.UnitTests
{
    [TestClass]
    public class UserDefinedFunctionsTest
    {

        [TestMethod]
        public void TestBCODE_PING()
        {
            var result = UserDefinedFunctions.PrintRandomInteger();
            Assert.IsNotNull(result);
        }
    }
}