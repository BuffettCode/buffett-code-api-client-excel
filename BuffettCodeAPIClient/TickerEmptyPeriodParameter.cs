using BuffettCodeCommon.Config;
using BuffettCodeCommon.Period;
using BuffettCodeCommon.Validator;
using System.Collections.Generic;

namespace BuffettCodeAPIClient
{
    public class TickerEmptyPeriodParameter : IApiV2Parameter, IApiV3Parameter, ITickerPeriodParameter

    {
        private readonly string ticker;
        private readonly IPeriod period;

        private TickerEmptyPeriodParameter(string ticker, IPeriod period)
        {
            this.ticker = ticker;
            this.period = period;
        }

        public IPeriod GetPeriod() => period;

        public static TickerEmptyPeriodParameter Create(string ticker, IPeriod period)
        {
            JpTickerValidator.Validate(ticker);
            return new TickerEmptyPeriodParameter(ticker, period);
        }

        public Dictionary<string, string> ToApiV2Parameters() => new Dictionary<string, string>() { { ApiRequestParamConfig.KeyTicker, ticker }, };
        public Dictionary<string, string> ToApiV3Parameters() => new Dictionary<string, string>() { { ApiRequestParamConfig.KeyTicker, ticker }, };

        public string GetTicker() => ticker;

        public override bool Equals(object obj)
        {
            if (obj is TickerEmptyPeriodParameter tep)
            {
                return tep.ticker.Equals(ticker) && tep.period.Equals(period);
            }
            else
            {
                return false;
            }
        }

        public override int GetHashCode() => ticker.GetHashCode();

    }
}