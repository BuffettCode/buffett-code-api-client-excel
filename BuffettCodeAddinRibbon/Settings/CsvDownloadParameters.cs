using BuffettCodeCommon.Config;
using BuffettCodeCommon.Period;
using BuffettCodeCommon.Validator;

namespace BuffettCodeAddinRibbon.Settings
{
    public class CsvDownloadParameters
    {

        private CsvDownloadParameters(string ticker, PeriodRange<FiscalQuarterPeriod> range, CsvDownloadOutputSettings outputSettings)
        {
            Ticker = ticker;
            Range = range;
            OutputSettings = outputSettings;
        }

        public static CsvDownloadParameters Create(string ticker, FiscalQuarterPeriod from, FiscalQuarterPeriod to, CsvDownloadOutputSettings outputSettings)
        {
            JpTickerValidator.Validate(ticker);
            var range = PeriodRange<FiscalQuarterPeriod>.Create(from, to);
            return new CsvDownloadParameters(ticker, range, outputSettings);
        }
        public string Ticker { get; set; }
        public PeriodRange<FiscalQuarterPeriod> Range { get; set; }

        public CsvDownloadOutputSettings OutputSettings { get; set; }
        public bool IsCreateNewFile() => OutputSettings.Destination is TabularOutputDestination.NewCsvFile;

        public bool IsUTF8Encoding() => OutputSettings.Encoding.Equals(TabularOutputEncoding.UTF8);
    }
}
