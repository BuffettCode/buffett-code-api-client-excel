namespace BuffettCodeExcelFunctions
{
    using BuffettCodeCommon;
    using BuffettCodeCommon.Config;
    using BuffettCodeCommon.Exception;
    using ExcelDna.Integration;
    using System;

    public class UserDefinedFunctions
    {
        private static readonly Configuration config = Configuration.GetInstance();

        /// <summary>
        /// Excelのユーザー定義関数BCODE。銘柄コードを指定して財務数値や指標を取得します
        /// </summary>
        /// <param name="ticker">銘柄コード</param>
        /// <param name="parameter1">パラメタ1</param>
        /// <param name="parameter2">パラメタ2</param>
        /// <param name="propertyName">項目名</param>
        /// <param name="isRawValue">rawデータフラグ</param>
        /// <param name="isPostfixUnit">単位フラグ</param>
        /// <returns>Excelのセルに表示する文字列</returns>
        [ExcelFunction(Description = "[deprecated] Get indicators, stock prices, and any further values by BuffettCode API", Name = "BCODE")]
        public static string BCodeLegacy(string ticker, string parameter1, string parameter2, string propertyName, bool isRawValue = false, bool isPostfixUnit = false)
        {
            try
            {
                var apiResouce = ApiResourceFetcher.FetchForLegacy(ticker, parameter1, parameter2, propertyName, true, true);
                return PropertySelector.SelectFormattedValue(propertyName, apiResouce, isRawValue, isPostfixUnit);
            }
            catch (Exception e)
            {
                return ToErrorMessage(e, propertyName);
            }
        }


        [ExcelFunction(IsHidden = true, Description = "[DEBUG] Print Api Key in a Registry", Name = "BCODE_API_KEY")]
        public static string PrintApiKeyInRegistry() => config.ApiKey;

        [ExcelFunction(IsHidden = true, Description = "[DEBUG] Check function call (from Excel to XLL)", Name = "BCODE_PING")]
        public static string PrintRandomInteger()
        {
            try
            {
                Random r = new Random();
                return r.Next().ToString();
            }
            catch (Exception e)
            {
                return ToErrorMessage(e);
            }
        }

        private static string ToErrorMessage(Exception e, string propertyName = "")
        {
            System.Diagnostics.Debug.WriteLine(e.StackTrace); // for debug

            // デバッグモードが設定されていたらエラーメッセージの代わりにスタックトレースをセルに表示
            if (config.DebugMode)
            {
                return e.ToString();
            }

            BaseBuffettCodeException bce = BuffettCodeExceptionFinder.Find(e);

            string message;
            if (bce is PropertyNotFoundException)
            {
                message = "指定された項目が見つかりません:" + propertyName;
            }
            else if (bce is QuotaException)
            {
                message = "APIの実行回数が上限に達しました";
            }
            else if (bce is InvalidAPIKeyException)
            {
                message = "APIキーが有効ではありません";
            }
            else if (bce is TestAPIConstraintException)
            {
                message = "テスト用のAPIキーでは取得できないデータです";
            }
            else if (bce is NotSupportedDataTypeException)
            {
                message = "未定義の項目名です";
            }
            else if (bce is ApiResponseParserException)
            {
                message = $"APIレスポンスのパースに失敗しました::{bce.Message}";
            }
            else if (bce is NotSupportedTierException)
            {
                message = $"取得可能な範囲を超えています::{bce.Message}";
            }
            else if (bce is ResourceNotFoundException)
            {
                message = $"存在しないデータにアクセスしようとしています。::{bce.Message}";
            }
            else if (bce is BuffettCodeApiClientException)
            {
                message = "APIの呼び出しでエラーが発生しました";
            }
            else
            {
                message = $"未定義のエラー::{e.Message}";
            }
            return $"<<{message}>>";
        }

    }
}