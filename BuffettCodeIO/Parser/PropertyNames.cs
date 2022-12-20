using System.Collections.Generic;
namespace BuffettCodeIO.Parser
{
    // API Response JSON key names
    class PropertyNames
    {
        public static readonly string Data = "data";
        public static readonly string ColumnDescription = "column_description";
        public static readonly string Ticker = "ticker";
        public static readonly string Day = "day";
        public static readonly string Year = "year";
        public static readonly string Month = "month";
        public static readonly string FiscalYear = "fiscal_year";
        public static readonly string FiscalQuarter = "fiscal_quarter";
        public static readonly string NameJp = "name_jp";
        public static readonly string Unit = "unit";
        public static readonly string FixedTierRange = "fixed_tier_range";
        public static readonly string OldestFiscalYear = "oldest_fiscal_year";
        public static readonly string OldestFiscalQuarter = "oldest_fiscal_quarter";
        public static readonly string LatestFiscalYear = "latest_fiscal_year";
        public static readonly string LatestFiscalQuarter = "latest_fiscal_quarter";
        public static readonly string OldestDate = "oldest_date";

        public static readonly HashSet<string> IgnoredPropertyNames = new HashSet<string> { FixedTierRange };
    }
}