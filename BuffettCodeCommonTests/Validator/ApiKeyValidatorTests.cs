using BuffettCodeCommon.Exception;
using Microsoft.VisualStudio.TestTools.UnitTesting;
namespace BuffettCodeCommon.Validator.Tests
{
    [TestClass()]
    public class ApiKeyValidatorTests
    {
        [TestMethod()]
        public void ValidateTest()
        {
            // test api token is valid 
            ApiKeyValidator.Validate("sAJGq9JH193KiwnF947v74KnDYkO7z634LWQQfPY");

            Assert.ThrowsException<ValidationError>(() => ApiKeyValidator.Validate("abc"));
        }
    }
}