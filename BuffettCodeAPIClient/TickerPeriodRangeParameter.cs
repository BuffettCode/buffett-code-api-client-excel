using BuffettCodeCommon.Config;
using BuffettCodeCommon.Period;
using BuffettCodeCommon.Validator;
using System.Collections.Generic;

namespace BuffettCodeAPIClient
{
    public class TickerPeriodRangeParameter : ITickerParameter, IApiV2Parameter, IApiV3Parameter
    {
        private readonly string ticker;
        private readonly PeriodRange<IComparablePeriod> periodRange;
        private TickerPeriodRangeParameter(string ticker, PeriodRange<IComparablePeriod> periodRange)
        {
            this.ticker = ticker;
            this.periodRange = periodRange;
        }

        public static TickerPeriodRangeParameter Create(string ticker, PeriodRange<IComparablePeriod> periodRange)
        {
            JpTickerValidator.Validate(ticker);
            return new TickerPeriodRangeParameter(ticker, periodRange);
        }

        public static TickerPeriodRangeParameter Create(string ticker, IComparablePeriod from, IComparablePeriod to)
        {
            JpTickerValidator.Validate(ticker);
            return new TickerPeriodRangeParameter(ticker, PeriodRange<IComparablePeriod>.Create(from, to));
        }

        public string GetTicker() => ticker;

        public Dictionary<string, string> ToApiV2Parameters() => new Dictionary<string, string>{
            {ApiRequestParamConfig.KeyTickers, ticker },
            {ApiRequestParamConfig.KeyFrom, periodRange.From.ToString() },
            {ApiRequestParamConfig.KeyTo, periodRange.To.ToString() },
        };

        public Dictionary<string, string> ToApiV3Parameters() => new Dictionary<string, string>{
            {ApiRequestParamConfig.KeyTicker, ticker },
            {ApiRequestParamConfig.KeyFrom, periodRange.From.ToString() },
            {ApiRequestParamConfig.KeyTo, periodRange.To.ToString() },
        };

        public PeriodRange<IComparablePeriod> PeriodRange => periodRange;

    }
}