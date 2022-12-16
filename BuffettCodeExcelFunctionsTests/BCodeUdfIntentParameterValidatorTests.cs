using BuffettCodeCommon.Exception;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BuffettCodeExcelFunctions.Tests
{
    [TestClass()]
    public class BCodeUdfIntentParameterValidatorTests
    {
        [TestMethod()]
        public void ValidateTest()
        {
            // valid cases
            BCodeUdfIntentValidator.Validate("2020Q1");
            BCodeUdfIntentValidator.Validate("LYLQ");
            BCodeUdfIntentValidator.Validate("LY-1LQ");
            BCodeUdfIntentValidator.Validate("LYLQ-20");
            BCodeUdfIntentValidator.Validate("LY-1LQ-20");
            BCodeUdfIntentValidator.Validate("2019LQ-20");
            BCodeUdfIntentValidator.Validate("LY-1Q3");
            BCodeUdfIntentValidator.Validate("2012-12-12");
            BCodeUdfIntentValidator.Validate("latest");
            BCodeUdfIntentValidator.Validate("COMPANY");
            // invalid cases
            Assert.ThrowsException<ValidationError>(() => BCodeUdfIntentValidator.Validate(""));
            Assert.ThrowsException<ValidationError>(() => BCodeUdfIntentValidator.Validate("dummy"));
            Assert.ThrowsException<ValidationError>(() => BCodeUdfIntentValidator.Validate("2012/12/12"));
            Assert.ThrowsException<ValidationError>(() => BCodeUdfIntentValidator.Validate("2020Q6"));
            Assert.ThrowsException<ValidationError>(() => BCodeUdfIntentValidator.Validate("2020"));
            Assert.ThrowsException<ValidationError>(() => BCodeUdfIntentValidator.Validate("Q3"));
            Assert.ThrowsException<ValidationError>(() => BCodeUdfIntentValidator.Validate("company"));
        }
    }
}