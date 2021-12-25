using BuffettCodeCommon.Exception;
using Microsoft.VisualStudio.TestTools.UnitTesting;
namespace BuffettCodeCommon.Validator.Tests
{
    [TestClass()]
    public class ApiFyParameterValidatorTests
    {
        [TestMethod()]
        public void ValidateTest()
        {
            // valid cases
            ApiFyParameterValidator.Validate("2020");
            ApiFyParameterValidator.Validate("LY");
            ApiFyParameterValidator.Validate("LY-1");
            ApiFyParameterValidator.Validate("LY-10");

            // invalid cases
            Assert.ThrowsException<ValidationError>(() => ApiFyParameterValidator.Validate(""));
            Assert.ThrowsException<ValidationError>(() => ApiFyParameterValidator.Validate("dummy"));
            Assert.ThrowsException<ValidationError>(() => ApiFyParameterValidator.Validate("LQ"));
            Assert.ThrowsException<ValidationError>(() => ApiFyParameterValidator.Validate("3"));

        }
    }
}