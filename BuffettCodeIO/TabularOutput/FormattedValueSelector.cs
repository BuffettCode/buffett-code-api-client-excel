using BuffettCodeIO.Formatter;
using BuffettCodeIO.Property;

namespace BuffettCodeIO.TabluarOutput
{
    public class FormattedValueSelector
    {
        private readonly IApiResource apiResource;

        public FormattedValueSelector(IApiResource apiResource)
        {
            this.apiResource = apiResource;
        }
        public string Select(string propertyName)
        {
            var desc = apiResource.GetDescription(propertyName);
            var rawValue = apiResource.GetValue(propertyName);
            return PropertyFormatterFactory.Create(desc).Format(rawValue, desc);
        }
    }
}