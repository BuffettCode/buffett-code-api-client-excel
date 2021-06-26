using BuffettCodeCommon.Config;
using BuffettCodeCommon.Period;
using BuffettCodeCommon.Validator;

namespace BuffettCodeAddinRibbon.Settings
{
    public class CsvDownloadParameters
    {

        private CsvDownloadParameters(string ticker, PeriodRange<FiscalQuarterPeriod> range, CsvFileOutputSettings outputSettings)
        {
            Ticker = ticker;
            Range = range;
            OutputSettings = outputSettings;
        }

        public static CsvDownloadParameters Create(string ticker, FiscalQuarterPeriod from, FiscalQuarterPeriod to, CsvFileOutputSettings outputSettings)
        {
            JpTickerValidator.Validate(ticker);
            var range = PeriodRange<FiscalQuarterPeriod>.Create(from, to);
            return new CsvDownloadParameters(ticker, range, outputSettings);
        }
        public string Ticker { get; set; }
        public PeriodRange<FiscalQuarterPeriod> Range { get; set; }

        public CsvFileOutputSettings OutputSettings { get; set; }
        public bool IsCreateNewFile() => OutputSettings.Destination is CSVOutputDestination.NewFile;

        public bool IsUTF8Encoding() => OutputSettings.Encoding.Equals(CSVOutputEncoding.UTF8);
    }
}
