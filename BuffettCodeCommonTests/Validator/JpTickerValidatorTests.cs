namespace BuffettCodeCommon.Validator.Tests
{
    using BuffettCodeCommon.Validator;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Defines the <see cref="JpTickerValidatorTests" />.
    /// </summary>
    [TestClass()]
    public class JpTickerValidatorTests
    {
        /// <summary>
        /// The TestJpTickerValidator.
        /// </summary>
        [TestMethod]
        public void TestJpTickerValidator()
        {
            var tickers = new List<String> { "1000", "2000", "9999" };
            tickers.ForEach(ticker => JpTickerValidator.Validate(ticker));

            var nonTickers = new List<String> { "999", "10000", "aaa" };
            nonTickers.ForEach(
                noTicker =>
                Assert.ThrowsException<ValidationError>(() => JpTickerValidator.Validate(noTicker)));
        }
    }
}
