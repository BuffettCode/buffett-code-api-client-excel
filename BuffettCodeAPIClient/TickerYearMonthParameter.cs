using BuffettCodeCommon.Config;
using BuffettCodeCommon.Period;
using BuffettCodeCommon.Validator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuffettCodeAPIClient
{
    public class TickerYearMonthParameter : IApiV2Parameter, IApiV3Parameter, ITickerIntentParameter
    {
        private readonly string ticker;
        private readonly string yearParam;
        private readonly string monthParam;
        private readonly IYearMonthPeriod period;

        private TickerYearMonthParameter(string ticker, string yearParam, string monthParam, IYearMonthPeriod period)
        {
            this.ticker = ticker;
            this.yearParam = yearParam;
            this.monthParam = monthParam;
            this.period = period;
        }

        public static TickerYearMonthParameter Create(string ticker, string yearParam, string monthParam, IYearMonthPeriod period)
        {
            JpTickerValidator.Validate(ticker);
            return new TickerYearMonthParameter(ticker, yearParam, monthParam, period);
        }
        public static TickerYearMonthParameter Create(string ticker, YearMonthPeriod period) => Create(ticker, period.Year.ToString(), period.Month.ToString(), period);
        public IIntent GetIntent() => period;

        public string GetTicker() => ticker;

        public Dictionary<string, string> ToApiV2Parameters()
        {
            throw new NotImplementedException();
        }

        public Dictionary<string, string> ToApiV3Parameters() => new Dictionary<string, string>() {
            { ApiRequestParamConfig.KeyTicker, ticker
            }, { ApiRequestParamConfig.KeyYear, yearParam }, { ApiRequestParamConfig.KeyMonth, monthParam } };

        public override bool Equals(object obj)
        {
            if (obj is TickerYearMonthParameter tymp)
            {
                return tymp.ticker.Equals(ticker) && tymp.period.Equals(period);
            }
            else
            {
                return false;
            }
        }

        public override int GetHashCode() => (ticker, period).GetHashCode();
    }
}