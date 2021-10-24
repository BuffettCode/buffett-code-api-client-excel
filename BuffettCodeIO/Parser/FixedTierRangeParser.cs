using BuffettCodeCommon.Exception;
using BuffettCodeCommon.Period;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BuffettCodeIO.Parser
{
    public class FixedTierRangeParser
    {
        public static PeriodRange<FiscalQuarterPeriod> Parse(IEnumerable<JProperty> jProperties)
        {
            try
            {
                var properties = jProperties.ToDictionary(p => p.Name, p => p.Value.ToString());
                var from = FiscalQuarterPeriod.Create(properties[PropertyNames.OldestFiscalYear], properties[PropertyNames.OldestFiscalQuarter]);
                var to = FiscalQuarterPeriod.Create(properties[PropertyNames.LatestFiscalYear], properties[PropertyNames.LatestFiscalQuarter]);
                return PeriodRange<FiscalQuarterPeriod>.Create(from, to);
            }
            catch (Exception e)
            {
                throw new ApiResponseParserException($"parse {PropertyNames.FixedTierRange} failed", e);
            }
        }
    }
}