using System.Collections.Generic;
using System.Text;

namespace BuffettCodeCommon.Config
{
    public enum TabularOutputDestination
    {
        NewCsvFile = 0,
        NewWorksheet = 1,
        TestNewCsvFile = 2,
        TestNewWorksheet = 3,
    }

    public static class TabularOutputEncoding
    {
        public readonly static Encoding UTF8 = Encoding.UTF8;
        public readonly static Encoding SJIS = Encoding.GetEncoding("shift_jis");
        public readonly static HashSet<Encoding> SupportedEncodings = new HashSet<Encoding> { UTF8, SJIS };
    }

}