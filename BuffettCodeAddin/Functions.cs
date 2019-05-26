using BuffettCodeAddin.Client;
using ExcelDna.Integration;
using RegistryUtils;
using System;

namespace BuffettCodeAddin
{
    public class Functions
    {
        private static BuffettCodeAPI api;

        private static RegistryMonitor monitor;

        [ExcelFunction(Description = "Get indicators, stock prices, and any further values by BuffettCode API")]
        public static string BCODE(string ticker, string parameter1, string parameter2, string propertyName, bool isRawValue = false, bool isPostfixUnit = false)
        {
            try
            {
                InitializeIfNeeded();

                return api.GetValue(ticker, parameter1, parameter2, propertyName, isRawValue, isPostfixUnit);
            }
            catch (Exception e)
            {
                return ToErrorMessage(e, propertyName);
            }
        }

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

        [ExcelFunction(IsHidden = true, Description = "Clear CacheStore")]
        public static string BCODE_CLEAR()
        {
            try
            {
                InitializeIfNeeded();
                api.ClearCache();
                System.Diagnostics.Debug.WriteLine("cache cleared.");
                return "";
            }
            catch (Exception e)
            {
                return ToErrorMessage(e);
            }
        }

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
            if (api == null)
            {
                Configuration.Reload();
                monitor = new RegistryMonitor(Configuration.GetMonitoringRegistryKey());
                monitor.RegChanged += new EventHandler(OnRegistryChanged);
                monitor.Start();
                api = new BuffettCodeAPI();
            }
        }

        private static void OnRegistryChanged(object sender, EventArgs e)
        {
            Configuration.Reload();
            if (Configuration.ClearCache)
            {
                api.ClearCache();
                Configuration.ClearCache = false;
            }
        }

        private static PropertyDescrption GetDescription(string propertyName)
        {
            return api.GetDescription("1301", "2017", "4", propertyName);
        }

        private static string ToErrorMessage(Exception e, string propertyName = "")
        {
            System.Diagnostics.Debug.WriteLine(e.StackTrace); // for debug

            string message;
            Exception bce = GetBuffettCodeException(e);
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
            } else
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
                    return cursor;
                }
                cursor = cursor.InnerException;
            } while (cursor != null);
            return null;
        }
    }
}
