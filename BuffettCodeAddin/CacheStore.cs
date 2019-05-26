using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;

namespace BuffettCodeAddin
{
    class CacheStore
    {
        private readonly ConcurrentDictionary<string, Quarter> quarterCache;

        private readonly ConcurrentDictionary<string, Indicator> indicatorCache;

        public CacheStore()
        {
            quarterCache = new ConcurrentDictionary<string, Quarter>();
            indicatorCache = new ConcurrentDictionary<string, Indicator>();
        }

        public void Add(IList<Quarter> quarters)
        {
            quarters.ToList().ForEach(q => quarterCache.TryAdd(q.GetIdentifier(), q));
        }
        public void Add(IList<Indicator> indicators)
        {
            indicators.ToList().ForEach(q => indicatorCache.TryAdd(q.GetIdentifier(), q));
        }

        public void ClearAllCache()
        {
            quarterCache.Clear();
            indicatorCache.Clear();
        }

        public bool HasQuarter(string ticker, string fiscalYear, string fiscalQuarter)
        {
            string identifier = Quarter.GetIdentifier(ticker, int.Parse(fiscalYear), int.Parse(fiscalQuarter));
            return quarterCache.ContainsKey(identifier);
        }

        public Quarter GetQuarter(string ticker, string fiscalYear, string fiscalQuarter)
        {
            string identifier = Quarter.GetIdentifier(ticker, int.Parse(fiscalYear), int.Parse(fiscalQuarter));
            return quarterCache[identifier];
        }

        public bool HasIndicator(string ticker)
        {
            return indicatorCache.ContainsKey(ticker);
        }
        public Indicator GetIndicator(string ticker)
        {
            return indicatorCache[ticker];
        }
    }
}
