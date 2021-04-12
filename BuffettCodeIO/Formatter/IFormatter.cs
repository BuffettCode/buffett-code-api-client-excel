namespace BuffettCodeIO.Formatter
{
    /// <summary>
    /// バフェットコードのWebAPIから取得した値のフォーマッタ
    /// </summary>
    /// <remarks>
    /// バフェットコードのWebAPIから取得した値は、
    /// たとえば売上高などの金額であれば<c>48000000000</c>、
    /// PERなどのマルチプルであれば<c>9.262569903846154</c>など、
    /// DBから取得した値そのままで、財務データの一般的な表記ではありません。
    /// そこで、項目定義を参照しながら適切な表記に直す機能をそれぞれの派生クラスに持たせています。
    /// </remarks>
    public interface IFormatter
    {
        /// <summary>
        /// 値をフォーマットします。
        /// </summary>
        /// <param name="value">値</param>
        /// <param name="description">項目定義</param>
        /// <returns>フォーマットされた値</returns>
        string Format(string value, PropertyDescrption description);
    }
}