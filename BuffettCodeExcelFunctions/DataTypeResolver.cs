using BuffettCodeCommon.Config;
using BuffettCodeCommon.Exception;
using System.Text.RegularExpressions;


namespace BuffettCodeExcelFunctions
{
    public class DataTypeResolver
    {
        public static DataTypeConfig Resolve(string periodParam)
        {
            // LYLQ
            if (periodParam.Equals("LYLQ"))
            {
                return DataTypeConfig.Quarter;
            }
            else if (periodParam.Equals("latest"))
            {
                return DataTypeConfig.Daily;
            }
            else if (Regex.IsMatch(periodParam, PeriodRegularExpressionConfig.FiscalQuarterPattern))
            {
                return DataTypeConfig.Quarter;
            }
            else if (Regex.IsMatch(periodParam, PeriodRegularExpressionConfig.DayPattern))
            {
                return DataTypeConfig.Daily;
            }
            else
            {
                throw new ValidationError($"{periodParam} is not supported input format");
            }
        }
    }
}
