using BuffettCodeCommon;
using BuffettCodeCommon.Exception;
using System;

namespace BuffettCodeExcelFunctions
{
    public class BCodeFunctionErrorHandler
    {
        public static string ToErrorMessage(Exception e, string propertyName, bool isDebugMode)
        {
            System.Diagnostics.Debug.WriteLine(e.StackTrace); // for debug

            // デバッグモードが設定されていたらエラーメッセージの代わりにスタックトレースをセルに表示
            if (isDebugMode)
            {
                return e.ToString();
            }

            BaseBuffettCodeException bce = BuffettCodeExceptionFinder.Find(e);

            string message;
            if (bce is PropertyNotFoundException)
            {
                message = $"指定された項目が見つかりません:{propertyName}";
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
                message = $"テスト用のAPIキーでは取得できないデータです:{propertyName}";
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
            else if (bce is ValidationError)
            {
                message = $"入力された値が不正です::{bce.Message}";
            }
            else
            {
                message = $"未定義のエラー::{e.Message}";
            }
            return $"<<{message}>>";
        }
    }
}