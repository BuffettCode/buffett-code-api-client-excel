namespace BuffettCodeAddin.Formatter
{
    class FormatterFactory
    {
        public static Formatter Create(string value, PropertyDescrption description = null)
        {
            if (description == null)
            {
                return InactionFormatter.GetInstance();
            }

            switch (description.Unit)
            {
                case "円":
                case "百万円":
                    return CurrencyFormatter.GetInstance();
                case "%":
                    return RatioFormatter.GetInstance();
                case "株":
                case "倍":
                case "日":
                case "ヶ月":
                case "人":
                    return NumericFormatter.GetInstance();
                case "年":
                default:
                    return InactionFormatter.GetInstance();
            }
        }
    }
}
