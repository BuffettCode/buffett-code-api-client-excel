using BuffettCodeCommon.Period;
using BuffettCodeCommon.Validator;
using System.Collections.Generic;

namespace BuffettCodeIO.Property
{
    public class Company : IApiResource
    {
        private readonly string ticker;
        private readonly SupportedTierRange<FiscalQuarterPeriod> supportedQuarterRanges;
        private readonly PropertyDictionary properties;
        private readonly PropertyDescriptionDictionary descriptions;

        private Company(string ticker,
            PeriodRange<FiscalQuarterPeriod> fixedTireRange,
            PeriodRange<FiscalQuarterPeriod> ondemandTireRange,
            PropertyDictionary properties,
            PropertyDescriptionDictionary descriptions)
        {
            this.ticker = ticker;
            this.supportedQuarterRanges = new SupportedTierRange<FiscalQuarterPeriod>(fixedTireRange, ondemandTireRange);
            this.properties = properties;
            this.descriptions = descriptions;
        }
        public string GetValue(string propertyName) => properties.Get(propertyName);
        public PropertyDescription GetDescription(string propertyName) => descriptions.Get(propertyName);

        public static Company Create(
            string ticker,
            PeriodRange<FiscalQuarterPeriod> flatTierRange,
            PeriodRange<FiscalQuarterPeriod> ondemandTireRange,
            PropertyDictionary properties,
            PropertyDescriptionDictionary descriptions)
        {
            JpTickerValidator.Validate(ticker);
            return new Company(ticker, flatTierRange, ondemandTireRange, properties, descriptions);
        }

        public ICollection<string> GetPropertyNames() => properties.Names;

        public IPeriod GetPeriod() => Snapshot.GetInstance();
        public string Ticker => ticker;

        public SupportedTierRange<FiscalQuarterPeriod> SupportedQuarterRanges => supportedQuarterRanges;
    }

}