using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BuffettCodeAddin.UnitTests
{
    [TestClass]
    public class UserDefinedFunctionsTest
    {

        [TestMethod]
        public void TestBCODE_PING()
        {
            var result = UserDefinedFunctions.BCODE_PING();
            Assert.IsNotNull(result);
        }
    }
}
