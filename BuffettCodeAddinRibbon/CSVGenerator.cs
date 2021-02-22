using BuffettCodeIO;
using BuffettCodeIO.Formatter;
using CsvHelper;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace BuffettCodeAddinRibbon
{
    /// <summary>
    /// CSV出力
    /// </summary>
    public class CSVGenerator
    {
        private static readonly string[] EXCLUDE_PROPERTIES = { "ticker" };

        private static readonly string[] ORDERED_PROPERTIES = { "company_name", "ceo_name", "headquarters_address", "accounting_standard", "fiscal_year", "fiscal_quarter", "tdnet_title", "edinet_title" };

        /// <summary>
        /// 財務数値データからCSVを作成し、ファイルに出力します。
        /// </summary>
        /// <param name="stream">出力先のファイルストリーム</param>
        /// <param name="encoding">文字コード</param>
        /// <param name="quarters">財務数値</param>
        public static void GenerateAndWrite(Stream stream, Encoding encoding, IList<Quarter> quarters)
        {
            using (var writer = new CsvWriter(new StreamWriter(stream, encoding)))
            {
                // header
                writer.WriteField("キー");
                writer.WriteField("項目名");
                writer.WriteField("単位");
                foreach (var quarter in quarters)
                {
                    writer.WriteField(quarter.FiscalYear + "Q" + quarter.FiscalQuarter);
                }
                writer.NextRecord();

                // values
                var propertyNames = GetPropertyNames(quarters[0]);
                foreach (var propertyName in propertyNames)
                {
                    var description = quarters[0].GetDescription(propertyName);
                    writer.WriteField(propertyName);
                    writer.WriteField(description.Label);
                    writer.WriteField(description.Unit);
                    foreach (var quarter in quarters)
                    {
                        var rawValue = quarter.GetValue(propertyName);
                        var formatter = FormatterFactory.Create(description);
                        string formattedValue = formatter.Format(rawValue, description);
                        writer.WriteField(formattedValue);
                    }
                    writer.NextRecord();
                }
            }
        }

        /// <summary>
        /// CSV出力用にソートされた項目名のリストを返します。
        /// </summary>
        /// <param name="quarter">任意のquarter</param>
        /// <returns>項目名のリスト</returns>
        public static IList<string> GetPropertyNames(Quarter quarter)
        {
            var result = ORDERED_PROPERTIES.ToList<string>();
            foreach (var propertyName in quarter.GetNames())
            {
                if (!ORDERED_PROPERTIES.Contains(propertyName) && !EXCLUDE_PROPERTIES.Contains(propertyName))
                {
                    result.Add(propertyName);
                }
            }
            return result;
        }
    }
}
