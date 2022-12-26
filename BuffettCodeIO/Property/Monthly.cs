using BuffettCodeCommon.Period;
using BuffettCodeCommon.Validator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuffettCodeIO.Property
{
    public class Monthly : IApiResource
    {
        private static readonly Dictionary<string, string> ITEM_NAME_ALIASES = new Dictionary<string, string>
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
        };

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

        public PropertyDescription GetDescription(string propertyName)
        {
            if (ITEM_NAME_ALIASES.ContainsKey(propertyName))
            {
                return descriptions.Get(ITEM_NAME_ALIASES[propertyName]);

            }
            else
            {
                return descriptions.Get(propertyName);
            }
        }

        public IIntent GetPeriod() => period;

        public string Ticker => ticker;


        public ICollection<string> GetPropertyNames() => properties.Names;

        public string GetValue(string propertyName)
        {
            if (ITEM_NAME_ALIASES.ContainsKey(propertyName))
            {
                return properties.Get(ITEM_NAME_ALIASES[propertyName]);

            }
            else
            {
                return properties.Get(propertyName);
            }

        }
        public static Monthly Create(string ticker, uint year, uint month, PropertyDictionary properties, PropertyDescriptionDictionary descriptions)
        {
            JpTickerValidator.Validate(ticker);
            var period = YearMonthPeriod.Create(year, month);
            return new Monthly(ticker, period, properties, descriptions);
        }
    }
}