using BuffettCodeCommon.Config;
using BuffettCodeCommon.Exception;


namespace BuffettCodeExcelFunctions
{
    public class DataTypeResolver
    {
        public static DataTypeConfig Resolve(string periodParam)
        {
            if (PeriodRegularExpressionConfig.BCodeUdfDailyInputRegex.IsMatch(periodParam))
            {
                return DataTypeConfig.Daily;
            }
            else if (PeriodRegularExpressionConfig.BCodeUdfFiscalQuarterInputRegex.IsMatch(periodParam))
            {
                return DataTypeConfig.Quarter;
            }
            else if (PeriodRegularExpressionConfig.BCodeUdfCompanyString == periodParam)
            {
                return DataTypeConfig.Company;
            }
            else
            {
                throw new ValidationError($"{periodParam} is not supported input format");
            }
        }
    }
}