namespace BuffettCodeAPIAdapter.Formatter
{
    /// <summary>
    /// フォーマッタファクトリ
    /// </summary>
    public static class FormatterFactory
    {
        /// <summary>
        /// 使用すべきフォーマッタを返します。
        /// </summary>
        /// <param name="description">項目定義</param>
        /// <returns>フォーマッタ</returns>
        public static IFormatter Create(PropertyDescrption description = null)
        {
            if (description == null)
            {
                return InactionFormatter.GetInstance();
            }

            switch (description.Unit)
            {
                case "円":
                case "百万円":
                    return CurrencyFormatter.GetInstance();
                case "%":
                    return RatioFormatter.GetInstance();
                case "株":
                case "倍":
                case "日":
                case "ヶ月":
                case "人":
                    return NumericFormatter.GetInstance();
                case "年":
                default:
                    return InactionFormatter.GetInstance();
            }
        }
    }
}
