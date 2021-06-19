using BuffettCodeCommon.Validator;

namespace BuffettCodeIO.Property

{
    public class Indicator : IApiSchema
    {

        private readonly string ticker;
        private readonly PropertyDictionary properties;
        private readonly PropertyDescriptionDictionary descriptions;

        public string Ticker => ticker;

        private Indicator(string ticker, PropertyDictionary properties, PropertyDescriptionDictionary descriptions)
        {
            this.ticker = ticker;
            this.properties = properties;
            this.descriptions = descriptions;
        }

        public static Indicator Create(string ticker, PropertyDictionary properties, PropertyDescriptionDictionary descriptions)
        {
            JpTickerValidator.Validate(ticker);
            return new Indicator(ticker, properties, descriptions);
        }

        public string GetValue(string propertyName) => properties.Get(propertyName);

        public PropertyDescription GetDescription(string propertyName) => descriptions.Get(propertyName);


    }
}