using BuffettCodeCommon.Period;
using BuffettCodeCommon.Validator;

namespace BuffettCodeIO.Property
{
    public class Company
    {
        private readonly string ticker;
        private readonly PeriodRange<FiscalQuarterPeriod> fixedTierRange;
        private readonly PeriodRange<FiscalQuarterPeriod> ondemandTireRange;
        private readonly PropertyDictionary properties;
        private readonly PropertyDescriptionDictionary descriptions;

        private Company(string ticker,
            PeriodRange<FiscalQuarterPeriod> fixedTireRange,
            PeriodRange<FiscalQuarterPeriod> ondemandtireRange,
            PropertyDictionary properties,
            PropertyDescriptionDictionary descriptions)
        {
            this.ticker = ticker;
            this.fixedTierRange = fixedTireRange;
            this.ondemandTireRange = ondemandtireRange;
            this.properties = properties;
            this.descriptions = descriptions;
        }
        public string GetValue(string propertyName) => properties.Get(propertyName);
        public PropertyDescription GetDescription(string propertyName) => descriptions.Get(propertyName);

        public static Company Create(
            string ticker,
            PeriodRange<FiscalQuarterPeriod> flatTierRange,
            PeriodRange<FiscalQuarterPeriod> ondemandtireRange,
            PropertyDictionary properties,
            PropertyDescriptionDictionary descriptions)
        {
            JpTickerValidator.Validate(ticker);
            return new Company(ticker, flatTierRange, ondemandtireRange, properties, descriptions);
        }

        public string Ticker => ticker;

        public PeriodRange<FiscalQuarterPeriod> FlatTierRange => fixedTierRange;
        public PeriodRange<FiscalQuarterPeriod> OndemandTireRange => ondemandTireRange;
    }

}