using BuffettCodeIO.Property;
using System;

namespace BuffettCodeIO.Formatter
{
    /// <summary>
    /// 数値のフォーマッタ
    /// </summary>
    public class NumericFormatter : IExcelFormatter
    {
        private static readonly NumericFormatter _instance = new NumericFormatter();

        private NumericFormatter()
        {
            // do nothing.
        }

        /// <inheritdoc/>
        public static NumericFormatter GetInstance()
        {
            return _instance;
        }

        public string Format(string value, PropertyDescription description)
        {
            if (long.TryParse(value, out long l))
            {
                return String.Format("{0:#,0}", l);
            }
            else if (double.TryParse(value, out double d))
            {
                return String.Format("{0:#,##0.#}", d);
            }
            else
            {
                return value;
            }
        }
    }
}