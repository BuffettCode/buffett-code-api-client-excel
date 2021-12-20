using BuffettCodeCommon.Config;
using BuffettCodeIO.Formatter;
using BuffettCodeIO.Property;

namespace BuffettCodeExcelFunctions
{
    public class PropertySelector
    {
        public static string SelectFormattedValue(string propertyName, IApiResource apiResource, bool isRawValue = false, bool isWithUnit = false, bool useDefaultUnit = false)
        {
            string rawValue = apiResource.GetValue(propertyName);

            var description = apiResource.GetDescription(propertyName);

            if (isRawValue)
            {
                return isWithUnit ? rawValue + description.Unit : rawValue;
            }
            else
            {

                if (useDefaultUnit)
                {
                    // 定義済みの「円」単位の数値は 100万円に変換する
                    if (DefaultUnitConfig.MillionYenProperties.Contains(description.Name) && description.Unit.Equals("円"))
                    {
                        description = new PropertyDescription(description.Name, description.JpName, "百万円");
                    }
                }

                var formatter = PropertyFormatterFactory.Create(description);
                string formattedValue = formatter.Format(rawValue, description);

                return isWithUnit ? formattedValue + description.Unit : formattedValue;
            }
        }
    }
}