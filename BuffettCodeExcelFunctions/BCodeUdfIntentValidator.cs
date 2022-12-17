using BuffettCodeCommon.Config;
using BuffettCodeCommon.Exception;


namespace BuffettCodeExcelFunctions
{
    public static class BCodeUdfIntentValidator
    {
        public static void Validate(string intent)
        {
            if (!(PeriodRegularExpressionConfig.BCodeUdfFiscalQuarterInputRegex.IsMatch(intent) || PeriodRegularExpressionConfig.BCodeUdfDailyInputRegex.IsMatch(intent) || PeriodRegularExpressionConfig.BCodeUdfCompanyString == intent))
            {
                throw new ValidationError($"{intent} is not supported period format for BCODE");
            }
        }
    }
}