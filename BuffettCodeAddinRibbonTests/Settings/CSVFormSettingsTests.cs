using BuffettCodeCommon.Config;
using BuffettCodeCommon.Exception;
using BuffettCodeCommon.Period;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BuffettCodeAddinRibbon.Settings.Tests
{
    [TestClass()]
    public class CSVFormSettingsTests
    {
        private static readonly string ticker = "1234";
        private static readonly FiscalQuarterPeriod from = FiscalQuarterPeriod.Create(2020, 1);
        private static readonly FiscalQuarterPeriod to = FiscalQuarterPeriod.Create(2020, 2);

        [TestMethod()]
        public void IsCreateNewFileTest()
        {
            var newFileOutput = CSVOutputSettings.Create(CSVOutputEncoding.UTF8, CSVOutputDestination.NewFile);
            var newSheetOutput = CSVOutputSettings.Create(CSVOutputEncoding.SJIS, CSVOutputDestination.NewSheet);
            Assert.IsTrue(CSVFormSettings.Create(ticker, from, to, newFileOutput).IsCreateNewFile());
            Assert.IsFalse(CSVFormSettings.Create(ticker, from, to, newSheetOutput).IsCreateNewFile());
        }

        [TestMethod()]
        public void IsUTF8EncodingTest()
        {
            var utf8Output = CSVOutputSettings.Create(CSVOutputEncoding.UTF8, CSVOutputDestination.NewFile);
            var sjisOutput = CSVOutputSettings.Create(CSVOutputEncoding.SJIS, CSVOutputDestination.NewSheet);
            Assert.IsTrue(CSVFormSettings.Create(ticker, from, to, utf8Output).IsUTF8Encoding());
            Assert.IsFalse(CSVFormSettings.Create(ticker, from, to, sjisOutput).IsUTF8Encoding());
        }

        [TestMethod()]
        public void CreateTest()
        {
            var outputSettings = CSVOutputSettings.Create(CSVOutputEncoding.UTF8, CSVOutputDestination.NewFile);

            var settings = CSVFormSettings.Create(ticker, from, to,
                outputSettings);
            Assert.AreEqual(ticker, settings.Ticker);
            Assert.AreEqual(from, settings.Range.From);
            Assert.AreEqual(to, settings.Range.To);

            Assert.ThrowsException<ValidationError>(() => CSVFormSettings.Create("aa", from, to, outputSettings));
        }
    }
}