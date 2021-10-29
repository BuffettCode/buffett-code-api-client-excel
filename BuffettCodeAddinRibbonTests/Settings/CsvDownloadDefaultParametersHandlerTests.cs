using BuffettCodeCommon.Config;
using BuffettCodeCommon.Period;
using Microsoft.VisualStudio.TestTools.UnitTesting;
namespace BuffettCodeAddinRibbon.Settings.Tests
{
    [TestClass()]
    public class CsvDownloadDefaultParametersHandlerTests
    {
        [TestMethod()]
        public void SaveAndLoadTest()
        {
            var ticker = "1234";
            var from = FiscalQuarterPeriod.Create(2021, 1);
            var to = FiscalQuarterPeriod.Create(2022, 1);
            var outputSettings = CsvDownloadOutputSettings.Create(TabularOutputEncoding.SJIS, TabularOutputDestination.NewWorksheet);
            var newParams = CsvDownloadParameters.Create(ticker, from, to, outputSettings);
            CsvDownloadDefaultParametersHandler.Save(newParams);
            var loaded = CsvDownloadDefaultParametersHandler.Load();
            Assert.AreEqual(ticker, loaded.Ticker);
            Assert.AreEqual(from, loaded.Range.From);
            Assert.AreEqual(to, loaded.Range.To);
            Assert.AreEqual(TabularOutputEncoding.SJIS, loaded.OutputSettings.Encoding);
            Assert.AreEqual(TabularOutputDestination.NewWorksheet
                , loaded.OutputSettings.Destination);
        }
    }

}