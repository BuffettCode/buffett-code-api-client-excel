using BuffettCodeAddinRibbon.Settings;
using BuffettCodeCommon.Config;
using BuffettCodeCommon.Period;
using BuffettCodeIO.Property;
using BuffettCodeIO.TabularOutput;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BuffettCodeAddinRibbon.CsvDownload.Tests
{
    [TestClass()]
    public class TabularWriterBuilderTests
    {
        private static readonly string ticker = "1234";
        private static readonly FiscalQuarterPeriod from = FiscalQuarterPeriod.Create(2020, 1);
        private static readonly FiscalQuarterPeriod to = FiscalQuarterPeriod.Create(2020, 2);
        private readonly CsvDownloadOutputSettings newFileSettting = CsvDownloadOutputSettings.Create(TabularOutputEncoding.UTF8, TabularOutputDestination.TestNewCsvFile);
        private readonly CsvDownloadOutputSettings newSheetsetting = CsvDownloadOutputSettings.Create(TabularOutputEncoding.UTF8, TabularOutputDestination.TestNewWorksheet);

        private readonly TabularWriterBuilder<Quarter> quarterBuilder = new TabularWriterBuilder<Quarter>();

        [TestMethod()]
        public void BuildQuarterCsvWriterTest()
        {
            var parameters = CsvDownloadParameters.Create(ticker, from, to,
                newFileSettting);
            var writer = quarterBuilder.Set(parameters).Build();
            Assert.IsInstanceOfType(writer, typeof(CsvFileTabularWriter<Quarter>));
        }

        // If you want test worksheet, comment in
        // On GitHub Actions, we can't use Windows Office now
        // [TestMethod()]
        public void BuildQuarterWorkseetTest()
        {
            var parameters = CsvDownloadParameters.Create(ticker, from, to,
                newSheetsetting);
            var writer = quarterBuilder.Set(parameters).Build();
            Assert.IsInstanceOfType(writer, typeof(WorksheetTabularWriter<Quarter>));
        }
    }
}