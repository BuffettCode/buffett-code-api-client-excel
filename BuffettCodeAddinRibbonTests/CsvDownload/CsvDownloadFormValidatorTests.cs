using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BuffettCodeAddinRibbon.CsvDownload.Tests
{
    [TestClass()]
    public class CsvDownloadFormValidatorTests
    {
        [TestMethod()]
        public void ValidateFiscalQuarterTest()
        {
            // valid cases
            Assert.IsNull(CsvDownloadFormValidator.ValidateFiscalQuarter("2020Q1"));
            Assert.IsNull(CsvDownloadFormValidator.ValidateFiscalQuarter("2021Q4"));

            // invalid cases
            Assert.IsNotNull(CsvDownloadFormValidator.ValidateFiscalQuarter("2020Q6"));
            Assert.IsNotNull(CsvDownloadFormValidator.ValidateFiscalQuarter("2020"));
            Assert.IsNotNull(CsvDownloadFormValidator.ValidateFiscalQuarter("20Q3"));
            Assert.IsNotNull(CsvDownloadFormValidator.ValidateFiscalQuarter("20Q1st"));
        }
    }
}