using System;
using System.Windows.Forms;

namespace BuffettCodeAddinRibbon
{
    public partial class SettingForm : Form
    {
        public SettingForm(string apiKey, int maxDegreeOfParallelism, bool debugMode)
        {
            InitializeComponent();
            textAPIKey.Text = apiKey;
            checkDebugMode.Checked = debugMode;

            if (maxDegreeOfParallelism == 0)
            {
                checkParallelism.Checked = false;
                textParallelism.Text = "";
                textParallelism.Enabled = false;
            }
            else
            {
                checkParallelism.Checked = true;
                textParallelism.Text = maxDegreeOfParallelism.ToString();
                textParallelism.Enabled = true;
            }
        }

        private void SettingForm_Load(object sender, EventArgs e)
        {

        }

        public string GetAPIKey()
        {
            return textAPIKey.Text;
        }

        public int GetMaxDegreeOfParallelism()
        {
            if (!checkParallelism.Checked)
            {
                return 0;
            }
            else if (string.IsNullOrWhiteSpace(textParallelism.Text))
            {
                return 0;
            }
            else if (int.TryParse(textParallelism.Text, out int i))
            {
                return i;
            }
            return 0;
        }

        public bool IsDebugMode()
        {
            return checkDebugMode.Checked;
        }

        private void ButtonOK_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            Close();
        }

        private void ButtonCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void TextAPIKey_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                DialogResult = DialogResult.OK;
                Close();
            }
        }

        private void CheckParallelism_CheckedChanged(object sender, EventArgs e)
        {
            textParallelism.Enabled = checkParallelism.Checked;
        }
    }
}
