using BuffettCodeCommon.Config;
using BuffettCodeCommon.Exception;

namespace BuffettCodeCommon.Validator
{
    public static class ApiFyParameterValidator
    {
        public static void Validate(string fyParam)
        {
            if (!PeriodRegularExpressionConfig.FiscalYearRequestRegex.IsMatch(fyParam))
            {
                throw new ValidationError($"{fyParam} is not supported format for fiscal year");
            }
        }
    }
}