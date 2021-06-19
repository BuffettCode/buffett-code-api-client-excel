using BuffettCodeCommon.Config;
using BuffettCodeCommon.Period;
using BuffettCodeCommon.Validator;
using System.Text;

namespace BuffettCodeAddinRibbon.Settings
{
    public static class CSVFormDefaultSettingsHandler
    {
        public static CSVFormSettings Load()
        {
            var ticker = Properties.Settings.Default.CSVTicker;
            JpTickerValidator.Validate(ticker);
            var from = FiscalQuarterPeriod.Parse(Properties.Settings.Default.CSVFrom);
            var to = FiscalQuarterPeriod.Parse(Properties.Settings.Default.CSVTo);
            var outputSettings = LoadOutputSettings();

            return CSVFormSettings.Create(ticker, from, to, outputSettings);
        }

        private static CSVOutputSettings LoadOutputSettings()
        {
            Encoding encoding = Properties.Settings.Default.CSVUTF8 ?
                 CSVOutputEncoding.UTF8 : CSVOutputEncoding.SJIS;
            CSVOutputDestination destination = Properties.Settings.Default.CSVIsFile ? CSVOutputDestination.NewFile : CSVOutputDestination.NewSheet;
            return CSVOutputSettings.Create(encoding, destination);
        }

        public static void Save(CSVFormSettings settings)
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
