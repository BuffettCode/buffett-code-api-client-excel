using BuffettCodeCommon.Config;
using BuffettCodeCommon.Exception;


namespace BuffettCodeExcelFunctions
{
    public class DataTypeResolver
    {
        public static DataTypeConfig Resolve(string intent)
        {
            if (PeriodRegularExpressionConfig.BCodeUdfDailyInputRegex.IsMatch(intent))
            {
                return DataTypeConfig.Daily;
            }
            else if (PeriodRegularExpressionConfig.BCodeUdfFiscalQuarterInputRegex.IsMatch(intent))
            {
                return DataTypeConfig.Quarter;
            }
            else if (PeriodRegularExpressionConfig.BCodeUdfCompanyString == intent)
            {
                return DataTypeConfig.Company;
            }
            else
            {
                throw new ValidationError($"{intent} is not supported input format");
            }
        }
    }
}