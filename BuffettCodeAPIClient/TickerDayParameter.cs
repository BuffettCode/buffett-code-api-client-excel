using BuffettCodeCommon.Config;
using BuffettCodeCommon.Exception;
using BuffettCodeCommon.Period;
using BuffettCodeCommon.Validator;
using System;
using System.Collections.Generic;

namespace BuffettCodeAPIClient
{
    public class TickerDayParameter : IApiV3Parameter, IPeriodParameter, ITickerPeriodParameter
    {
        private readonly string ticker;
        private readonly string dateParam;
        private readonly IDailyPeriod period;
        public IPeriod GetPeriod() => period;

        private TickerDayParameter(string ticker, string dateParam, IDailyPeriod period)
        {
            this.ticker = ticker;
            this.dateParam = dateParam;
            this.period = period;
        }

        public static TickerDayParameter Create(string ticker, IDailyPeriod period)
        {
            JpTickerValidator.Validate(ticker);
            if (period is LatestDayPeriod)
            {
                return new TickerDayParameter(ticker, ApiRequestParamConfig.ValueLatest, period);
            }
            else if (period is DayPeriod)
            {
                return new TickerDayParameter(ticker, period.ToString(), period);
            }
            else
            {
                throw new ArgumentException($"{period} is not supported.");
            }
        }


        public static TickerDayParameter Create(string ticker, string dateParam)
        {
            JpTickerValidator.Validate(ticker);
            if (dateParam.Equals(ApiRequestParamConfig.ValueLatest))
            {
                return new TickerDayParameter(ticker, ApiRequestParamConfig.ValueLatest, LatestDayPeriod.GetInstance());
            }
            else if (PeriodRegularExpressionConfig.DayRegex.IsMatch(dateParam))
            {
                return new TickerDayParameter(ticker, dateParam, DayPeriod.Parse(dateParam));
            }
            else
            {
                throw new ValidationError($"input {dateParam} is not supported format");
            }
        }

        public Dictionary<string, string> ToApiV3Parameters()
        => new Dictionary<string, string>() { { ApiRequestParamConfig.KeyTicker, ticker }, { ApiRequestParamConfig.KeyDate, dateParam }, };

        public string GetTicker() => ticker;

        public override bool Equals(object obj)
        {
            if (obj is TickerDayParameter tdp)
            {
                return tdp.ticker.Equals(ticker) && tdp.period.Equals(period);
            }
            else
            {
                return false;
            }
        }

        public override int GetHashCode() => (ticker, period).GetHashCode();

    }
}