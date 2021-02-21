using BuffettCodeAPIAdapter;

namespace BuffettCode
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
        /// <summary>
        /// APIキーを取得します。
        /// </summary>
        /// <returns>APIキー</returns>
        public static string GetApiKey()
        {
            Configuration.Reload();
            return Configuration.ApiKey;
        }

        /// <summary>
        /// APIキーを更新します。
        /// </summary>
        /// <param name="apiKey">APIキー</param>
        public static void UpdateApiKey(string apiKey)
        {
            Configuration.ApiKey = apiKey;
        }

        /// <summary>
        /// APIコールの最大同時実行数を取得します。
        /// </summary>
        /// <returns>APIコールの最大同時実行数</returns>
        public static int GetMaxDegreeOfParallelism()
        {
            Configuration.Reload();
            return Configuration.MaxDegreeOfParallelism;
        }

        /// <summary>
        /// APIコールの最大同時実行数を更新します。
        /// </summary>
        /// <param name="maxDegreeOfParallelism">APIコールの最大同時実行数</param>
        public static void UpdateMaxDegreeOfParallelism(int maxDegreeOfParallelism)
        {
            Configuration.MaxDegreeOfParallelism = maxDegreeOfParallelism;
        }

        /// <summary>
        /// デバッグモードかどうかを取得します。
        /// </summary>
        /// <returns>デバッグモードかどうか</returns>
        public static bool IsDebugMode()
        {
            Configuration.Reload();
            return Configuration.DebugMode;
        }

        /// <summary>
        /// デバッグモードかどうかを更新します。
        /// </summary>
        /// <param name="debugMode">デバッグモードかどうか</param>
        public static void UpdateDebugMode(bool debugMode)
        {
            Configuration.DebugMode = debugMode;
        }

        /// <summary>
        /// キャッシュをクリアします。
        /// </summary>
        public static void ClearCache()
        {
            Configuration.ClearCache = true;
        }
    }
}
