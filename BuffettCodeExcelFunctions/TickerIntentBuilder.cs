using BuffettCodeAPIClient;
using BuffettCodeCommon.Period;
using BuffettCodeCommon.Validator;
using System.Collections.Generic;


namespace BuffettCodeExcelFunctions
{
    public class TickerIntentBuilder : ITickerIntentBuilder
    {
        private readonly ApiResourceFetcher fetcher;
        private string ticker;
        private string intent;
        private readonly Dictionary<string, FiscalQuarterPeriod> latestFiscalQuarters;


        private TickerIntentBuilder(ApiResourceFetcher fetcher, Dictionary<string, FiscalQuarterPeriod> latestFiscalQuarters)
        {
            this.fetcher = fetcher;
            this.latestFiscalQuarters = latestFiscalQuarters;
        }

        public static TickerIntentBuilder Create(ApiResourceFetcher fetcher) => new TickerIntentBuilder(fetcher, new Dictionary<string, FiscalQuarterPeriod>());


        public static TickerIntentBuilder CreateForTest(ApiResourceFetcher fetcher, Dictionary<string, FiscalQuarterPeriod> latestFiscalQuarters) => new TickerIntentBuilder(fetcher, latestFiscalQuarters);


        public TickerIntentBuilder SetTicker(string ticker)
        {
            JpTickerValidator.Validate(ticker);
            this.ticker = ticker;
            return this;
        }

        public TickerIntentBuilder SetIntent(string intent)
        {
            this.intent = intent;
            return this;
        }

        private FiscalQuarterPeriod LatestFiscalQuarter
        {
            get
            {
                if (!latestFiscalQuarters.ContainsKey(ticker))
                {
                    var latest = fetcher.FetchLatestFiscalQuarter(ticker, true, true);
                    latestFiscalQuarters.Add(ticker, latest);
                }
                return latestFiscalQuarters[ticker];
            }
        }

        public ITickerIntentParameter Build() => TickerIntentCreator.Create(ticker, intent, LatestFiscalQuarter);

    }
}