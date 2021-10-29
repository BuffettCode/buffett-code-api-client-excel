using BuffettCodeCommon.Period;
using BuffettCodeIO.Property;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace BuffettCodeIO.TabluarOutput.Tests
{
    [TestClass()]
    public class CsvFileTabularWriterTests
    {
        private static readonly string ticker = "1234";
        private static readonly string key = "test_key";
        private static readonly string value = "test_value";
        private static readonly string label = "ラベル";
        private static readonly string unit = "test_unit";

        private static readonly FiscalQuarterPeriod period = FiscalQuarterPeriod.Create(2021, 3);
        private static readonly IDictionary<string, string> properties = new Dictionary<string, string> { { key, value } };
        private static readonly IDictionary<string, PropertyDescription> descriptions = new Dictionary<string, PropertyDescription> { { key, new PropertyDescription(key, label, unit) } };

        private static Quarter CreateQuarter(string ticker, FiscalQuarterPeriod period, IDictionary<string, string> properties, IDictionary<string, PropertyDescription> descriptions)
        {
            return Quarter.Create(
                ticker,
                period,
                properties is null ? PropertyDictionary.Empty() : new PropertyDictionary(properties),
                descriptions is null ? PropertyDescriptionDictionary.Empty() : new PropertyDescriptionDictionary(descriptions)
            );
        }


        [TestMethod()]
        public void WriteQuarterTest()
        {
            var quarter = CreateQuarter(ticker, period, properties, descriptions);
            var csvOutput = new Tabular<Quarter>().Add(quarter);
            var fileName = "CsvFileWriterWriteQuarterTest.csv";
            var fileInfo = new FileInfo(fileName);

            // at first, the file is not created
            Assert.IsFalse(fileInfo.Exists);

            using (var writer = new CsvFileTabularWriter<Quarter>(fileInfo.Create(), Encoding.UTF8))
            {
                writer.Write(csvOutput);
            }
            // after writing records, check size
            fileInfo.Refresh();
            Assert.IsTrue(fileInfo.Length > 0);
            fileInfo.Delete();
        }

    }
}