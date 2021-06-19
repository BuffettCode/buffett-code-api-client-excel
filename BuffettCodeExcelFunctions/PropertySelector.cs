using BuffettCodeIO.Formatter;
using BuffettCodeIO.Property;

namespace BuffettCodeExcelFunctions
{
    class PropertySelector
    {
        public static string SelectFormattedValue(string propertyName, IApiResource apiResource, bool isRawValue = false, bool isPostfixUnit = false)
        {
            string rawValue = apiResource.GetValue(propertyName);
            if (isRawValue)
            {
                return rawValue;
            }

            var description = apiResource.GetDescription(propertyName);
            var formatter = PropertyFormatterFactory.Create(description);
            string formattedValue = formatter.Format(rawValue, description);
            if (isPostfixUnit)
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