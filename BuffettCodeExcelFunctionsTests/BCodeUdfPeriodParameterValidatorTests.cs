using BuffettCodeCommon.Exception;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BuffettCodeExcelFunctions.Tests
{
    [TestClass()]
    public class BCodeUdfPeriodParameterValidatorTests
    {
        [TestMethod()]
        public void ValidateTest()
        {
            // valid cases
            BCodeUdfPeriodParameterValidator.Validate("2020Q1");
            BCodeUdfPeriodParameterValidator.Validate("LYLQ");
            BCodeUdfPeriodParameterValidator.Validate("LY-1LQ");
            BCodeUdfPeriodParameterValidator.Validate("LYLQ-20");
            BCodeUdfPeriodParameterValidator.Validate("LY-1LQ-20");
            BCodeUdfPeriodParameterValidator.Validate("2019LQ-20");
            BCodeUdfPeriodParameterValidator.Validate("LY-1Q3");
            BCodeUdfPeriodParameterValidator.Validate("2012-12-12");
            BCodeUdfPeriodParameterValidator.Validate("latest");
            BCodeUdfPeriodParameterValidator.Validate("COMPANY");
            // invalid cases
            Assert.ThrowsException<ValidationError>(() => BCodeUdfPeriodParameterValidator.Validate(""));
            Assert.ThrowsException<ValidationError>(() => BCodeUdfPeriodParameterValidator.Validate("dummy"));
            Assert.ThrowsException<ValidationError>(() => BCodeUdfPeriodParameterValidator.Validate("2012/12/12"));
            Assert.ThrowsException<ValidationError>(() => BCodeUdfPeriodParameterValidator.Validate("2020Q6"));
            Assert.ThrowsException<ValidationError>(() => BCodeUdfPeriodParameterValidator.Validate("2020"));
            Assert.ThrowsException<ValidationError>(() => BCodeUdfPeriodParameterValidator.Validate("Q3"));
            Assert.ThrowsException<ValidationError>(() => BCodeUdfPeriodParameterValidator.Validate("company"));
        }
    }
}