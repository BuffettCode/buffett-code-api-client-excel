using BuffettCodeIO.Formatter;
using BuffettCodeIO.Property;
using CsvHelper;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;

namespace BuffettCodeAddinRibbon
{
    public class CsvFileWriter
    {

        public static void Write(Stream stream, Encoding encoding, IList<Quarter> quarters)
        {

            using (var writer = new CsvWriter(new StreamWriter(stream, encoding), CultureInfo.InvariantCulture))
            {
                writer.WriteField("キー");
                writer.WriteField("項目名");
                writer.WriteField("単位");
                foreach (var quarter in quarters)
                {
                    writer.WriteField($"{quarter.Period.Year}Q{quarter.Period.Quarter}");
                }
                writer.NextRecord();

                // values
                var propertyNames = CsvPropertyHelper.CreatePropertyNameList(quarters[0]);
                foreach (var propertyName in propertyNames)
                {
                    var description = quarters[0].GetDescription(propertyName);
                    writer.WriteField(propertyName);
                    writer.WriteField(description.Label);
                    writer.WriteField(description.Unit);
                    foreach (var quarter in quarters)
                    {
                        var rawValue = quarter.GetValue(propertyName);
                        var formatter = PropertyFormatterFactory.Create(description);
                        string formattedValue = formatter.Format(rawValue, description);
                        writer.WriteField(formattedValue);
                    }
                    writer.NextRecord();
                }
            }
        }

    }
}
