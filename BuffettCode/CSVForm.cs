using BuffettCodeAddin;
using BuffettCodeAddin.Client;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BuffettCode
{
    public partial class CSVForm : Form
    {
        private string apiKey;

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
                IList<Quarter> quarters = Quarter.parse(ticker, json);

                System.IO.Stream stream;
                stream = sfd.OpenFile();
                if (stream != null)
                {
                    System.IO.StreamWriter sw = new System.IO.StreamWriter(stream);
                    sw.Write(ToCSV(quarters));
                    sw.Close();
                    stream.Close();
                }

            }

            DialogResult = DialogResult.OK;
            Close();
        }

        private void ButtonCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private string ToCSV(IList<Quarter> quarters)
        {
            string csv = "";
            // header
            string header = "";
            foreach (Quarter quarter in quarters)
            {
                header = header + "," + quarter.GetQuarter();
            }
            csv = header + "\r\n";

            // rows
            IList<string> columns = quarters[0].GetNames();
            foreach (string column in columns) {
                string row = column;
                foreach(Quarter quarter in quarters)
                {
                    row = row + "," + quarter.GetValue(column);
                }
                csv = csv + row + "\r\n";
            }
            return csv;
        }
    }
}
