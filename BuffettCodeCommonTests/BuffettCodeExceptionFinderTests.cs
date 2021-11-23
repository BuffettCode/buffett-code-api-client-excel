using BuffettCodeCommon.Exception;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BuffettCodeCommon.Tests
{
    class ExceptionWrapper : System.Exception
    {
        public ExceptionWrapper(string message, System.Exception inner) : base(message, inner) { }
    }

    class TestBuffettCodeException : BaseBuffettCodeException { }

    [TestClass()]
    public class BuffettCodeExceptionFinderTests
    {
        [TestMethod()]
        public void FindTest()
        {
            // BuffettCodeException
            var bcException = new TestAPIConstraintException();
            var bcExceptionWrapper = new ExceptionWrapper("dummy", bcException);
            var nonBcException = new System.Exception();
            var nonBcExceptionWrapper = new ExceptionWrapper("dummy", nonBcException);

            Assert.AreEqual(
                bcException,
                BuffettCodeExceptionFinder.Find(bcException));

            Assert.AreEqual(
                bcException,
                BuffettCodeExceptionFinder.Find(bcExceptionWrapper));

            Assert.IsNull(
                BuffettCodeExceptionFinder.Find(nonBcException));

            Assert.IsNull(
                BuffettCodeExceptionFinder.Find(nonBcExceptionWrapper));
        }
    }
}