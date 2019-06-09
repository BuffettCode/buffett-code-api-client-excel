using BuffettCodeAddin;
using BuffettCodeAddin.Client;
using BuffettCodeAddin.Formatter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BuffettCode
{
    public partial class CSVForm : Form
    {
        private readonly string apiKey;

        private static readonly int lowerLimitYear = 2008;

        private static readonly int upperLimitYear = DateTime.Today.Year;

        public CSVForm(string apiKey)
        {
            InitializeComponent();
            LoadSettings();
            this.apiKey = apiKey;
            AcceptButton = buttonOK;
        }

        private void LoadSettings()
        {
            textTicker.Text = Properties.Settings.Default.CSVTicker;
            textFrom.Text = Properties.Settings.Default.CSVFrom;
            textTo.Text = Properties.Settings.Default.CSVTo;
            radioCSV.Checked = Properties.Settings.Default.CSVIsFile;
            radioSheet.Checked = !Properties.Settings.Default.CSVIsFile;
            radioUTF8.Checked = Properties.Settings.Default.CSVUTF8;
            radioShiftJIS.Checked = !Properties.Settings.Default.CSVUTF8;
        }

        private void CSVForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            SaveSettings();
        }

        private void SaveSettings()
        {
            Properties.Settings.Default.CSVTicker = textTicker.Text;
            Properties.Settings.Default.CSVFrom = textFrom.Text;
            Properties.Settings.Default.CSVTo = textTo.Text;
            Properties.Settings.Default.CSVIsFile = radioCSV.Checked;
            Properties.Settings.Default.CSVUTF8 = radioUTF8.Checked;
            Properties.Settings.Default.Save();
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
                if (radioCSV.Checked)
                {
                    WriteCSVFile();
                }
                else
                {
                    WriteNewSheet();
                }
            } catch (TestAPIConstraintException)
            {
                MessageBox.Show("テスト用のAPIキーでは末尾が01の銘柄コードのみ使用できます。", "CSV出力", MessageBoxButtons.OK);
                return;
            } catch (QuotaException)
            {
                MessageBox.Show("APIの実行回数が上限に達しました。", "CSV出力", MessageBoxButtons.OK);
                return;
            }
            catch (InvalidAPIKeyException)
            {
                MessageBox.Show("APIキーが有効ではありません。", "CSV出力", MessageBoxButtons.OK);
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
                var quarters = GetSortedQuarters(ticker, from, to);
                if (quarters.Count == 0)
                {
                    MessageBox.Show("条件に当てはまる財務データがありませんでした。", "CSV出力", MessageBoxButtons.OK);
                    return;
                }

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
            var quarters = GetSortedQuarters(ticker, from, to);

            if (quarters.Count == 0)
            {
                MessageBox.Show("条件に当てはまる財務データがありませんでした。", "CSV出力", MessageBoxButtons.OK);
                return;
            }

            // create new sheet
            Microsoft.Office.Interop.Excel.Worksheet worksheet;
            try
            { 
                worksheet = BuffettCode.Globals.ThisAddIn.Application.Worksheets.Add();
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
                foreach (var quarter in quarters)
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

        private IList<Quarter> GetSortedQuarters(string ticker, string from, string to)
        {
            var client = new BuffettCodeClientV2();
            var quarters = new List<Quarter>();

            foreach (KeyValuePair<string, string> range in SliceRange(from, to))
            {
                Task<string> task = client.GetQuarterRange(apiKey, ticker, range.Key, range.Value, false);
                string json = task.Result;
                quarters.AddRange(Quarter.Parse(ticker, json));
            }

            var distinct = new List<Quarter>(quarters).Distinct(new IPropertyAggregationComparer()).Cast<Quarter>();
            var sorted = new List<Quarter>(distinct);
            sorted.Sort((left, right) => { return left.GetIdentifier().CompareTo(right.GetIdentifier()); });
            return sorted;
        }

        private IDictionary<string, string> SliceRange(string from, string to)
        {
            var result = new Dictionary<string, string>();
            var gap = GetGap(from, to);

            var cursor = from;
            while (gap > 12)
            {
                result.Add(cursor, ToRange(cursor));
                cursor = Add3Years(cursor);
                gap -= 12;
            }
            result.Add(cursor, to);

            return result;
        }

        private int GetGap(string from, string to)
        {
            int ty = int.Parse(to.Split('Q')[0]);
            int fy = int.Parse(from.Split('Q')[0]);
            int tq = int.Parse(to.Split('Q')[1]);
            int fq = int.Parse(from.Split('Q')[1]);
            return (ty - fy) * 4 + (tq - fq) + 1;
        }

        private string ToRange(string from)
        {
            int y = int.Parse(from.Split('Q')[0]);
            int q = int.Parse(from.Split('Q')[1]);
            return q == 1 ? (y + 2) + "Q4" : (y + 3) + "Q" + (q - 1);
        }

        private string Add3Years(string quarter)
        {
            int y = int.Parse(quarter.Split('Q')[0]);
            int q = int.Parse(quarter.Split('Q')[1]);
            return (y + 3) + "Q" + q;
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
