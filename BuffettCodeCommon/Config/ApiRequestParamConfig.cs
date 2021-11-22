using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuffettCodeCommon.Config
{
    public static class ApiRequestParamConfig
    {
        // keys
        public static readonly string KeyTicker = "ticker";
        public static readonly string KeyTickers = "tickers";
        public static readonly string KeyFy = "fy";
        public static readonly string KeyFq = "fq";
        public static readonly string KeyFrom = "from";
        public static readonly string KeyTo = "to";
        public static readonly string KeyDate = "date";
  

        public static readonly string ValueLy = "LY";
        public static readonly string ValueLq = "LQ";
        public static readonly string ValueLatest = "latest";
        // test api keyでも利用可能な "01" 銘柄を代表値として使う
        public static readonly string ValueRepresentativeJpTicker = "6501";
    }
}
