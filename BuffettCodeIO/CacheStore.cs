using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;

namespace BuffettCodeIO
{
    /// <summary>
    /// APIレスポンスのキャッシュストア
    /// </summary>
    class CacheStore
    {
        private readonly ConcurrentDictionary<string, Quarter> quarterCache;

        private readonly ConcurrentDictionary<string, Indicator> indicatorCache;

        public CacheStore()
        {
            quarterCache = new ConcurrentDictionary<string, Quarter>();
            indicatorCache = new ConcurrentDictionary<string, Indicator>();
        }

        /// <summary>
        /// <see cref="Quarter"/>をキャッシュに追加します。
        /// </summary>
        /// <param name="quarter"></param>
        public void Add(Quarter quarter)
        {
            quarterCache.TryAdd(quarter.GetIdentifier(), quarter);
        }

        /// <summary>
        /// <see cref="Indicator"/>をキャッシュに追加します。
        /// </summary>
        /// <param name="indicator"></param>
        public void Add(Indicator indicator)
        {
            indicatorCache.TryAdd(indicator.GetIdentifier(), indicator);
        }

        /// <summary>
        /// <see cref="Quarter"/>をキャッシュに追加します。
        /// </summary>
        /// <param name="quarters"></param>
        public void Add(IEnumerable<Quarter> quarters)
        {
            quarters.ToList().ForEach(q => Add(q));
        }

        /// <summary>
        /// <see cref="Indicator"/>をキャッシュに追加します。
        /// </summary>
        /// <param name="indicators"></param>
        public void Add(IEnumerable<Indicator> indicators)
        {
            indicators.ToList().ForEach(i => Add(i));
        }

        /// <summary>
        /// 全てのキャッシュをクリアします。
        /// </summary>
        public void ClearAllCache()
        {
            quarterCache.Clear();
            indicatorCache.Clear();
        }

        /// <summary>
        /// <see cref="Quarter"/>がキャッシュされているかを返します。
        /// </summary>
        /// <param name="ticker">銘柄コード</param>
        /// <param name="fiscalYear">会計年度</param>
        /// <param name="fiscalQuarter">会計四半期</param>
        /// <returns>キャッシュされている場合はtrue</returns>
        public bool HasQuarter(string ticker, string fiscalYear, string fiscalQuarter)
        {
            string identifier = Quarter.GetIdentifier(ticker, int.Parse(fiscalYear), int.Parse(fiscalQuarter));
            return quarterCache.ContainsKey(identifier);
        }

        /// <summary>
        /// <see cref="Quarter"/>を取得します。
        /// </summary>
        /// <param name="ticker">銘柄コード</param>
        /// <param name="fiscalYear">会計年度</param>
        /// <param name="fiscalQuarter">会計四半期</param>
        /// <returns><see cref="Quarter"/></returns>
        public Quarter GetQuarter(string ticker, string fiscalYear, string fiscalQuarter)
        {
            string identifier = Quarter.GetIdentifier(ticker, int.Parse(fiscalYear), int.Parse(fiscalQuarter));
            return quarterCache[identifier];
        }

        /// <summary>
        /// <see cref="Indicator"/>がキャッシュされているかを返します。
        /// </summary>
        /// <param name="ticker">銘柄コード</param>
        /// <returns>キャッシュされている場合はtrue</returns>
        public bool HasIndicator(string ticker)
        {
            return indicatorCache.ContainsKey(ticker);
        }

        /// <summary>
        /// <see cref="Indicator"/>を取得します。
        /// </summary>
        /// <param name="ticker">銘柄コード</param>
        /// <returns><see cref="Indicator"/></returns>
        public Indicator GetIndicator(string ticker)
        {
            return indicatorCache[ticker];
        }
    }
}