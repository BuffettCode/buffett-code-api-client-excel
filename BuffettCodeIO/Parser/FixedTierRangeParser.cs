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
        public static (PeriodRange<FiscalQuarterPeriod>, PeriodRange<DayPeriod>) Parse(IEnumerable<JProperty> jProperties)
        {
            try
            {
                var properties = jProperties.ToDictionary(p => p.Name, p => p.Value);
                var oldestQuarter = FiscalQuarterPeriod.Create(properties[PropertyNames.OldestFiscalYear].ToString(), properties[PropertyNames.OldestFiscalQuarter].ToString());
                var latestQuarter = FiscalQuarterPeriod.Create(properties[PropertyNames.LatestFiscalYear].ToString(), properties[PropertyNames.LatestFiscalQuarter].ToString());
                var oldestDate = DayPeriod.Create((DateTime)properties[PropertyNames.OldestDate]);
                // use today as "latestDay"
                var latestDay = DayPeriod.Create(DateTime.Today);

                var quarterRange = PeriodRange<FiscalQuarterPeriod>.Create(oldestQuarter, latestQuarter);
                var dayRange = PeriodRange<DayPeriod>.Create(oldestDate, latestDay);
                return (quarterRange, dayRange);

            }
            catch (Exception e)
            {
                throw new ApiResponseParserException($"parse {PropertyNames.FixedTierRange} failed", e);
            }
        }
    }
}