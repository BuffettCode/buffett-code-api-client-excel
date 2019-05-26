using BuffettCodeAddin;

namespace BuffettCode
{
    /// <summary>
    /// XLL Addinが主体となる機能やデータを抽象化するためのクラス
    /// </summary>
    class AddinFacade
    {
        public static string GetApiKey()
        {
            Configuration.Reload();
            return Configuration.ApiKey;
        }

        public static void UpdateApiKey(string apiKey)
        {
            Configuration.ApiKey = apiKey;
        }

        public static void ClearCache()
        {
            Configuration.ClearCache = true;
        }
    }
}
