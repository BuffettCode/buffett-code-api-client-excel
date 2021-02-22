using System.Threading.Tasks;

namespace BuffettCodeIO.Client
{
    /// <summary>
    /// バフェットコードWebAPIクライアント
    /// </summary>
    /// <remarks>
    /// バフェットコードのWebAPIをコールして結果をJSONの文字列で返すクライアント。
    /// APIコールは非同期で行われます。
    /// バフェットコードのWebAPIにはバージョンがあるため、インタフェースを切って各バージョンのクラスを派生させています。
    /// </remarks>
    public interface IBuffettCodeClient
    {
        /// <summary>
        /// 四半期ごとの財務数値を取得します。
        /// </summary>
        /// <param name="apiKey">APIキー</param>
        /// <param name="ticker">銘柄コード</param>
        /// <param name="fiscalYear">会計年度</param>
        /// <param name="fiscalQuarter">会計四半期</param>
        /// <param name="isConfigureAwait">クラス内部のTask呼び出しで<c>.ConfigureAwait(true)</c>するかどうか</param>
        /// <returns>JSON文字列</returns>
        Task<string> GetQuarter(string apiKey, string ticker, string fiscalYear, string fiscalQuarter, bool isConfigureAwait = true);

        /// <summary>
        /// 四半期ごとの財務数値を範囲指定で取得します。
        /// </summary>
        /// <param name="apiKey">APIキー</param>
        /// <param name="ticker">銘柄コード</param>
        /// <param name="from">対象四半期の始点(e.g. 2018Q1)</param>
        /// <param name="to">対象四半期の終点(e.g. 2018Q1)</param>
        /// <param name="isConfigureAwait">クラス内部のTask呼び出しで<c>.ConfigureAwait(true)</c>するかどうか</param>
        /// <returns>JSON文字列</returns>
        Task<string> GetQuarterRange(string apiKey, string ticker, string from, string to, bool isConfigureAwait = true);

        /// <summary>
        /// 指標を取得します。
        /// </summary>
        /// <param name="apiKey">APIキー</param>
        /// <param name="ticker">銘柄コード</param>
        /// <param name="isConfigureAwait"></param>
        /// <param name="isConfigureAwait">クラス内部のTask呼び出しで<c>.ConfigureAwait(true)</c>するかどうか</param>
        /// <returns>JSON文字列</returns>
        Task<string> GetIndicator(string apiKey, string ticker, bool isConfigureAwait = true);
    }
}
