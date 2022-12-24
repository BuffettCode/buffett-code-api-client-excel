using BuffettCodeIO.Property;
using Microsoft.Office.Interop.Excel;
using System.Linq;

namespace BuffettCodeIO.TabularOutput
{
    public class WorksheetTabularWriter<T> : ITabularWriter<T> where T : IApiResource
    {

        private readonly Worksheet worksheet;

        public WorksheetTabularWriter(Worksheet worksheet)
        {
            this.worksheet = worksheet;
        }

        public void Dispose()
        {
            worksheet.Activate();
        }

        public void Write(Tabular<T> tabular)
        {
            uint rowIndex = 1;
            worksheet.Application.ScreenUpdating = false;
            var calcConfig = worksheet.Application.Calculation;
            worksheet.Application.Calculation = XlCalculation.xlCalculationManual;
            tabular.ToRows().ToList().ForEach(r =>
            {
                WriteRow(rowIndex, r);
                rowIndex++;
            });
            worksheet.Application.Calculation = calcConfig;
            worksheet.Application.ScreenUpdating = true;
        }

        private void WriteRow(uint rowNumber, TabularRow row)
        {
            // write meta cols
            worksheet.Cells[rowNumber, 1] = row.Key;
            worksheet.Cells[rowNumber, 2] = row.Name;
            worksheet.Cells[rowNumber, 3] = row.Unit;
            uint colNumber = 4;
            // write data cols
            row.Values.ToList().ForEach(v =>
            {
                worksheet.Cells[rowNumber, colNumber] = v;
                colNumber++;
            });
        }
    }
}