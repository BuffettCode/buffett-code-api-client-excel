using System.Collections.Generic;
using System.Text;

namespace BuffettCodeCommon.Config
{
    public static class CSVOutputEncoding
    {
        public readonly static Encoding UTF8 = Encoding.UTF8;
        public readonly static Encoding SJIS = Encoding.GetEncoding("shift_jis");
        public readonly static HashSet<Encoding> SupportedEncodings = new HashSet<Encoding> { UTF8, SJIS };
    }

    public static class CSVOutputProperties
    {
        public static readonly string[] QuarterExcludeProperties = { "ticker" };

        public static readonly string[] QuarterOrderedProperties = { "company_name", "ceo_name", "headquarters_address", "accounting_standard", "fiscal_year", "fiscal_quarter", "tdnet_title", "edinet_title" };
    }
}