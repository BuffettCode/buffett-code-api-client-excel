namespace BuffettCodeExcelFunctions
{
    using BuffettCodeCommon;
    using BuffettCodeCommon.Config;
    using BuffettCodeCommon.Exception;
    using ExcelDna.Integration;
    using System;


    public static class UserDefinedFunctions
    {
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

        private static bool IsV2Syntax(string parameter) => !(
            PeriodRegularExpressionConfig.BCodeUdfFiscalQuarterInputRegex.IsMatch(parameter) ||
            PeriodRegularExpressionConfig.BCodeUdfDailyInputRegex.IsMatch(parameter) ||
            PeriodRegularExpressionConfig.BCodeUdfMonthlyInputRegex.IsMatch(parameter) ||
            PeriodRegularExpressionConfig.BCodeUdfCompanyString == parameter
        );


        [ExcelFunction(Description = "Getting values using BuffettCode API", Name = "BCODE")]
        public static string BCode(string ticker, string intent, string propertyName, string isRawValue, string isWithUnit = "")
        {
            try
            {
                if (IsV2Syntax(intent))
                {
                    throw new UDFObsoletedFunctionCallException("Legacy 'BCODE(ticker, fy, fq, column)' function support has been ended");
                }
                propertyName = PropertyNameResolver.Resolve(propertyName);
                BCodeUdfIntentValidator.Validate(intent);
                var dataType = DataTypeResolver.Resolve(intent);
                var isRawValueBool = ParseBoolParameter(isRawValue, false);
                var isWithUnitBool = ParseBoolParameter(isWithUnit, false);
                return bCodeExecutor.Execute(ticker, dataType, intent, propertyName, isRawValueBool, isWithUnitBool);

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