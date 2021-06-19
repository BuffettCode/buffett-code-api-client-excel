namespace BuffettCodeExcelFunctions
{
    using BuffettCodeCommon;
    using BuffettCodeCommon.Exception;
    using BuffettCodeIO;
    using BuffettCodeIO.Property;
    using ExcelDna.Integration;
    using System;

    /// <summary>
    /// Excelのユーザー定義関数(UDF)
    /// </summary>
    /// <remarks>
    /// バフェットコードのExcelアドインがユーザに提供する関数の実装。
    /// メインとなるBCODE関数と、デバッグおよびサポートのために作られたいくつかの隠し関数を含みます。
    /// それぞれの関数のリファレンスはバフェットコードのWebサイトを参照のこと。
    /// </remarks>
    public class UserDefinedFunctions
    {
        private static readonly Configuration config = Configuration.GetInstance();
        private static readonly BuffettCodeApiV2TaskProcessor apiTaskProcessor = new BuffettCodeApiV2TaskProcessor(config.ApiKey, config.MaxDegreeOfParallelism);
        private static readonly object updateLock = new object();

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
        [ExcelFunction(Description = "Get indicators, stock prices, and any further values by BuffettCode API", Name = "BCODE")]
        public static string BCodeLegacy(string ticker, string parameter1, string parameter2, string propertyName, bool isRawValue = false, bool isPostfixUnit = false)
        {
            try
            {
                UpdateProcessorIfNeeded();
                return apiTaskProcessor.GetValue(ticker, parameter1, parameter2, propertyName, isRawValue, isPostfixUnit);
            }
            catch (Exception e)
            {
                return ToErrorMessage(e, propertyName);
            }
        }

        /// <summary>
        /// Excelのユーザー定義関数BCODE_LABEL。項目名を指定して日本語の名称を取得します
        /// </summary>
        /// <param name="propertyName">項目名</param>
        /// <returns>Excelのセルに表示する文字列</returns>
        [ExcelFunction(IsHidden = true, Description = "Get property name in Japanese")]
        public static string BCODE_LABEL(string propertyName)
        {
            try
            {
                UpdateProcessorIfNeeded();
                return GetDescription(propertyName).Label;
            }
            catch (Exception e)
            {
                return ToErrorMessage(e, propertyName);
            }
        }

        /// <summary>
        /// Excelのユーザー定義関数BCODE_UNIT。項目名を指定して単位の名称を取得します
        /// </summary>
        /// <param name="propertyName">項目名</param>
        /// <returns>Excelのセルに表示する文字列</returns>
        [ExcelFunction(IsHidden = true, Description = "Get unit name in Japanese")]
        public static string BCODE_UNIT(string propertyName)
        {
            try
            {
                UpdateProcessorIfNeeded();
                return GetDescription(propertyName).Unit;
            }
            catch (Exception e)
            {
                return ToErrorMessage(e, propertyName);
            }
        }

        /// <summary>
        /// Excelのユーザー定義関数BCODE_API_KEY。
        /// </summary>
        /// <returns>Registryに格納されたAPI Token</returns>
        [ExcelFunction(IsHidden = true, Description = "Clear CacheStore")]
        public static string BCODE_API_KEY()
        {
            try
            {
                UpdateProcessorIfNeeded();
                return config.ApiKey;
            }
            catch (Exception e)
            {
                return ToErrorMessage(e);
            }
        }



        /// <summary>
        /// Excelのユーザー定義関数BCODE_CLEAR。ApiClientが持つAPIレスポンスのキャッシュをクリアします。デバッグ用
        /// </summary>
        /// <returns>Excelのセルに表示する文字列。常に空文字列</returns>
        [ExcelFunction(IsHidden = true, Description = "Clear CacheStore")]
        public static string BCODE_CLEAR()
        {
            try
            {
                UpdateProcessorIfNeeded();
                apiTaskProcessor.ClearCache();
                return "Succeed: CacheClear";
            }
            catch (Exception e)
            {
                return ToErrorMessage(e);
            }
        }

        /// <summary>
        /// Excelのユーザー定義関数BCODE_PING。ExcelからXLLアドインへのファンクションコールをチェックします。デバッグ用
        /// </summary>
        /// <returns>Excelのセルに表示する文字列。ランダムな整数値</returns>
        [ExcelFunction(IsHidden = true, Description = "Check function call (from Excel to XLL)")]
        public static string BCODE_PING()
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

        private static void UpdateProcessorIfNeeded()
        {
            lock (updateLock)
            {
                if (!apiTaskProcessor.ApiKey.Equals(config.ApiKey))
                {
                    apiTaskProcessor.UpdateApiKey(config.ApiKey);
                }
                if (!apiTaskProcessor.MaxDegreeOfParallelism.Equals
                    (config.MaxDegreeOfParallelism))
                {
                    apiTaskProcessor.UpdateMaxDegreeOfParallelism(config.MaxDegreeOfParallelism);
                }
            }
        }

        private static PropertyDescription GetDescription(string propertyName)
        {
            // column_descriptionをAPIから取得させるため、適当なパラメタを渡している
            return apiTaskProcessor.GetDescription("1301", "2019", "4", propertyName);
        }

        private static string ToErrorMessage(Exception e, string propertyName = "")
        {
            System.Diagnostics.Debug.WriteLine(e.StackTrace); // for debug

            // デバッグモードが設定されていたらエラーメッセージの代わりにスタックトレースをセルに表示
            if (config.DebugMode)
            {
                return e.ToString();
            }

            // 例外によってはBuffettCodeExceptionがInnerExceptionに入ってくるので、
            // 再帰的にスキャンして取り出している
            Exception bce = GetBuffettCodeException(e);

            string message;
            if (bce is PropertyNotFoundException)
            {
                message = "指定された項目が見つかりません:" + propertyName;
            }
            else if (bce is AggregationNotFoundException)
            {
                message = "指定されたデータを取得できませんでした";
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
            else if (bce is ResolveApiException)
            {
                message = "未定義の項目名です";
            }
            else if (bce is ApiResponseParserException)
            {
                message = "APIレスポンスのパースに失敗しました";
            }
            else
            {
                message = "未定義のエラー";
            }
            return "<<" + message + ">>";
        }

        private static Exception GetBuffettCodeException(Exception e)
        {
            Exception cursor = e;
            do
            {
                if (cursor is BaseBuffettCodeException)
                {
                    break;
                }
                cursor = cursor.InnerException;
            } while (cursor != null);
            return cursor;
        }
    }
}