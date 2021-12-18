namespace BuffettCodeCommon.Config
{
    public static class PeriodRegularExpressionConfig
    {
        public static readonly string FiscalQuarterPattern = @"[12]\d{3}Q[1-5]";

        public static readonly string DayPattern = @"^[12]\d{3}-(0[1-9]|1[0-2])-(0[1-9]|[12][0-9]|3[01])$";
    }
}