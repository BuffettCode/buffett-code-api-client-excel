using BuffettCodeAPIClient;
using BuffettCodeIO.Formatter;
using BuffettCodeIO.Processor;
using BuffettCodeIO.Resolver;

namespace BuffettCodeIO
{
    /// <summary>
    /// バフェットコードAPI
    /// </summary>
    /// <remarks>
    /// バフェットコードのWeb APIへのアクセスを抽象化するクラス。
    /// 銘柄コード、項目名、付随するパラメタから値や定義を取得します。
    /// 値はパラメタおよび定義に従ってフォーマットされます（カンマ区切りや金額の桁など）。
    /// </remarks>
    public class BuffettCodeApiTaskProcessor
    {
        private readonly BuffettCodeApiV2Client client;

        private readonly IAPIResolver resolver;

        private readonly ITaskProcessor<string> processor;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="maxDegreeOfParallelism">APIコールの最大同時実行数</param>
        public BuffettCodeApiTaskProcessor(string apiKey, int maxDegreeOfParallelism)
        {
            client = BuffettCodeApiV2Client.GetInstance(apiKey);
            resolver = APIResolverFactory.Create();
            processor = new SemaphoreTaskProcessor<string>(maxDegreeOfParallelism);
        }
        /// <summary>
        /// 全てのキャッシュをクリアします。
        /// </summary>
        public void ClearCache()
        {
            client.ClearCache();
        }

        /// <summary>
        /// 財務数値、指標などの値を取得します。
        /// </summary>
        /// <param name="ticker">銘柄コード</param>
        /// <param name="parameter1">パラメタ1</param>
        /// <param name="parameter2">パラメタ2</param>
        /// <param name="propertyName">項目名</param>
        /// <param name="isRawValue">rawデータフラグ</param>
        /// <param name="isPostfixUnit">単位フラグ</param>
        /// <returns>値</returns>
        public string GetValue(string ticker, string parameter1, string parameter2, string propertyName, bool isRawValue = false, bool isPostfixUnit = false)
        {
            var aggregation = GetAggregation(ticker, parameter1, parameter2, propertyName);
            string rawValue = aggregation.GetValue(propertyName);
            if (isRawValue)
            {
                return rawValue;
            }

            var description = aggregation.GetDescription(propertyName);
            var formatter = FormatterFactory.Create(description);
            string formattedValue = formatter.Format(rawValue, description);
            if (isPostfixUnit)
            {
                formattedValue += description.Unit;
            }
            return formattedValue;
        }

        /// <summary>
        /// 項目定義を取得します。
        /// </summary>
        /// <param name="ticker">銘柄コード</param>
        /// <param name="parameter1">パラメタ1</param>
        /// <param name="parameter2">パラメタ2</param>
        /// <param name="propertyName">項目名</param>
        /// <returns>項目定義</returns>
        public PropertyDescrption GetDescription(string ticker, string parameter1, string parameter2, string propertyName)
        {
            var aggregation = GetAggregation(ticker, parameter1, parameter2, propertyName);
            return aggregation.GetDescription(propertyName);
        }

        private IPropertyAggregation GetAggregation(string ticker, string parameter1, string parameter2, string propertyName)
        {
            switch (resolver.Resolve(propertyName))
            {
                case APIType.Quarter:
                    return GetQuarter(ticker, parameter1, parameter2);
                case APIType.Indicator:
                    return GetIndicator(ticker);
                default:
                    throw new ResolveAPIException();
            }
        }

        private Indicator GetIndicator(string ticker)
        {
            var task = client.GetIndicator(ticker, true);
            string json = processor.Process(task);
            return Indicator.Parse(ticker, json)[0];
        }

        private Quarter GetQuarter(string ticker, string fiscalYear, string fiscalQuarter)
        {

            var task = client.GetQuarter(ticker, uint.Parse(fiscalYear), uint.Parse(fiscalQuarter), false);
            string json = processor.Process(task);
            return Quarter.Parse(ticker, json)[0];
        }
    }

}