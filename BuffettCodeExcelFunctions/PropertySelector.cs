using BuffettCodeIO.Formatter;
using BuffettCodeIO.Property;
using BuffettCodeCommon.Config;

namespace BuffettCodeExcelFunctions
{
    public class PropertySelector
    {
        public static string SelectFormattedValue(string propertyName, IApiResource apiResource, bool isRawValue = false, bool isWithUnit = false)
        {
            string rawValue = apiResource.GetValue(propertyName);
            if (isRawValue)
            {
                return rawValue;
            }

            var description = apiResource.GetDescription(propertyName);

            // 定義済みの「円」単位の数値は 100万円に変換する
            if (DefaultUnitConfig.MillionYenProperties.Contains(description.Name) && description.Unit.Equals("円"))
            {
                description = new PropertyDescription(description.Name, description.JpName, "百万円");
            } 

            var formatter = PropertyFormatterFactory.Create(description);
            string formattedValue = formatter.Format(rawValue, description);
            if (isWithUnit)
            {
                formattedValue += description.Unit;
            }
            return formattedValue;
        }

        public static PropertyDescription SelectDescription(string propertyName, IApiResource apiResource)
        {
            return apiResource.GetDescription(propertyName);
        }

    }
}