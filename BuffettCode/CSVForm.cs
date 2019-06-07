using BuffettCodeAddin;
using BuffettCodeAddin.Client;
using BuffettCodeAddin.Formatter;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BuffettCode
{
    public partial class CSVForm : Form
    {
        private readonly string apiKey;

        public CSVForm(string apiKey)
        {
            InitializeComponent();
            this.apiKey = apiKey;
        }

        private void ButtonOK_Click(object sender, EventArgs e)
        {
            if (radioCSV.Checked)
            {
                WriteCSVFile();
            } else
            {
                WriteNewSheet();
            }
        }

        private void ButtonCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void WriteCSVFile()
        {
            string ticker = textTicker.Text;
            string from = textFrom.Text;
            string to = textTo.Text;
            string filename = ticker + "_" + from + "_to_" + to + ".csv";

            SaveFileDialog sfd = new SaveFileDialog
            {
                FileName = filename,
                Filter = "CSVファイル(*.csv)|*.csv",
                Title = "保存先のファイルを選択してください",
                RestoreDirectory = true,
                OverwritePrompt = true,
                CheckPathExists = true
            };

            if (sfd.ShowDialog() == DialogResult.OK)
            {
                var client = new BuffettCodeClientV2();
                Task<string> task = client.GetQuarterRange(apiKey, ticker, from, to, false);
                string json = task.Result;
                IList<Quarter> quarters = Quarter.Parse(ticker, json);

                using (var stream = sfd.OpenFile())
                {
                    var encoding = radioUTF8.Checked ? Encoding.UTF8 : Encoding.GetEncoding("shift_jis");
                    CSVGenerator.GenerateAndWrite(stream, encoding, quarters);
                }
                MessageBox.Show("CSVファイルを保存しました。", "CSV出力", MessageBoxButtons.OK);
            }

            DialogResult = DialogResult.OK;
            Close();
        }


        private void WriteNewSheet()
        {
            string ticker = textTicker.Text;
            string from = textFrom.Text;
            string to = textTo.Text;

            var client = new BuffettCodeClientV2();
            Task<string> task = client.GetQuarterRange(apiKey, ticker, from, to, false);
            string json = task.Result;
            IList<Quarter> quarters = Quarter.Parse(ticker, json);
            var sorted = new List<Quarter>(quarters);
            sorted.Sort((left, right) => { return left.GetIdentifier().CompareTo(right.GetIdentifier()); });

            // create new sheet
            Microsoft.Office.Interop.Excel.Worksheet worksheet;
            try
            { 
                worksheet = BuffettCode.Globals.ThisAddIn.Application.Worksheets.Add();
            }
            catch (Exception e)
            {
                MessageBox.Show("新しいシートの作成に失敗しました。", "CSV出力", MessageBoxButtons.OK);
                return;
            }

            // write header
            int row = 1;
            int col = 1;
            worksheet.Cells[col, row++] = "キー";
            worksheet.Cells[col, row++] = "項目名";
            worksheet.Cells[col, row++] = "単位";
            foreach (var quarter in sorted)
            {
                worksheet.Cells[col, row++] = quarter.FiscalYear + "Q" + quarter.FiscalQuarter;
            }

            // write values
            var propertyNames = CSVGenerator.GetPropertyNames(quarters[0]);
            foreach (var propertyName in propertyNames)
            {
                row = 1;
                col++;

                var description = quarters[0].GetDescription(propertyName);
                worksheet.Cells[col, row++] = propertyName;
                worksheet.Cells[col, row++] = description.Label;
                worksheet.Cells[col, row++] = description.Unit;
                foreach (var quarter in sorted)
                {
                    var rawValue = quarter.GetValue(propertyName);
                    var formatter = FormatterFactory.Create(rawValue, description);
                    string formattedValue = formatter.Format(rawValue, description);
                    worksheet.Cells[col, row++] = formattedValue;
                }
            }
            MessageBox.Show("新しいシートを作成しました。", "CSV出力", MessageBoxButtons.OK);

            DialogResult = DialogResult.OK;
            Close();
        }

        private void RadioCSV_CheckedChanged(object sender, EventArgs e)
        {
            UpdateForm();
        }

        private void UpdateForm()
        {
            groupEncoding.Visible = radioCSV.Checked;
        }

        private void RadioSheet_CheckedChanged(object sender, EventArgs e)
        {
            UpdateForm();
        }
    }
}
