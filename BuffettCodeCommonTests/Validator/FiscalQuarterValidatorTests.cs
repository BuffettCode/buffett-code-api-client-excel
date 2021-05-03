using BuffettCodeCommon.Exception;
namespace BuffettCodeCommon.Validator.Tests
{
    using BuffettCodeCommon.Validator;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System.Collections.Generic;

    /// <summary>
    /// Defines the <see cref="FiscalQuarterValidatorTests" />.
    /// </summary>
    [TestClass()]
    public class FiscalQuarterValidatorTests
    {
        /// <summary>
        /// The ValidateTest.
        /// </summary>
        [TestMethod()]
        public void ValidateTest()
        {
            // 1 - 5  is quarter, path validate method
            new List<uint> { 1, 2, 3, 4, 5 }.ForEach(q => FiscalQuarterValidator.Validate(q));

            // error case, 0 or 6+ int
            Assert.ThrowsException<ValidationError>(() => FiscalQuarterValidator.Validate(0));


            Assert.ThrowsException<ValidationError>(() => FiscalQuarterValidator.Validate(6));
        }
    }
}