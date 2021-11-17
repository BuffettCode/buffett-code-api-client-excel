using BuffettCodeCommon.Period;
using BuffettCodeCommon.Validator;
using System.Collections.Generic;


namespace BuffettCodeIO.Property
{
    public class Daily : IApiResource
    {
        private readonly string ticker;
        private readonly DayPeriod period;
        private readonly PropertyDictionary properties;
        private readonly PropertyDescriptionDictionary descriptions;

        private Daily(string ticker, DayPeriod period, PropertyDictionary properties, PropertyDescriptionDictionary descriptions)
        {
            this.ticker = ticker;
            this.period = period;
            this.properties = properties;
            this.descriptions = descriptions;
        }

        public DayPeriod Period => period;

        public PropertyDescription GetDescription(string propertyName) => descriptions.Get(propertyName);

        public IPeriod GetPeriod() => period;

        public string Ticker => ticker;

        public ICollection<string> GetPropertyNames() => properties.Names;
        public string GetValue(string propertyName) => properties.Get(propertyName);
        public static Daily Create(string ticker, DayPeriod period, PropertyDictionary properties, PropertyDescriptionDictionary descriptions)
        {
            JpTickerValidator.Validate(ticker);
            return new Daily(ticker, period, properties, descriptions);
        }
    }
}