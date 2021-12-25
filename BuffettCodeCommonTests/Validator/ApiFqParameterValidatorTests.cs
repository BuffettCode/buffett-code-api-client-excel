using BuffettCodeCommon.Exception;
using Microsoft.VisualStudio.TestTools.UnitTesting;
namespace BuffettCodeCommon.Validator.Tests
{
    [TestClass()]
    public class ApiFqParameterValidatorTests
    {
        [TestMethod()]
        public void ValidateTest()
        {
            // valid
            ApiFqParameterValidator.Validate("1");
            ApiFqParameterValidator.Validate("2");
            ApiFqParameterValidator.Validate("3");
            ApiFqParameterValidator.Validate("4");
            ApiFqParameterValidator.Validate("5");
            ApiFqParameterValidator.Validate("LQ");
            ApiFqParameterValidator.Validate("LQ-1");
            ApiFqParameterValidator.Validate("LQ-20");

            // invalid

            Assert.ThrowsException<ValidationError>(() => ApiFqParameterValidator.Validate(""));
            Assert.ThrowsException<ValidationError>(() => ApiFqParameterValidator.Validate("dummy"));
            Assert.ThrowsException<ValidationError>(() => ApiFqParameterValidator.Validate("Q3"));
            Assert.ThrowsException<ValidationError>(() => ApiFqParameterValidator.Validate("2020"));
        }
    }
}