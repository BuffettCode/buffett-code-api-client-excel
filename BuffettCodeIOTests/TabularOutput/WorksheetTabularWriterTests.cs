using BuffettCodeCommon.Period;
using BuffettCodeIO.Property;
using Microsoft.Office.Interop.Excel;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;


namespace BuffettCodeIO.TabularOutput.Tests
{
    [TestClass()]
    public class WorksheetTabularWriterTests
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

        string GetSheetCellValve(Worksheet worksheet, uint colNom, uint rowNum) => (string)(worksheet.Cells[colNom, rowNum] as Range).Value;


        // If you want test worksheet, comment in
        // On GitHub Actions, we can't use Windows Office now
        // [TestMethod()]
        public void WorksheetTabularWriterTest()
        {
            var quarter = CreateQuarter(ticker, period, properties, descriptions);
            var tabular = new Tabular<Quarter>().Add(quarter);
            var excel = new Application();
            excel.SheetsInNewWorkbook = 1;
            excel.Visible = true;
            Workbook workbook = excel.Workbooks.Add();
            Worksheet worksheet = workbook.Worksheets.Add();
            using (var writer = new WorksheetTabularWriter<Quarter>(worksheet))
            {
                writer.Write(tabular);
            }
            Assert.AreEqual("キー", GetSheetCellValve(worksheet, 1, 1));
            Assert.AreEqual("項目名", GetSheetCellValve(worksheet, 1, 2));
            Assert.AreEqual("単位", GetSheetCellValve(worksheet, 1, 3));
            worksheet.Delete();
            workbook.Close(0);
            excel.Quit();
        }
    }
}