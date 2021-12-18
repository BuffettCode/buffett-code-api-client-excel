using BuffettCodeIO.Property;
using System;

namespace BuffettCodeIO.Formatter
{
    public class MillionYenFormatter : IPropertyFormatter
    {
        private static readonly MillionYenFormatter _instance = new MillionYenFormatter();

        private MillionYenFormatter()
        {
            // do nothing
        }

        public static MillionYenFormatter GetInstance() => _instance;

        public string Format(string value, PropertyDescription description)
        {
            if (long.TryParse(value, out long l))
            {
                return String.Format("{0:#,0}", l / 1000000);
            }
            else if (double.TryParse(value, out double d))
            {
                return String.Format("{0:#,##0.#}", d / 1000000);
            }
            else
            {
                return value;
            }
        }

    }
}