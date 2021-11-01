using BuffettCodeCommon.Config;
using BuffettCodeCommon.Exception;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Text;
namespace BuffettCodeCommon.Validator.Tests
{
    [TestClass()]
    public class CSVOutputEncodingValidatorTests
    {
        [TestMethod()]
        public void ValidateTest()
        {
            // valid
            CSVOutputEncodingValidator.Validate(TabularOutputEncoding.UTF8);
            CSVOutputEncodingValidator.Validate(TabularOutputEncoding.SJIS);
            // invalid
            Assert.ThrowsException<ValidationError>(() => CSVOutputEncodingValidator.Validate(Encoding.UTF7));
        }
    }
}