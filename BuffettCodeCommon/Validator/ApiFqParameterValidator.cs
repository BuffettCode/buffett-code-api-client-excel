using BuffettCodeCommon.Config;
using BuffettCodeCommon.Exception;

namespace BuffettCodeCommon.Validator
{
    public static class ApiFqParameterValidator
    {
        public static void Validate(string fqParam)
        {
            if (!PeriodRegularExpressionConfig.FiscalQuarterRequestRegex.IsMatch(fqParam))
            {
                throw new ValidationError($"{fqParam} is not supported format for fiscal quarter");
            }
        }
    }
}