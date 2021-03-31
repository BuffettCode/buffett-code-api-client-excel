namespace BuffettCodeCommon.Validator.Tests
{
    using BuffettCodeCommon.Validator;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System.Collections.Generic;

    /// <summary>
    /// Defines the <see cref="FiscalYearValidatorTests" />.
    /// </summary>
    [TestClass()]
    public class FiscalYearValidatorTests
    {
        /// <summary>
        /// The ValidateTest.
        /// </summary>
        [TestMethod()]
        public void ValidateTest()
        {
            // ok case
            new List<uint> { 1901, 2000, 2020, 2099 }.ForEach(q => FiscalYearValidator.Validate(q));

            // error case
            Assert.ThrowsException<ValidationError>(() => FiscalYearValidator.Validate(0));
            Assert.ThrowsException<ValidationError>(() => FiscalYearValidator.Validate(1899));
            Assert.ThrowsException<ValidationError>(() => FiscalYearValidator.Validate(2101));
        }
    }
}
