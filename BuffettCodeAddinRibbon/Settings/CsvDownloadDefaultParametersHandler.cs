using BuffettCodeCommon.Config;
using BuffettCodeCommon.Period;
using BuffettCodeCommon.Validator;
using System.Text;

namespace BuffettCodeAddinRibbon.Settings
{
    public static class CsvDownloadDefaultParametersHandler
    {
        public static CsvDownloadParameters Load()
        {
            var ticker = Properties.Settings.Default.CSVTicker;
            JpTickerValidator.Validate(ticker);
            var from = FiscalQuarterPeriod.Parse(Properties.Settings.Default.CSVFrom);
            var to = FiscalQuarterPeriod.Parse(Properties.Settings.Default.CSVTo);
            var outputSettings = LoadOutputSettings();

            return CsvDownloadParameters.Create(ticker, from, to, outputSettings);
        }

        private static CsvDownloadOutputSettings LoadOutputSettings()
        {
            Encoding encoding = Properties.Settings.Default.CSVUTF8 ?
                 TabularOutputEncoding.UTF8 : TabularOutputEncoding.SJIS;
            TabularOutputDestination destination = Properties.Settings.Default.CSVIsFile ? TabularOutputDestination.NewCsvFile : TabularOutputDestination.NewWorksheet;
            return CsvDownloadOutputSettings.Create(encoding, destination);
        }

        public static void Save(CsvDownloadParameters settings)
        {
            Properties.Settings.Default.CSVTicker = settings.Ticker;
            Properties.Settings.Default.CSVFrom = settings.Range.From.ToString();
            Properties.Settings.Default.CSVTo = settings.Range.To.ToString();
            Properties.Settings.Default.CSVIsFile = settings.IsCreateNewFile();
            Properties.Settings.Default.CSVUTF8 = settings.IsUTF8Encoding();
            Properties.Settings.Default.Save();
        }

    }
}
