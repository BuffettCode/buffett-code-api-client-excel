namespace BuffettCodeIO.Resolver
{
    /// <summary>
    /// APIリゾルバファクトリ
    /// </summary>
    public static class V2DataTypeResolverFactory
    {
        /// <summary>
        /// APIリゾルバを返します
        /// </summary>
        /// <returns>APIリゾルバ</returns>
        public static IDataTypeResolver Create()
        {
            bool isInitialized = V2WebResourceDataTypeResolver.IsInitialized();
            if (isInitialized)
            {
                return V2WebResourceDataTypeResolver.GetInstance();
            }
            else
            {
                return V2ConstDataTypeResolver.GetInstance();
            }
        }
    }
}