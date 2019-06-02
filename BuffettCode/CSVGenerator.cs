using BuffettCodeAddin;
using BuffettCodeAddin.Formatter;
using CsvHelper;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace BuffettCode
{
    class CSVGenerator
    {
        private static readonly string[] EXCLUDE_PROPERTIES = { "ticker" };

        private static readonly string[] ORDERED_PROPERTIES = { "company_name", "ceo_name", "headquarters_address", "accounting_standard", "fiscal_year", "fiscal_quarter" };

        public static void GenerateAndWrite(Stream stream, Encoding encoding, IList<Quarter> quarters)
        {
            if (quarters.Count == 0)
            {
                return;
            }

            var sorted = quarters.ToList();
            sorted.Sort((left, right) => { return left.GetIdentifier().CompareTo(right.GetIdentifier()); });
            using (var writer = new CsvWriter(new StreamWriter(stream, encoding)))
            {
                // header
                writer.WriteField("キー");
                writer.WriteField("項目名");
                writer.WriteField("単位");
                foreach (var quarter in sorted)
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
                    foreach (var quarter in sorted)
                    {
                        var rawValue = quarter.GetValue(propertyName);
                        var formatter = FormatterFactory.Create(rawValue, description);
                        string formattedValue = formatter.Format(rawValue, description);
                        writer.WriteField(formattedValue);
                    }
                    writer.NextRecord();
                }
            }
        }

        private static IList<string> GetPropertyNames(Quarter quarter)
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
