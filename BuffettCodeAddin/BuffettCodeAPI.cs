using BuffettCodeAddin.Client;
using BuffettCodeAddin.Formatter;

namespace BuffettCodeAddin
{
    public class BuffettCodeAPI
    {
        private readonly IBuffettCodeClient client;

        private readonly CacheStore cache;

        public BuffettCodeAPI()
        {
            client = new BuffettCodeClientV2();
            cache = new CacheStore();
        }

        public void ClearCache()
        {
            cache.ClearAllCache();
        }

        public string GetValue(string ticker, string parameter1, string parameter2, string propertyName, bool isRawValue = false, bool isPostfixUnit = false)
        {
            var aggregation = GetAggregation(ticker, parameter1, parameter2, propertyName);
            string rawValue = aggregation.GetValue(propertyName);
            if (isRawValue)
            {
                return rawValue;
            }

            var description = aggregation.GetDescription(propertyName);
            var formatter = FormatterFactory.Create(rawValue, description);
            string formattedValue = formatter.Format(rawValue, description);
            if (isPostfixUnit)
            {
                formattedValue += description.Unit;
            }
            return formattedValue;
        }

        public PropertyDescrption GetDescription(string ticker, string parameter1, string parameter2, string propertyName)
        {
            var aggregation = GetAggregation(ticker, parameter1, parameter2, propertyName);
            return aggregation.GetDescription(propertyName);
        }

        private IPropertyAggregation GetAggregation(string ticker, string parameter1, string parameter2, string propertyName)
        {
            if (APIResolver.IsIndicator(propertyName))
            {
                return GetIndicator(ticker);
            }
            else
            {
                return GetQuarter(ticker, parameter1, parameter2);
            }
        }

        private Indicator GetIndicator(string ticker)
        {
            if (!cache.HasIndicator(ticker))
            {
                var task = client.GetIndicator(Configuration.ApiKey, ticker);
                string json = task.Result;
                cache.Add(Indicator.parse(ticker, json));
            }

            return cache.GetIndicator(ticker);
        }

        private Quarter GetQuarter(string ticker, string fiscalYear, string fiscalQuarter)
        {
            if (!cache.HasQuarter(ticker, fiscalYear, fiscalQuarter))
            {
                var task = client.GetQuarter(Configuration.ApiKey, ticker, fiscalYear, fiscalQuarter);
                string json = task.Result;
                cache.Add(Quarter.parse(ticker, json));
            }

            return cache.GetQuarter(ticker, fiscalYear, fiscalQuarter);
        }
    }
}
