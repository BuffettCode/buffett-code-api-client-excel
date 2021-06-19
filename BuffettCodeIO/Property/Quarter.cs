using BuffettCodeCommon.Period;
using BuffettCodeCommon.Validator;
using System.Collections.Generic;

namespace BuffettCodeIO.Property

{
    /// <summary>
    /// 財務数値
    /// </summary>
    /// <remarks>
    /// /api/{version}/quarter のレスポンスに対応します。
    /// </remarks>
    public class Quarter : IApiResource
    {
        private readonly string ticker;
        private readonly FiscalQuarterPeriod period;
        private readonly PropertyDictionary properties;
        private readonly PropertyDescriptionDictionary descriptions;

        public string Ticker => ticker;

        public FiscalQuarterPeriod Period => period;

        private Quarter(string ticker, FiscalQuarterPeriod period, PropertyDictionary properties, PropertyDescriptionDictionary descriptions)
        {
            this.ticker = ticker;
            this.period = period;
            this.properties = properties;
            this.descriptions = descriptions;
        }

        public ICollection<string> GetPropertyNames() => properties.Names;

        public string GetValue(string propertyName) => properties.Get(propertyName);
        public PropertyDescription GetDescription(string propertyName) => descriptions.Get(propertyName);

        public static Quarter Create(string ticker, FiscalQuarterPeriod period, PropertyDictionary properties, PropertyDescriptionDictionary descriptions)
        {
            JpTickerValidator.Validate(ticker);
            return new Quarter(ticker, period, properties, descriptions);
        }

        public static Quarter Create(string ticker, uint fiscalYear, uint fiscalQuarter, PropertyDictionary properties, PropertyDescriptionDictionary descriptions)
        {
            JpTickerValidator.Validate(ticker);
            var period = FiscalQuarterPeriod.Create(fiscalYear, fiscalQuarter);
            return new Quarter(ticker, period, properties, descriptions);
        }


        public override bool Equals(object obj)
        {
            if (obj is null)
            {
                return false;
            }
            else if (this.GetType() != obj.GetType())
            {
                return false;
            }
            else
            {
                var q = (Quarter)obj;
                return this.ticker.Equals(q.ticker)
                    && this.period.Equals(q.period)
                    && this.properties.Equals(q.properties)
                    && this.descriptions.Equals(q.descriptions);
            }
        }

        public override int GetHashCode() => (ticker, period, properties, descriptions).GetHashCode();

    }
}