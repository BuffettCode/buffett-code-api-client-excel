namespace BuffettCodeIO.Resolver
{
    /// <summary>
    /// APIリゾルバ
    /// </summary>
    /// <remarks>
    /// ExcelアドインではBCODEという関数1つで全ての値を取れるようにしていますが、
    /// バフェットコードでは複数のAPIを提供しており、取得したい値によって使用するAPIが異なります。
    /// BCODE関数で指定された項目名に対してどのAPIを使用すべきか決定する機能をこのインタフェースに持たせています。
    /// </remarks>
    public interface IAPIResolver
    {
        /// <summary>
        /// 使用すべきAPIを決定します。
        /// </summary>
        /// <param name="propertyName">項目名</param>
        /// <returns>APIType</returns>
        APIType Resolve(string propertyName);
    }
}