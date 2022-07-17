using BuffettCodeCommon;
using BuffettCodeCommon.Exception;

namespace BuffettCodeAddinRibbon.CsvDownload
{
    public class CsvDownloadExceptionHandler
    {

        private static bool IsDebugMode => Configuration.GetInstance().DebugMode;

        public static string ToMessageBoxString(BaseBuffettCodeException exception)
        {
            // if debug mode, return exception
            if (IsDebugMode)
            {
                return $"exception={exception}\nstack_trace={exception.StackTrace}";
            }

            if (exception is TestAPIConstraintException)
            {
                return "テスト用のAPIキーでは末尾が01の銘柄コードのみ使用できます。";
            }
            else if (exception is DailyQuotaException)
            {
                return "一日当たりのAPIの実行回数が上限に達しました。";
            }
            else if (exception is InvalidAPIKeyException)
            {
                return $"APIキーが有効ではありません。";
            }
            else if (exception is ApiResponseParserException)
            {
                return $"APIのレスポンスのパースに失敗しました。";
            }
            else if (exception is NotSupportedTierException)
            {
                return $"取得可能な範囲を超えています。";
            }
            else if (exception is ResourceNotFoundException)
            {
                return $"存在しないデータにアクセスしようとしています。";
            }
            else if (exception is ApiMonthlyLimitExceededException)
            {
                return "今月のAPIの実行回数が上限に達しました。";
            }
            else
            {
                return $"データの取得中にエラーが発生しました。";
            }
        }

    }
}
