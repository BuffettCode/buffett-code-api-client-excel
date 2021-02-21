namespace BuffettCodeAPIAdapter.Resolver
{
    /// <summary>
    /// APIリゾルバファクトリ
    /// </summary>
    public static class APIResolverFactory
    {
        /// <summary>
        /// APIリゾルバを返します
        /// </summary>
        /// <returns>APIリゾルバ</returns>
        public static IAPIResolver Create()
        {
            bool isInitialized = WebResourceAPIResolver.IsInitialized();
            if (isInitialized)
            {
                return WebResourceAPIResolver.GetInstance();
            }
            else
            {
                return ConstAPIResolver.GetInstance();
            }
        }
    }
}
