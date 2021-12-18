using BuffettCodeIO.Property;
namespace BuffettCodeIO.Formatter
{

    public static class PropertyFormatterFactory
    {
        public static IPropertyFormatter Create(PropertyDescription description = null)
        {
            if (description == null)
            {
                return InactionFormatter.GetInstance();
            }

            switch (description.Unit)
            {
                case "%":
                    return RatioFormatter.GetInstance();
                case "円":
                case "株":
                case "倍":
                case "日":
                case "ヶ月":
                case "人":
                    return NumericFormatter.GetInstance();
                case "百万円":
                    return MillionYenFormatter.GetInstance();
                case "年":
                default:
                    return InactionFormatter.GetInstance();
            }
        }
    }
}