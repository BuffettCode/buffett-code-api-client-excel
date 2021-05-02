namespace BuffettCodeExcelFunctions
{
    using BuffettCodeCommon;
    using BuffettCodeCommon.Exception;
    using BuffettCodeIO;
    using ExcelDna.Integration;
    using RegistryUtils;
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
        private static BuffettCodeApiClientWithCache apiClient;

        private static RegistryMonitor monitor;

        private static readonly object initializeLock = new object();

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
        [ExcelFunction(Description = "Get indicators, stock prices, and any further values by BuffettCode API")]
        public static string BCODE(string ticker, string parameter1, string parameter2, string propertyName, bool isRawValue = false, bool isPostfixUnit = false)
        {
            try
            {
                InitializeIfNeeded();
                return apiClient.GetValue(ticker, parameter1, parameter2, propertyName, isRawValue, isPostfixUnit);
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
                InitializeIfNeeded();
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
                InitializeIfNeeded();
                return GetDescription(propertyName).Unit;
            }
            catch (Exception e)
            {
                return ToErrorMessage(e, propertyName);
            }
        }

        /// <summary>
        /// Excelのユーザー定義関数BCODE_KEY。APIキーを設定します。デバッグ用
        /// </summary>
        /// <param name="apiKey">APIキー</param>
        /// <returns>Excelのセルに表示する文字列。常に空文字列</returns>
        [ExcelFunction(IsHidden = true, Description = "Set BuffettCode API Key to XLL Add-in")]
        public static string BCODE_KEY(string apiKey)
        {
            try
            {
                InitializeIfNeeded();
                Configuration.ApiKey = apiKey;
                return "";
            }
            catch (Exception e)
            {
                return ToErrorMessage(e);
            }
        }

        /// <summary>
        /// Excelのユーザー定義関数BCODE_CLEAR。XLLアドインがヒープに持つAPIレスポンスのキャッシュをクリアします。デバッグ用
        /// </summary>
        /// <returns>Excelのセルに表示する文字列。常に空文字列</returns>
        [ExcelFunction(IsHidden = true, Description = "Clear CacheStore")]
        public static string BCODE_CLEAR()
        {
            try
            {
                InitializeIfNeeded();
                apiClient.ClearCache();
                return "";
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

        private static void InitializeIfNeeded()
        {
            lock (initializeLock)
            {
                if (apiClient == null)
                {
                    Configuration.Reload();
                    monitor = new RegistryMonitor(Configuration.GetMonitoringRegistryKey());
                    monitor.RegChanged += new EventHandler(OnRegistryChanged);
                    monitor.Start();
                    apiClient = new BuffettCodeApiClientWithCache(Configuration.ApiKey, Configuration.MaxDegreeOfParallelism);
                }
            }
        }

        private static void OnRegistryChanged(object sender, EventArgs e)
        {
            Configuration.Reload();
            if (Configuration.ClearCache)
            {
                apiClient.ClearCache();
                Configuration.ClearCache = false;
            }
        }

        private static PropertyDescrption GetDescription(string propertyName)
        {
            // column_descriptionをAPIから取得させるため、適当なパラメタを渡している
            return apiClient.GetDescription("1301", "2017", "4", propertyName);
        }

        private static string ToErrorMessage(Exception e, string propertyName = "")
        {
            System.Diagnostics.Debug.WriteLine(e.StackTrace); // for debug

            // デバッグモードが設定されていたらエラーメッセージの代わりにスタックトレースをセルに表示
            if (Configuration.DebugMode)
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
            else if (bce is ResolveAPIException)
            {
                message = "未定義の項目名です";
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
                if (cursor is BuffettCodeException)
                {
                    break;
                }
                cursor = cursor.InnerException;
            } while (cursor != null);
            return cursor;
        }
    }
}