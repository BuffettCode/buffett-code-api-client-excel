using BuffettCodeAddin;
using BuffettCodeAddin.Client;
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
            string ticker = textTicker.Text;
            string from = textFrom.Text;
            string to = textTo.Text;
            string filename = ticker + "_" + from + "_" + to + ".csv";

            SaveFileDialog sfd = new SaveFileDialog();
            sfd.FileName = filename;
            sfd.Filter = "CSVファイル(*.csv)|*.csv";
            sfd.Title = "保存先のファイルを選択してください";
            sfd.RestoreDirectory = true;
            sfd.OverwritePrompt = true;
            sfd.CheckPathExists = true;

            if (sfd.ShowDialog() == DialogResult.OK)
            {
                var client = new BuffettCodeClientV2();
                Task<string> task = client.GetQuarterRange(apiKey, ticker, from, to, false);
                string json = task.Result;
                IList<Quarter> quarters = Quarter.Parse(ticker, json);

                using (var stream = sfd.OpenFile())
                {
                    CSVGenerator.GenerateAndWrite(stream, Encoding.UTF8, quarters);
                }
                MessageBox.Show("CSVファイルを保存しました。", "CSV出力", MessageBoxButtons.OK);
            }

            DialogResult = DialogResult.OK;
            Close();
        }

        private void ButtonCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}
