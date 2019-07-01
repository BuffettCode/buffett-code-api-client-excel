using System;

namespace BuffettCodeAddin.UnitTests
{
    /// <summary>
    /// 単体テストのサポートクラス
    /// </summary>
    class BuffettCodeTestUtils
    {
        /// <summary>
        /// APIキーを返します。
        /// </summary>
        /// <returns>APIキー</returns>
        public static string GetValidApiKey()
        {
            var key = Environment.GetEnvironmentVariable("BCODE_KEY");
            if (string.IsNullOrEmpty(key))
            {
                Configuration.Reload();
                return Configuration.ApiKey;
            }
            else
            {
                return key;
            }
        }

        /// <summary>
        /// 試用版向けに提供しているAPIキーを返します。
        /// </summary>
        /// <returns>APIキー</returns>
        public static string GetTestApiKey()
        {
            // 広く公開しているAPIキーのためソースコードにもそのまま記載する
            return "sAJGq9JH193KiwnF947v74KnDYkO7z634LWQQfPY";
        }

        /// <summary>
        /// 不正なAPIキーを返します。
        /// </summary>
        /// <returns>不正なAPIキー</returns>
        public static string GetInvalidApiKey()
        {
            return "invalidApiKey!!!";
        }
    }
}
