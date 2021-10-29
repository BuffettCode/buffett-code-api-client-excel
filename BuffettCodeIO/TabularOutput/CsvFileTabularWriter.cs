using BuffettCodeIO.Property;
using CsvHelper;
using System;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;


namespace BuffettCodeIO.TabluarOutput
{
    public class CsvFileTabularWriter<T> : ITabularWriter<T>, IDisposable where T : IApiResource
    {
        private readonly CsvWriter writer;

        public CsvFileTabularWriter(Stream stream, Encoding encoding)
        {
            writer = new CsvWriter(new StreamWriter(stream, encoding), CultureInfo.InvariantCulture);
        }

        public void Dispose()
        {
            writer.Dispose();
        }

        public void Write(Tabular<T> output)
        {
            output.ToRows().ToList().ForEach(r => WriteRow(r));
            writer.Flush();
        }

        private void WriteRow(TabularRow row)
        {
            // write meta cols
            writer.WriteField(row.Key);
            writer.WriteField(row.Name);
            writer.WriteField(row.Unit);
            // write data cols
            row.Values.ToList().ForEach(v => writer.WriteField(v));
            writer.NextRecord();
        }

    }
}