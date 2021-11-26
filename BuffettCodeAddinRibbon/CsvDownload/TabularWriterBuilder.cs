using BuffettCodeAddinRibbon.Settings;
using BuffettCodeCommon.Config;
using BuffettCodeCommon.Exception;
using BuffettCodeIO.Property;
using BuffettCodeIO.TabularOutput;
using Microsoft.Office.Interop.Excel;
using System;
using System.Windows.Forms;

namespace BuffettCodeAddinRibbon.CsvDownload
{
    public class TabularWriterBuilder<T> where T : IApiResource
    {
        private CsvDownloadParameters parameters;


        public TabularWriterBuilder<T> Set(CsvDownloadParameters parameters)
        {
            this.parameters = parameters;
            return this;
        }


        public ITabularWriter<T> Build()
        {
            switch (parameters.OutputSettings.Destination)
            {
                // for production
                case TabularOutputDestination.NewCsvFile:
                    return BuildCsvFileTabluarWriter(true);
                case TabularOutputDestination.NewWorksheet:
                    Worksheet worksheet = Globals.ThisAddIn.Application.Worksheets.Add();
                    return BuildWorksheetTabularWriter(worksheet);

                // for unit testing
                case TabularOutputDestination.TestNewCsvFile:
                    return BuildCsvFileTabluarWriter(false);
                case TabularOutputDestination.TestNewWorksheet:
                    var excel = new Microsoft.Office.Interop.Excel.Application();
                    excel.SheetsInNewWorkbook = 1;
                    excel.Visible = true;
                    Workbook workbook = excel.Workbooks.Add();
                    return BuildWorksheetTabularWriter(workbook.Worksheets.Add());
                default:
                    throw new NotSupportedCSVOutputDestinationException($"{parameters.OutputSettings.Destination} is not supported.");
            }
        }
        private CsvFileTabularWriter<T> BuildCsvFileTabluarWriter(bool checkDialog)
        {
            var filename = $@"{parameters.Ticker}_{parameters.Range.From}_{parameters.Range.To}.csv";
            SaveFileDialog sfd = new SaveFileDialog
            {
                FileName = filename,
                Filter = "CSVファイル(*.csv)|*.csv",
                Title = "保存先のファイルを選択してください",
                RestoreDirectory = true,
                OverwritePrompt = true,
                CheckPathExists = true
            };
            if (checkDialog)
            {
                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    return new CsvFileTabularWriter<T>(sfd.OpenFile(), parameters.OutputSettings.Encoding);
                }
                else
                {
                    throw new OperationCanceledException($"Canceled writing a file={filename}.");
                }
            }
            else
            {
                return new CsvFileTabularWriter<T>(sfd.OpenFile(), parameters.OutputSettings.Encoding);
            }
        }

        private string OutputWorksheetName => $"{parameters.Ticker}_{parameters.Range.From}_{parameters.Range.To}";

        private WorksheetTabularWriter<T> BuildWorksheetTabularWriter(Worksheet worksheet)
        {
            // delete old output without prompt
            // ref: https://social.msdn.microsoft.com/Forums/vstudio/en-US/26714e15-b7a1-4e24-afb9-32ffdb45e131/delete-worksheet-in-vsto-c-without-prompting-the-user?forum=vsto
            Sheets sheets = Globals.ThisAddIn.Application.Worksheets;
            foreach (Worksheet s in sheets)
            {
                if (s.Name.Equals(OutputWorksheetName))
                {
                    Globals.ThisAddIn.Application.DisplayAlerts = false;
                    s.Delete();
                    Globals.ThisAddIn.Application.DisplayAlerts = true;
                    break;
                }
            }
            worksheet.Name = OutputWorksheetName;
            return new WorksheetTabularWriter<T>(worksheet);
        }

    }

}
