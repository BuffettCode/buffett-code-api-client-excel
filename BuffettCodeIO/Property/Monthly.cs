using BuffettCodeCommon.Period;
using BuffettCodeCommon.Validator;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuffettCodeIO.Property
{
    public class Monthly : IApiResource
    {
        private static readonly ReadOnlyDictionary<string, string> ITEM_NAME_ALIASES = new ReadOnlyDictionary<string, string>(new Dictionary<string, string>
        {
            {"2y_beta", "beta.years_2.beta"},
            {"3y_beta", "beta.years_3.beta"},
            {"5y_beta", "beta.years_5.beta"},
            {"2y_beta_r2", "beta.years_2.r_squared"},
            {"3y_beta_r2", "beta.years_2.r_squared"},
            {"5y_beta_r2", "beta.years_2.r_squared"},
            {"2y_beta_count", "beta.years_2.count"},
            {"3y_beta_count", "beta.years_3.count"},
            {"5y_beta_count", "beta.years_5.count"}
        });

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

        private string GetKeyName(string propertyName)
        {
            if (ITEM_NAME_ALIASES.ContainsKey(propertyName))
            {
                return ITEM_NAME_ALIASES[propertyName];
            }
            else
            {
                return propertyName;
            }
        }

        public PropertyDescription GetDescription(string propertyName) => descriptions.Get(GetKeyName(propertyName));

        public IIntent GetPeriod() => period;

        public string Ticker => ticker;


        public ICollection<string> GetPropertyNames() => properties.Names;

        public string GetValue(string propertyName) => properties.Get(GetKeyName(propertyName));

        public static Monthly Create(string ticker, uint year, uint month, PropertyDictionary properties, PropertyDescriptionDictionary descriptions)
        {
            JpTickerValidator.Validate(ticker);
            var period = YearMonthPeriod.Create(year, month);
            return new Monthly(ticker, period, properties, descriptions);
        }
    }
}