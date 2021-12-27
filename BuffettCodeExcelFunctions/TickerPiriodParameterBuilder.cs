using BuffettCodeAPIClient;
using BuffettCodeCommon.Period;
using BuffettCodeCommon.Validator;
using System.Collections.Generic;


namespace BuffettCodeExcelFunctions
{
    public class TickerPiriodParameterBuilder : ITickerPeriodParameterBuilder
    {
        private readonly ApiResourceFetcher fetcher;
        private string ticker;
        private string periodParam;
        private readonly Dictionary<string, FiscalQuarterPeriod> latestFiscalQuarters;


        private TickerPiriodParameterBuilder(ApiResourceFetcher fetcher, Dictionary<string, FiscalQuarterPeriod> latestFiscalQuarters)
        {
            this.fetcher = fetcher;
            this.latestFiscalQuarters = latestFiscalQuarters;
        }

        public static TickerPiriodParameterBuilder Create(ApiResourceFetcher fetcher) => new TickerPiriodParameterBuilder(fetcher, new Dictionary<string, FiscalQuarterPeriod>());


        public static TickerPiriodParameterBuilder CreateForTest(ApiResourceFetcher fetcher, Dictionary<string, FiscalQuarterPeriod> latestFiscalQuarters) => new TickerPiriodParameterBuilder(fetcher, latestFiscalQuarters);


        public TickerPiriodParameterBuilder SetTicker(string ticker)
        {
            JpTickerValidator.Validate(ticker);
            this.ticker = ticker;
            return this;
        }

        public TickerPiriodParameterBuilder SetPeriodParam(string periodParam)
        {
            this.periodParam = periodParam;
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

        public ITickerPeriodParameter Build() => TickerPeriodParameterCreator.Create(ticker, periodParam, LatestFiscalQuarter);

    }
}