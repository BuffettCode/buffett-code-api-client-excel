using BuffettCodeAddinRibbon.Settings;
using System;
using System.Windows.Forms;

namespace BuffettCodeAddinRibbon
{
    public partial class SettingForm : Form
    {
        public SettingForm(AddinSettings setting)
        {
            InitializeComponent();
            textAPIKey.Text = setting.ApiKey;
            checkDebugMode.Checked = setting.DebugMode;
            checkUseOndemandEndpoint.Checked = setting.UseOndemandEndpoint;
            var maxDegreeOfParallelism = setting.MaxDegreeOfParallelism;
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

        public uint GetMaxDegreeOfParallelism()
        {
            if (!checkParallelism.Checked)
            {
                return 0;
            }
            else if (string.IsNullOrWhiteSpace(textParallelism.Text))
            {
                return 0;
            }
            else if (uint.TryParse(textParallelism.Text, out uint i))
            {
                return i;
            }
            return 0;
        }

        public bool IsDebugMode()
        {
            return checkDebugMode.Checked;
        }

        public bool IsUseOndemandEndpoint() => checkUseOndemandEndpoint.Checked;

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

        private void CheckParallelism_CheckedChanged(object sender, EventArgs e)
        {
            textParallelism.Enabled = checkParallelism.Checked;
        }


        private void TabAPI_Click(object sender, EventArgs e)
        {

        }


    }
}
