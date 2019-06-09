using System;
using System.Windows.Forms;

namespace BuffettCode
{
    public partial class SettingForm : Form
    {
        public SettingForm(string apiKey, bool debugMode)
        {
            InitializeComponent();
            textAPIKey.Text = apiKey;
            checkDebugMode.Checked = debugMode;
        }

        private void SettingForm_Load(object sender, EventArgs e)
        {

        }

        public string GetAPIKey()
        {
            return textAPIKey.Text;
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
    }
}
