
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
            radioSheet.Checked = !parameters.IsCreateNewFile();
            radioUTF8.Checked = parameters.IsUTF8Encoding();
            radioShiftJIS.Checked = !parameters.IsUTF8Encoding();
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
            var errorMessage = CsvDownloadFormValidator.ValidateFiscalQuarter(textFrom.Text);
            if (!string.IsNullOrEmpty(errorMessage))
            {
                textFrom.Select();
                return false;
            }
            errorMessage = CsvDownloadFormValidator.ValidateFiscalQuarter(textTo.Text);
            if (!string.IsNullOrEmpty(errorMessage))
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
            var errorMessage = CsvDownloadFormValidator.ValidateFiscalQuarter(textFrom.Text);
            if (!string.IsNullOrEmpty(errorMessage))
            {
                e.Cancel = true;
                errorProvider.SetError(textFrom, errorMessage);
            }
        }

        private void TextFrom_Validated(object sender, EventArgs e)
        {
            errorProvider.SetError(textFrom, "");
        }

        private void TextTo_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            var errorMessage = CsvDownloadFormValidator.ValidateFiscalQuarter(textTo.Text);
            if (!string.IsNullOrEmpty(errorMessage))
            {
                e.Cancel = true;
                errorProvider.SetError(textTo, errorMessage);
            }
        }

        private void TextTo_Validated(object sender, EventArgs e)
        {
            errorProvider.SetError(textTo, "");
        }
    }
}