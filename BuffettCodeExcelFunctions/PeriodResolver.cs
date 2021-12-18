using BuffettCodeCommon.Config;
using BuffettCodeCommon.Period;
using System.Text.RegularExpressions;

namespace BuffettCodeExcelFunctions
{
    public class PeriodResolver
    {
        public static IPeriod Resolve(string periodParam)
        {
            // LYLQ
            if (periodParam.Equals("LYLQ"))
            {
                return LatestFiscalQuarterPeriod.GetInstance();
            }
            else if (periodParam.Equals("latest"))
            {
                return LatestDayPeriod.GetInstance();
            }
            else if (Regex.IsMatch(periodParam, PeriodRegularExpressionConfig.FiscalQuarterPattern))
            {
                return FiscalQuarterPeriod.Parse(periodParam);
            }
            else if (Regex.IsMatch(periodParam, PeriodRegularExpressionConfig.DayPattern))
            {
                return DayPeriod.Parse(periodParam);
            }
            else
            {
                return null;
            }
        }

    }
}