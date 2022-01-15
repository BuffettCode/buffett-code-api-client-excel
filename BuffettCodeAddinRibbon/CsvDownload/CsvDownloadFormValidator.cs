using BuffettCodeCommon.Exception;
using BuffettCodeCommon.Validator;

namespace BuffettCodeAddinRibbon.CsvDownload
{
    public class CsvDownloadFormValidator
    {
        public static string ValidateFiscalQuarter(string fiscalPeriodParam)
        {
            var tokens = fiscalPeriodParam.Split('Q');
            if (tokens.Length != 2)
            {
                return "フォーマットが正しくありません。(例: 2017Q1)";
            }
            else if (!uint.TryParse(tokens[0], out uint year))
            {
                return "年が数値ではありません。";
            }
            else if (!uint.TryParse(tokens[1], out uint quarter))
            {
                return "四半期が数値ではありません。";
            }
            else
            {
                try
                {
                    FiscalYearValidator.Validate(year);
                }
                catch (ValidationError) { return $"年の設定が不正です: {year}"; }
                try
                {
                    FiscalQuarterValidator.Validate(quarter);
                }
                catch (ValidationError) { return $"四半期の設定が不正です: {quarter}"; }
            }
            return null;
        }
    }
}
