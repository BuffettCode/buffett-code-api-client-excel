using System;

namespace BuffettCodeIO.Formatter
{
    /// <summary>
    /// 割合のフォーマッタ
    /// </summary>
    public class RatioFormatter : IFormatter
    {
        private static RatioFormatter _instance = new RatioFormatter();

        private RatioFormatter()
        {
            // do nothing.
        }

        public static RatioFormatter GetInstance()
        {
            return _instance;
        }

        /// <inheritdoc/>
        public string Format(string value, PropertyDescrption description)
        {
            var format = GetFormat(description);
            return double.TryParse(value, out double d) ? String.Format(format, d) : value;
        }

        private string GetFormat(PropertyDescrption description)
        {
            switch (description.Name)
            {
                case "dividend_yield_forecast": // 配当利回り（会社予想）
                case "dividend_yield_actual": // 配当利回り（実績）
                    return "{0:f2}";
                default:
                    return "{0:f1}";
            }
        }
    }
}
