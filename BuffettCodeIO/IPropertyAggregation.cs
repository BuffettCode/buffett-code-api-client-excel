namespace BuffettCodeIO
{
    /// <summary>
    /// 項目の集約
    /// </summary>
    /// <remarks>
    /// バフェットコードのAPIが提供する項目を集めたもの。
    /// 基本的にAPIのレスポンスに対応します。
    /// </remarks>
    public interface IPropertyAggregation
    {
        /// <summary>
        /// 自身を一意に特定する文字列を返します。
        /// </summary>
        /// <returns></returns>
        string GetIdentifier();

        /// <summary>
        /// 値を返します。
        /// </summary>
        /// <param name="propertyName">項目名</param>
        /// <returns>値</returns>
        string GetValue(string propertyName);

        /// <summary>
        /// 項目定義を返します。
        /// </summary>
        /// <param name="propertyName">項目名</param>
        /// <returns>項目定義</returns>
        PropertyDescrption GetDescription(string propertyName);
    }
}