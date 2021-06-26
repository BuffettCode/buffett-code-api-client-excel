
using BuffettCodeAddinRibbon.Settings;
using BuffettCodeCommon;
using BuffettCodeCommon.Config;
using BuffettCodeCommon.Exception;
using BuffettCodeCommon.Period;
using BuffettCodeIO;
using BuffettCodeIO.Formatter;
using BuffettCodeIO.Property;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace BuffettCodeAddinRibbon
{
    public partial class CsvDownloadForm : Form
    {

        private static readonly int lowerLimitYear = 2008;

        private static readonly int upperLimitYear = DateTime.Today.Year;

        public CsvDownloadForm()
        {
            InitializeComponent();
            var parameters = CsvDownloadDefaultParametersHandler.Load();
            UpdateFormValues(parameters);
            AcceptButton = buttonOK;
        }

        private void UpdateFormValues(CsvDownloadParameters parameters)
        {
            textTicker.Text = parameters.Ticker;
            textFrom.Text = parameters.Range.From.ToString();
            textTo.Text = parameters.Range.To.ToString();
            radioCSV.Checked = parameters.IsCreateNewFile();
            radioUTF8.Checked = parameters.IsUTF8Encoding();
        }

        private CsvDownloadParameters CreateParametersFromFormValues()
        {
            var ticker = textTicker.Text;
            var from = FiscalQuarterPeriod.Parse(textFrom.Text);
            var to = FiscalQuarterPeriod.Parse(textTo.Text);
            var encoding = radioUTF8.Checked ? CSVOutputEncoding.UTF8 : CSVOutputEncoding.SJIS;
            var destination = radioCSV.Checked ? CSVOutputDestination.NewFile
                : CSVOutputDestination.NewSheet;
            return CsvDownloadParameters.Create(ticker, from, to, CsvFileOutputSettings.Create(encoding, destination));
        }

        private void CSVForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            var parameters = CreateParametersFromFormValues();
            CsvDownloadDefaultParametersHandler.Save(parameters);
        }

        private void ButtonOK_Click(object sender, EventArgs e)
        {
            Execute();
        }

        private void Execute()
        {
            if (!ValidateControls())
            {
                return;
            }
            try
            {
                var parameters = CreateParametersFromFormValues();
                var quarters = GetSortedQuarters(parameters);
                if (radioCSV.Checked)
                {
                    WriteCSVFile(parameters, quarters);
                }
                else
                {
                    WriteNewSheet(parameters, quarters);
                }
            }
            catch (TestAPIConstraintException)
            {
                MessageBox.Show("テスト用のAPIキーでは末尾が01の銘柄コードのみ使用できます。", "CSV出力", MessageBoxButtons.OK);
                return;
            }
            catch (QuotaException)
            {
                MessageBox.Show("APIの実行回数が上限に達しました。", "CSV出力", MessageBoxButtons.OK);
                return;
            }
            catch (InvalidAPIKeyException)
            {
                MessageBox.Show("APIキーが有効ではありません。", "CSV出力", MessageBoxButtons.OK);
                return;
            }
            catch (ApiResponseParserException)
            {
                MessageBox.Show("APIのレスポンスのパースに失敗しました。", "CSV出力", MessageBoxButtons.OK);
                return;
            }
            catch (Exception)
            {
                MessageBox.Show("データの取得中にエラーが発生しました。", "CSV出力", MessageBoxButtons.OK);
                return;
            }
        }

        private bool ValidateControls()
        {
            var message = ValidateQuarter(textFrom.Text);
            if (!string.IsNullOrEmpty(message))
            {
                textFrom.Select();
                return false;
            }
            message = ValidateQuarter(textTo.Text);
            if (!string.IsNullOrEmpty(message))
            {
                textTo.Select();
                return false;
            }
            return true;
        }

        private void WriteCSVFile(CsvDownloadParameters parameters, IList<Quarter> quarters)
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

            if (sfd.ShowDialog() == DialogResult.OK)
            {
                if (quarters.Count == 0)
                {
                    MessageBox.Show("条件に当てはまる財務データがありませんでした。", "CSV出力", MessageBoxButtons.OK);
                    return;
                }

                using (var stream = sfd.OpenFile())
                {
                    CsvFileWriter.Write(stream, parameters.OutputSettings.Encoding, quarters);
                }
                MessageBox.Show("CSVファイルを保存しました。", "CSV出力", MessageBoxButtons.OK);
            }

            DialogResult = DialogResult.OK;
            Close();
        }

        private void WriteNewSheet(CsvDownloadParameters parameters, IList<Quarter> quarters)
        {
            if (quarters.Count == 0)
            {
                MessageBox.Show("条件に当てはまる財務データがありませんでした。", "CSV出力", MessageBoxButtons.OK);
                return;
            }

            // create new sheet
            Microsoft.Office.Interop.Excel.Worksheet worksheet;
            try
            {
                worksheet = Globals.ThisAddIn.Application.Worksheets.Add();
            }
            catch (Exception)
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
            foreach (var quarter in quarters)
            {
                worksheet.Cells[col, row++] = $"{quarter.Period.Year}Q{quarter.Period.Quarter}";
            }

            // write values
            var propertyNames = CsvPropertyHelper.CreatePropertyNameList(quarters[0]);
            foreach (var propertyName in propertyNames)
            {
                row = 1;
                col++;

                var description = quarters[0].GetDescription(propertyName);
                worksheet.Cells[col, row++] = propertyName;
                worksheet.Cells[col, row++] = description.Label;
                worksheet.Cells[col, row++] = description.Unit;
                foreach (var quarter in quarters)
                {
                    var rawValue = quarter.GetValue(propertyName);
                    var formatter = PropertyFormatterFactory.Create(description);
                    string formattedValue = formatter.Format(rawValue, description);
                    worksheet.Cells[col, row++] = formattedValue;
                }
            }
            worksheet.Name = $"{parameters.Ticker}_{parameters.Range.From}_{parameters.Range.To}";
            MessageBox.Show("新しいシートを作成しました。", "CSV出力", MessageBoxButtons.OK);

            DialogResult = DialogResult.OK;
            Close();
        }

        private IList<Quarter> GetSortedQuarters(CsvDownloadParameters parameters)
        {
            var config = Configuration.GetInstance();
            var processor = new BuffettCodeApiTaskProcessor(config.ApiVersion, config.ApiKey, config.MaxDegreeOfParallelism);

            var quarters = PeriodRange<FiscalQuarterPeriod>.Slice(parameters.Range, 12)
                .SelectMany
                (r => processor.GetApiResources(DataTypeConfig.Quarter, parameters.Ticker, r.From, r.To)
                ).Cast<Quarter>();
            return quarters.Distinct().OrderBy(q => q.Period).ToArray();
        }

        private void RadioCSV_CheckedChanged(object sender, EventArgs e)
        {
            UpdateForm();
        }
        private void RadioSheet_CheckedChanged(object sender, EventArgs e)
        {
            UpdateForm();
        }
        private void UpdateForm()
        {
            groupEncoding.Visible = radioCSV.Checked;
        }

        private void TextFrom_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            var message = ValidateQuarter(textFrom.Text);
            if (!string.IsNullOrEmpty(message))
            {
                e.Cancel = true;
                errorProvider.SetError(textFrom, message);
            }
        }

        private void TextFrom_Validated(object sender, EventArgs e)
        {
            errorProvider.SetError(textFrom, "");
        }

        private void TextTo_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            var message = ValidateQuarter(textTo.Text);
            if (!string.IsNullOrEmpty(message))
            {
                e.Cancel = true;
                errorProvider.SetError(textTo, message);
            }
        }

        private void TextTo_Validated(object sender, EventArgs e)
        {
            errorProvider.SetError(textTo, "");
        }

        private string ValidateQuarter(string param)
        {
            var tokens = param.Split('Q');
            if (tokens.Length != 2)
            {
                return "フォーマットが正しくありません。(例: 2017Q1)";
            }
            if (!int.TryParse(tokens[0], out int year))
            {
                return "年が数値ではありません。";
            }
            else if (year < lowerLimitYear)
            {
                return lowerLimitYear + "年以降で指定してください。";
            }
            else if (year > upperLimitYear)
            {
                return upperLimitYear + "年以前で指定してください。";
            }
            if (!int.TryParse(tokens[1], out int quarter))
            {
                return "四半期が数値ではありません。";
            }
            else if (quarter < 1 || quarter > 4)
            {
                return lowerLimitYear + "四半期は1~4を指定してください。";
            }
            return null;
        }
    }
}