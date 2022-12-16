using BuffettCodeCommon.Config;
using BuffettCodeCommon.Period;
using BuffettCodeCommon.Validator;
using System.Collections.Generic;

namespace BuffettCodeAPIClient
{
    public class TickerQuarterParameter : IApiV2Parameter, IApiV3Parameter, ITickerIntentParameter
    {
        private readonly string ticker;
        private readonly string fyParam;
        private readonly string fqParam;
        private readonly IQuarterlyPeriod period;

        private TickerQuarterParameter(string ticker, string fyParam, string fqParam, IQuarterlyPeriod period)
        {
            this.ticker = ticker;
            this.fyParam = fyParam;
            this.fqParam = fqParam;
            this.period = period;
        }

        public static TickerQuarterParameter Create(string ticker, string fyParam, string fqParam, IQuarterlyPeriod period)
        {
            JpTickerValidator.Validate(ticker);
            ApiFyParameterValidator.Validate(fyParam);
            ApiFqParameterValidator.Validate(fqParam);
            return new TickerQuarterParameter(ticker, fyParam, fqParam, period);
        }

        public static TickerQuarterParameter Create(string ticker, FiscalQuarterPeriod period) => Create(ticker, period.Year.ToString(), period.Quarter.ToString(), period);

        public static TickerQuarterParameter Create(string ticker, RelativeFiscalQuarterPeriod period) => Create(ticker, period.FiscalYearAsString(), period.FiscalQuarterAsString(), period);


        private Dictionary<string, string> ToApiParameter() => new Dictionary<string, string>() {
            { ApiRequestParamConfig.KeyTicker, ticker
            }, { ApiRequestParamConfig.KeyFy, fyParam }, { ApiRequestParamConfig.KeyFq, fqParam } };


        public Dictionary<string, string> ToApiV2Parameters() => ToApiParameter();
        public Dictionary<string, string> ToApiV3Parameters() => ToApiParameter();

        public IIntent GetIntent() => period;

        public string GetTicker() => ticker;

        public override bool Equals(object obj)
        {
            if (obj is TickerQuarterParameter tqp)
            {
                return (ticker, fyParam, fqParam, period).Equals((tqp.ticker, tqp.fyParam, tqp.fqParam, tqp.period));
            }
            else
            {
                return false;
            }
        }

        public override int GetHashCode() => (ticker, fyParam, fqParam, period).GetHashCode();

        public override string ToString() => $"TickerQuarterParameters:{(ticker, fyParam, fqParam, period)}";
    }
}