using BuffettCodeAPIClient;
using BuffettCodeCommon;

namespace BuffettCodeAddinRibbon
{
    /// <summary>
    /// XLLが主体となる機能やデータを抽象化するためのfacadeクラス
    /// </summary>
    /// <remarks>
    /// BuffettCodeAddinの提供するインタフェースがまだrobustでないので、
    /// 間に1つ挟んでおくために作成したもの
    /// </remarks>
    class AddinFacade
    {
        private static readonly Configuration config = Configuration.GetInstance();

        private static readonly BuffettCodeApiV2Client apiClient = BuffettCodeApiV2Client.GetInstance(config.ApiKey);

        /// <summary>
        /// APIキーを取得します。
        /// </summary>
        /// <returns>APIキー</returns>

        public static string GetApiKey() => config.ApiKey;

        /// <summary>
        /// APIキーを更新します。
        /// </summary>
        /// <param name="apiKey">APIキー</param>
        public static void UpdateApiKey(string apiKey)
        {
            config.ApiKey = apiKey;
        }

        /// <summary>
        /// APIコールの最大同時実行数を取得します。
        /// </summary>
        /// <returns>APIコールの最大同時実行数</returns>
        public static int GetMaxDegreeOfParallelism() => config.MaxDegreeOfParallelism;


        /// <summary>
        /// APIコールの最大同時実行数を更新します。
        /// </summary>
        /// <param name="maxDegreeOfParallelism">APIコールの最大同時実行数</param>
        public static void UpdateMaxDegreeOfParallelism(int maxDegreeOfParallelism)
        {
            config.MaxDegreeOfParallelism = maxDegreeOfParallelism;
        }

        /// <summary>
        /// デバッグモードかどうかを取得します。
        /// </summary>
        /// <returns>デバッグモードかどうか</returns>
        public static bool IsDebugMode() => config.DebugMode;


        /// <summary>
        /// デバッグモードかどうかを更新します。
        /// </summary>
        /// <param name="debugMode">デバッグモードかどうか</param>
        public static void UpdateDebugMode(bool debugMode)
        {
            config.DebugMode = debugMode;
        }

        /// <summary>
        /// キャッシュをクリアします。
        /// </summary>
        public static void ClearCache()
        {
            apiClient.ClearCache();
        }

        public static BuffettCodeApiV2Client GetApiClient()
        {
            // update api key 
            apiClient.UpdateApiKey(config.ApiKey);
            return apiClient;
        }
    }
}
