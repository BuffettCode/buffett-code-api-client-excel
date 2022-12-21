using System.Text.RegularExpressions;

namespace BuffettCodeCommon.Config
{
    public static class PeriodRegularExpressionConfig
    {
        public static readonly Regex FiscalYearRequestRegex = new Regex(@"^([12]\d{3}|LY(\-(?<years>[1-9]\d*))?)$");

        public static readonly Regex FiscalQuarterRequestRegex = new Regex(@"^([1-5]|LQ(\-(?<quarters>[1-9]\d*))?)$");

        public static readonly Regex FiscalQuarterRegex = new Regex(@"^[12]\d{3}Q[1-5]$");

        public static readonly Regex YearMonthRegex = new Regex(@"^(?<year>[12]\d{3})-(?<month>[01]\d)$");

        public static readonly Regex DayRegex = new Regex(@"^[12]\d{3}-(0[1-9]|1[0-2])-(0[1-9]|[12][0-9]|3[01])$");

        public static readonly Regex RelativeFiscalQuarterRegex = new Regex(@"^LY(\-(?<years>[1-9]\d*))?LQ(\-(?<quarters>[1-9]\d*))?$");

        public static readonly Regex BCodeUdfFiscalQuarterInputRegex = new Regex(@"^(?<fiscalYear>([12]\d{3}|LY(\-(?<years>[1-9]\d*))?))(?<fiscalQuarter>(Q[1-5]|LQ(\-(?<quarters>[1-9]\d*))?))$");

        public static readonly Regex BCodeUdfDailyInputRegex = new Regex(@"^([12]\d{3}-(0[1-9]|1[0-2])-(0[1-9]|[12][0-9]|3[01]))|(latest)$");

        public static readonly Regex BCodeUdfMonthlyInputRegex = new Regex(@"^(?<year>[12]\d{3})-(?<month>[01]\d)$");

        public static readonly string BCodeUdfCompanyString = "COMPANY";
    }
}