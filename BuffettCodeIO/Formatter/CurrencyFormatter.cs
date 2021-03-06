using BuffettCodeIO.Property;
using System;
namespace BuffettCodeIO.Formatter
{
    /// <summary>
    /// 金額のフォーマッタ
    /// </summary>
    public class CurrencyFormatter : IPropertyFormatter
    {
        private static readonly CurrencyFormatter _instance = new CurrencyFormatter();

        private CurrencyFormatter()
        {
            // do nothing.
        }

        public static CurrencyFormatter GetInstance()
        {
            return _instance;
        }

        /// <inheritdoc/>
        public string Format(string value, PropertyDescription description)
        {
            var denominator = GetDenominator(description);
            if (long.TryParse(value, out long l))
            {
                return String.Format("{0:#,0}", l / denominator);
            }
            else if (double.TryParse(value, out double d))
            {
                return String.Format("{0:#,##0.#}", d / denominator);
            }
            else
            {
                return value;
            }
        }

        private long GetDenominator(PropertyDescription description)
        {
            switch (description.Unit)
            {
                case "百万円":
                    return 1000000;
                default:
                    return 1;
            }
        }
    }
}