using BuffettCodeAddin;

namespace BuffettCode
{
    /// <summary>
    /// XLL Addinが主体となる機能やデータを抽象化するためのクラス
    /// </summary>
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
