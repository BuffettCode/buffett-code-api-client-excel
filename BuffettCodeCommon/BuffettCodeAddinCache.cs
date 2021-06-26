using System.Runtime.Caching;


namespace BuffettCodeCommon
{
    static public class BuffettCodeAddinCache
    {
        private static readonly MemoryCache cache = new MemoryCache("BuffettCodeExcelAddin");

        public static MemoryCache GetInstance() => cache;
        public static void Dispose() => cache.Dispose();
    }
}