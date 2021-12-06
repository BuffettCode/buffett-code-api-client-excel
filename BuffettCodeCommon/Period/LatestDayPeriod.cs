using BuffettCodeCommon.Config;
using System.Collections.Generic;

namespace BuffettCodeCommon.Period
{
    public class LatestDayPeriod : IDailyPeriod
    {
        private static readonly LatestDayPeriod instance = new LatestDayPeriod();
        private LatestDayPeriod() { }

        public static LatestDayPeriod GetInstance() => instance;

        public override string ToString() => "LatestDayPeriod";

        public Dictionary<string, string> ToV3Parameter() => new Dictionary<string, string>() { { ApiRequestParamConfig.KeyDate, ApiRequestParamConfig.ValueLatest }, };
    }
}