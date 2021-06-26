using BuffettCodeCommon.Config;
using BuffettCodeCommon.Exception;
using BuffettCodeCommon.Period;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BuffettCodeAddinRibbon.Settings.Tests
{
    [TestClass()]
    public class CsvDownloadParametersTests
    {
        private static readonly string ticker = "1234";
        private static readonly FiscalQuarterPeriod from = FiscalQuarterPeriod.Create(2020, 1);
        private static readonly FiscalQuarterPeriod to = FiscalQuarterPeriod.Create(2020, 2);

        [TestMethod()]
        public void IsCreateNewFileTest()
        {
            var newFileOutput = CsvFileOutputSettings.Create(CSVOutputEncoding.UTF8, CSVOutputDestination.NewFile);
            var newSheetOutput = CsvFileOutputSettings.Create(CSVOutputEncoding.SJIS, CSVOutputDestination.NewSheet);
            Assert.IsTrue(CsvDownloadParameters.Create(ticker, from, to, newFileOutput).IsCreateNewFile());
            Assert.IsFalse(CsvDownloadParameters.Create(ticker, from, to, newSheetOutput).IsCreateNewFile());
        }

        [TestMethod()]
        public void IsUTF8EncodingTest()
        {
            var utf8Output = CsvFileOutputSettings.Create(CSVOutputEncoding.UTF8, CSVOutputDestination.NewFile);
            var sjisOutput = CsvFileOutputSettings.Create(CSVOutputEncoding.SJIS, CSVOutputDestination.NewSheet);
            Assert.IsTrue(CsvDownloadParameters.Create(ticker, from, to, utf8Output).IsUTF8Encoding());
            Assert.IsFalse(CsvDownloadParameters.Create(ticker, from, to, sjisOutput).IsUTF8Encoding());
        }

        [TestMethod()]
        public void CreateTest()
        {
            var outputParams = CsvFileOutputSettings.Create(CSVOutputEncoding.UTF8, CSVOutputDestination.NewFile);

            var parameters = CsvDownloadParameters.Create(ticker, from, to,
                outputParams);
            Assert.AreEqual(ticker, parameters.Ticker);
            Assert.AreEqual(from, parameters.Range.From);
            Assert.AreEqual(to, parameters.Range.To);

            Assert.ThrowsException<ValidationError>(() => CsvDownloadParameters.Create("aa", from, to, outputParams));
        }
    }
}