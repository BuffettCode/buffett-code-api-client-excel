namespace BuffettCodeIO
{
    /// <summary>
    /// 指定されたパラメタに対して<see cref="IPropertyAggregation"/>を取得できなかったときにthrowされる例外
    /// </summary>
    /// <remarks>
    /// 基本的にユーザの入力したパラメタのミス(存在しない銘柄コードを指定した場合など)が原因で起こるはず
    /// </remarks>
    public class AggregationNotFoundException : BuffettCodeException
    {
    }
}
