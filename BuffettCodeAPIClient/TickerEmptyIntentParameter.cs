using BuffettCodeCommon.Config;
using BuffettCodeCommon.Period;
using BuffettCodeCommon.Validator;
using System.Collections.Generic;

namespace BuffettCodeAPIClient
{
    public class TickerEmptyIntentParameter : IApiV2Parameter, IApiV3Parameter, ITickerIntentParameter

    {
        private readonly string ticker;
        private readonly IIntent intent;

        private TickerEmptyIntentParameter(string ticker, IIntent intent)
        {
            this.ticker = ticker;
            this.intent = intent;
        }

        public IIntent GetIntent() => intent;

        public static TickerEmptyIntentParameter Create(string ticker, IIntent intent)
        {
            JpTickerValidator.Validate(ticker);
            return new TickerEmptyIntentParameter(ticker, intent);
        }

        public Dictionary<string, string> ToApiV2Parameters() => new Dictionary<string, string>() { { ApiRequestParamConfig.KeyTicker, ticker }, };
        public Dictionary<string, string> ToApiV3Parameters() => new Dictionary<string, string>() { { ApiRequestParamConfig.KeyTicker, ticker }, };

        public string GetTicker() => ticker;

        public override bool Equals(object obj)
        {
            if (obj is TickerEmptyIntentParameter tep)
            {
                return tep.ticker.Equals(ticker) && tep.intent.Equals(intent);
            }
            else
            {
                return false;
            }
        }

        public override int GetHashCode() => ticker.GetHashCode();

    }
}