using BuffettCodeCommon.Period;
using BuffettCodeCommon.Validator;
using System.Collections.Generic;


namespace BuffettCodeIO.Property
{
    public class Monthly : IApiResource
    {
        private readonly string ticker;
        private readonly YearMonthPeriod period;
        private readonly PropertyDictionary properties;
        private readonly PropertyDescriptionDictionary descriptions;


        private Monthly(string ticker, YearMonthPeriod period, PropertyDictionary properties, PropertyDescriptionDictionary descriptions)
        {
            this.ticker = ticker;
            this.period = period;
            this.properties = properties;
            this.descriptions = descriptions;
        }

        public PropertyDescription GetDescription(string propertyName) => descriptions.Get(propertyName);

        public IIntent GetPeriod() => period;

        public string Ticker => ticker;


        public ICollection<string> GetPropertyNames() => properties.Names;

        public string GetValue(string propertyName) => properties.Get(propertyName);

        public static Monthly Create(string ticker, uint year, uint month, PropertyDictionary properties, PropertyDescriptionDictionary descriptions)
        {
            JpTickerValidator.Validate(ticker);
            var period = YearMonthPeriod.Create(year, month);
            return new Monthly(ticker, period, properties, descriptions);
        }
    }
}