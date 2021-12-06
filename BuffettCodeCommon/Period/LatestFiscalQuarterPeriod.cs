using BuffettCodeCommon.Config;
using System.Collections.Generic;

namespace BuffettCodeCommon.Period
{
    public class LatestFiscalQuarterPeriod : IQuarterlyPeriod
    {
        private static readonly LatestFiscalQuarterPeriod instance = new LatestFiscalQuarterPeriod();
        private LatestFiscalQuarterPeriod() { }

        public static LatestFiscalQuarterPeriod GetInstance() => instance;

        public override string ToString() => "LatestFiscalQuarter";

        public Dictionary<string, string> ToV2Parameter() => new Dictionary<string, string>() { { ApiRequestParamConfig.KeyFy, ApiRequestParamConfig.ValueLy }, { ApiRequestParamConfig.KeyFq, ApiRequestParamConfig.ValueLq } };

        public Dictionary<string, string> ToV3Parameter() => new Dictionary<string, string>() { { ApiRequestParamConfig.KeyFy, ApiRequestParamConfig.ValueLy }, { ApiRequestParamConfig.KeyFq, ApiRequestParamConfig.ValueLq } };


    }
}