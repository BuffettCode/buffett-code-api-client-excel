
using BuffettCodeAddinRibbon.CsvDownload;
using BuffettCodeAddinRibbon.Settings;
using BuffettCodeCommon;
using BuffettCodeCommon.Config;
using BuffettCodeCommon.Exception;
using BuffettCodeCommon.Period;
using BuffettCodeIO.Property;
using BuffettCodeIO.TabularOutput;
using System;
using System.Linq;
using System.Windows.Forms;

namespace BuffettCodeAddinRibbon
{
    public partial class CsvDownloadForm : Form
    {

        private static readonly int lowerLimitYear = 2008;

        private static readonly int upperLimitYear = DateTime.Today.Year;

        private readonly TabularWriterBuilder<Quarter> quarterTabularWriterBuilder = new TabularWriterBuilder<Quarter>();
        private readonly ApiResourceGetter apiResourceGetter = ApiResourceGetter.Create();

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
            var encoding = radioUTF8.Checked ? TabularOutputEncoding.UTF8 : TabularOutputEncoding.SJIS;
            var destination = radioCSV.Checked ? TabularOutputDestination.NewCsvFile
                : TabularOutputDestination.NewWorksheet;
            return CsvDownloadParameters.Create(ticker, from, to, CsvDownloadOutputSettings.Create(encoding, destination));
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
                var resources = apiResourceGetter.GetQuarters(parameters);
                var quarters = resources.ToList();
                if (quarters.Count == 0)
                {
                    MessageBox.Show("条件に当てはまる財務データがありませんでした。", "CSV出力", MessageBoxButtons.OK);
                }
                else
                {
                    var tabular = TabularFormatter<Quarter>.Format(quarters);
                    var writer = quarterTabularWriterBuilder.Set(parameters).Build();
                    writer.Write(tabular);
                    MessageBox.Show("財務データの取得が完了しました", "CSV出力", MessageBoxButtons.OK);
                }
                DialogResult = DialogResult.OK;
            }
            // user cancel
            catch (OperationCanceledException)
            {
                MessageBox.Show("CSV出力がキャンセルされました", "CSV出力", MessageBoxButtons.OK);
            }
            // known errors
            catch (BaseBuffettCodeException bce)
            {
                var message = CsvDownloadExceptionHandler.ToMessageBoxString(bce);
                MessageBox.Show(message, "CSV出力", MessageBoxButtons.OK);
            }
            // unknown errors
            catch (Exception e)
            {
                var bce = BuffettCodeExceptionFinder.Find(e);
                if (bce is null)
                {
                    MessageBox.Show($"予期せぬエラーが発生しました。\n{e}", "CSV出力", MessageBoxButtons.OK);
                }
                else
                {
                    var message = CsvDownloadExceptionHandler.ToMessageBoxString(bce);
                    MessageBox.Show(message, "CSV出力", MessageBoxButtons.OK);

                }
            }
            Close();
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

        private void groupEncoding_Enter(object sender, EventArgs e)
        {

        }
    }
}