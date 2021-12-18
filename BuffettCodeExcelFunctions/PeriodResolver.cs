using BuffettCodeCommon.Period;
using System.Text.RegularExpressions;

namespace BuffettCodeExcelFunctions
{
    public class PeriodResolver
    {
        private static readonly string FiscalQuarterPattern = @"[12]\d{3}Q[1-5]";

        private static readonly string DayPattern = @"^[12]\d{3}-(0[1-9]|1[0-2])-(0[1-9]|[12][0-9]|3[01])$";


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
            else if (Regex.IsMatch(periodParam, FiscalQuarterPattern))
            {
                return FiscalQuarterPeriod.Parse(periodParam);
            }
            else if (Regex.IsMatch(periodParam, DayPattern))
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
