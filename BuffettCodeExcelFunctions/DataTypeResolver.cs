using BuffettCodeCommon.Config;
using BuffettCodeCommon.Exception;


namespace BuffettCodeExcelFunctions
{
    public class DataTypeResolver
    {
        public static DataTypeConfig Resolve(string periodParam)
        {
            if (periodParam.Equals("latest"))
            {
                return DataTypeConfig.Daily;
            }
            else if (PeriodRegularExpressionConfig.FiscalQuarterRegex.IsMatch(periodParam))
            {
                return DataTypeConfig.Quarter;
            }
            else if (PeriodRegularExpressionConfig.RelativeFiscalQuarterRegex.IsMatch(periodParam))
            {
                return DataTypeConfig.Quarter;
            }
            else if (PeriodRegularExpressionConfig.DayRegex.IsMatch(periodParam))
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