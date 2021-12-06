using BuffettCodeCommon.Exception;
using BuffettCodeCommon.Period;
using BuffettCodeIO.Property;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BuffettCodeIO.Parser
{
    public class FixedTierRangeParser
    {


        public static FixedTierRange Parse(IEnumerable<JProperty> jProperties)
        {
            try
            {
                var properties = jProperties.ToDictionary(p => p.Name, p => p.Value.ToString());
                var oldestQuarter = FiscalQuarterPeriod.Create(properties[PropertyNames.OldestFiscalYear].ToString(), properties[PropertyNames.OldestFiscalQuarter].ToString());
                var latestQuarter = FiscalQuarterPeriod.Create(properties[PropertyNames.LatestFiscalYear], properties[PropertyNames.LatestFiscalQuarter]);
                var oldestDate = DayPeriod.Create(DateTime.Parse(properties[PropertyNames.OldestDate]));

                return new FixedTierRange(oldestQuarter, latestQuarter, oldestDate);
            }
            catch (Exception e)
            {
                throw new ApiResponseParserException($"parse {PropertyNames.FixedTierRange} failed", e);
            }
        }
    }
}