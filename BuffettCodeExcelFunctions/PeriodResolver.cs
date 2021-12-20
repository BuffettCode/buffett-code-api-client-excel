using BuffettCodeCommon.Config;
using BuffettCodeCommon.Exception;
using BuffettCodeCommon.Period;

namespace BuffettCodeExcelFunctions
{
    public class PeriodResolver
    {
        public static IPeriod Resolve(string periodParam)
        {
            if (periodParam.Equals("latest"))
            {
                return LatestDayPeriod.GetInstance();
            }
            else if (PeriodRegularExpressionConfig.FiscalQuarterRegex.IsMatch(periodParam))
            {
                return FiscalQuarterPeriod.Parse(periodParam);
            }
            else if (PeriodRegularExpressionConfig.RelativeFiscalQuarterRegex.IsMatch(periodParam))
            {
                return RelativeFiscalQuarterPeriod.Parse(periodParam);
            }
            else if (PeriodRegularExpressionConfig.DayRegex.IsMatch(periodParam))
            {
                return DayPeriod.Parse(periodParam);
            }
            else
            {
                throw new ValidationError($"{periodParam} is not supported input format");
            }
        }

    }
}