using BuffettCodeAddinRibbon.Settings;
using BuffettCodeCommon;
using BuffettCodeCommon.Config;
using BuffettCodeCommon.Period;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;


namespace BuffettCodeAddinRibbon.CsvDownload.Tests
{
    [TestClass()]
    public class ApiResourceGetterTests
    {
        public void CreateTest()
        {
            // default
            Assert.IsInstanceOfType(
                 ApiResourceGetter.Create(),
                 typeof(ApiResourceGetter)
            );
        }

        [TestMethod()]
        public void GetQuartersTest()
        {
            var ticker = "6501";
            var from = FiscalQuarterPeriod.Create(2020, 1);
            var to = FiscalQuarterPeriod.Create(2020, 2);
            var outputSettings = CsvDownloadOutputSettings.Create(TabularOutputEncoding.SJIS, TabularOutputDestination.NewWorksheet);
            var parameters = CsvDownloadParameters.Create(ticker, from, to, outputSettings);
            var quarters = ApiResourceGetter.Create().GetQuarters(parameters).ToArray();
            var quarter2020Q1 = quarters[0];
            var quarter2020Q2 = quarters[1];
            Assert.AreEqual(2, quarters.Length);
            Assert.AreEqual(ticker, quarter2020Q1.Ticker);
            Assert.AreEqual(ticker, quarter2020Q2.Ticker);
            Assert.AreEqual(from, quarter2020Q1.Period);
            Assert.AreEqual(to, quarter2020Q2.Period);
        }
    }
}