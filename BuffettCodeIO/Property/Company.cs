using BuffettCodeCommon.Period;
using BuffettCodeCommon.Validator;
using System.Collections.Generic;

namespace BuffettCodeIO.Property
{
    public class Company : IApiResource
    {
        private readonly string ticker;
        private readonly SupportedTierRange<FiscalQuarterPeriod> supportedQuarterRanges;

        private readonly SupportedTierRange<DayPeriod> supportedDayRanges;
        private readonly PropertyDictionary properties;
        private readonly PropertyDescriptionDictionary descriptions;

        private Company(string ticker,
            PeriodRange<FiscalQuarterPeriod> fixedTierQuarterRange,
            PeriodRange<FiscalQuarterPeriod> ondemandTierQuarterRange,
            PeriodRange<DayPeriod> fixedTierDayRange,
            PeriodRange<DayPeriod> ondemandTierDayRange,
            PropertyDictionary properties,
            PropertyDescriptionDictionary descriptions)
        {
            this.ticker = ticker;
            this.supportedQuarterRanges = new SupportedTierRange<FiscalQuarterPeriod>(fixedTierQuarterRange, ondemandTierQuarterRange);
            this.supportedDayRanges = new SupportedTierRange<DayPeriod>(fixedTierDayRange, ondemandTierDayRange);
            this.properties = properties;
            this.descriptions = descriptions;
        }
        public string GetValue(string propertyName) => properties.Get(propertyName);
        public PropertyDescription GetDescription(string propertyName) => descriptions.Get(propertyName);

        public static Company Create(
            string ticker,
            PeriodRange<FiscalQuarterPeriod> fixedTierQuarterRange,
            PeriodRange<FiscalQuarterPeriod> ondemandQuarterTierRange,
            PeriodRange<DayPeriod> fixedTierDayRange,
            PeriodRange<DayPeriod> ondemandTierDayRange,
            PropertyDictionary properties,
            PropertyDescriptionDictionary descriptions)
        {
            JpTickerValidator.Validate(ticker);
            return new Company(ticker, fixedTierQuarterRange, ondemandQuarterTierRange, fixedTierDayRange, ondemandTierDayRange, properties, descriptions);
        }


        public ICollection<string> GetPropertyNames() => properties.Names;

        public IPeriod GetPeriod() => Snapshot.GetInstance();
        public string Ticker => ticker;

        public SupportedTierRange<FiscalQuarterPeriod> SupportedQuarterRanges => supportedQuarterRanges;

        public SupportedTierRange<DayPeriod> SupportedDailyRanges => supportedDayRanges;
    }

}