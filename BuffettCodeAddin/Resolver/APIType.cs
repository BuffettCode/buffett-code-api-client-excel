namespace BuffettCodeAddin.Resolver
{
    /// <summary>
    /// バフェットコードAPI
    /// </summary>
    public enum APIType
    {
        /// <summary>
        /// 財務数値
        /// (/api/{version}/quarter)
        /// </summary>
        Quarter,

        /// <summary>
        /// インディケータ
        /// (/api/{version}/indicator)
        /// </summary>
        Indicator
    }
}
