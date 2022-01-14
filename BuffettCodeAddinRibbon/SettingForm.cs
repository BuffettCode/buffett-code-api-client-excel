using BuffettCodeAddinRibbon.Settings;
using BuffettCodeCommon.Config;
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
            checkIsOndemandEndpointEnabled.Checked = setting.IsOndemandEndpointEnabled;
            checkForceOndemandApi.Checked = setting.IsForceOndemandEndpoint;
            if (!checkIsOndemandEndpointEnabled.Checked)
            {
                checkForceOndemandApi.Visible = false;
                forceOndemandEndpointDesc.Visible = false;
            }
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

        public bool IsOndemandEndpointEnabled() => checkIsOndemandEndpointEnabled.Checked;

        public bool IsForceOndemandEndpoint() => checkForceOndemandApi.Checked;

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
        private void TabAPI_Click(object sender, EventArgs e)
        {

        }

        private void TextAPIKey_TextChanged(object sender, EventArgs e)
        {

        }

        private void OndemandModeDescDesc_TextChanged(object sender, EventArgs e)
        {

        }

        private void ApiSpecialNotes_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            // mark as visited
            apiSpecialNotesLink.LinkVisited = true;
            // open link using a default browser
            System.Diagnostics.Process.Start(ApiRelatedUrlConfig.API_SPECIAL_NOTES);

        }

        private void OndemandUsageEntryLink_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            // mark as visited
            ondemandUsageEntryLink.LinkVisited = true;
            // open link using a default browser
            System.Diagnostics.Process.Start(ApiRelatedUrlConfig.ONDEMAND_API_USAGE_ENTRY);
        }

        private void CheckDebugMode_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void TabDeveloper_Click(object sender, EventArgs e)
        {

        }

        private void CheckIsOndemandEndpointEnabled_CheckedChanged(object sender, EventArgs e)
        {
            if (checkIsOndemandEndpointEnabled.Checked)
            {
                checkForceOndemandApi.Visible = true; 
                forceOndemandEndpointDesc.Visible = true;
            }
            else 
            {
                checkForceOndemandApi.Visible = false;
                forceOndemandEndpointDesc.Visible = false;
                checkForceOndemandApi.Checked = false;
            }
        }

        private void CheckForceOndemandEndpoint_CheckedChanged(object sender, EventArgs e)
        {

        }

    }
}
