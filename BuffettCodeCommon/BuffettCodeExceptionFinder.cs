using BuffettCodeCommon.Exception;
namespace BuffettCodeCommon
{
    public static class BuffettCodeExceptionFinder
    {

        public static BaseBuffettCodeException Find(System.Exception e)
        {
            System.Exception cursor = e;
            // 例外によってはBuffettCodeExceptionがInnerExceptionに入ってくるので、
            // 再帰的にスキャンして取り出している
            do
            {
                if (cursor is BaseBuffettCodeException bce)
                {
                    return bce;
                }
                cursor = cursor.InnerException;
            } while (cursor != null);
            return null;
        }
    }
}