using BuffettCodeCommon.Config;
using BuffettCodeCommon.Exception;


namespace BuffettCodeExcelFunctions
{
    public static class BCodeUdfPeriodParameterValidator
    {
        public static void Validate(string periodParam)
        {
            if (!(PeriodRegularExpressionConfig.BCodeUdfFiscalQuarterInputRegex.IsMatch(periodParam) || PeriodRegularExpressionConfig.BCodeUdfDailyInputRegex.IsMatch(periodParam)))
            {
                throw new ValidationError($"{periodParam} is not supported period format for BCODE");
            }
        }
    }
}