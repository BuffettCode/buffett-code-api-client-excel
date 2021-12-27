namespace BuffettCodeExcelFunctions
{
    using BuffettCodeCommon;
    using BuffettCodeCommon.Config;
    using BuffettCodeCommon.Exception;
    using ExcelDna.Integration;
    using System;


    public static class UserDefinedFunctions
    {
        private static readonly BCodeLegacyExecutor bCodeLegacyExecutor = new BCodeLegacyExecutor(BuffettCodeApiVersion.Version2);
        private static readonly BCodeExecutor bCodeExecutor = new BCodeExecutor(BuffettCodeApiVersion.Version3);

        private static bool ParseBoolParameter(string parameter, bool defaultValue)
        {
            try
            {
                return string.IsNullOrEmpty(parameter) ? defaultValue : bool.Parse(parameter);
            }
            catch (FormatException e)
            {
                throw new ValidationError($"{parameter} is not boolean", e);
            }
        }

        private static bool IsLegacyMode(string parameter) => !(PeriodRegularExpressionConfig.BCodeUdfFiscalQuarterInputRegex.IsMatch(parameter) || PeriodRegularExpressionConfig.BCodeUdfDailyInputRegex.IsMatch(parameter));


        [ExcelFunction(Description = "Getting values using BuffettCode API", Name = "BCODE")]
        public static string BCode(string ticker, string parameter1, string parameter2, string parameter3, string parameter4 = "", string parameter5 = "")
        {
            var isLegacyMode = IsLegacyMode(parameter1);
            var propertyName = "";
            try
            {
                // legacy mode
                if (isLegacyMode)
                {
                    var fyParameter = parameter1;
                    var fqParameter = parameter2;
                    propertyName = parameter3;
                    var isRawValue = ParseBoolParameter(parameter4, false);
                    var isWithUnit = ParseBoolParameter(parameter5, false);
                    return bCodeLegacyExecutor.Execute(ticker, fyParameter, fqParameter, propertyName, isRawValue, isWithUnit);
                }
                // current mode
                else
                {
                    var periodParam = parameter1;
                    propertyName = parameter2;
                    BCodeUdfPeriodParameterValidator.Validate(periodParam);
                    var dataType = DataTypeResolver.Resolve(periodParam);
                    var isRawValue = ParseBoolParameter(parameter3, false);
                    var isWithUnit = ParseBoolParameter(parameter4, false);
                    return bCodeExecutor.Execute(ticker, dataType, periodParam, propertyName, isRawValue, isWithUnit);
                }
            }
            catch (Exception e)
            {
                return BCodeFunctionErrorHandler.ToErrorMessage(e, propertyName, Configuration.GetInstance().DebugMode);
            }
        }

        [ExcelFunction(IsHidden = true, Description = "[DEBUG] Print Api Key in a Registry", Name = "BCODE_API_KEY")]
        public static string PrintApiKeyInRegistry()
        {
            try
            {
                return Configuration.GetInstance().ApiKey;
            }
            catch (Exception e)
            {
                // always print errors as debug mode
                return BCodeFunctionErrorHandler.ToErrorMessage(e, "UDF::BCODE_API_KEY", true);
            }

        }

        [ExcelFunction(IsHidden = true, Description = "[DEBUG] Check function call (from Excel to XLL)", Name = "BCODE_PING")]
        public static string PrintRandomInteger()
        {
            try
            {
                Random r = new Random();
                return r.Next().ToString();
            }
            catch (Exception e)
            {
                // always print errors as debug mode
                return BCodeFunctionErrorHandler.ToErrorMessage(e, "UDF::BCODE_PING", true);
            }
        }

    }
}