using BuffettCodeAddin;
using Microsoft.Office.Tools.Ribbon;
using Microsoft.Win32;
using System;
using System.Windows.Forms;

namespace BuffettCode
{
    public partial class Ribbon
    {
        private void Ribbon_Load(object sender, RibbonUIEventArgs e)
        {

        }

        private void SettingButton_Click(object sender, RibbonControlEventArgs e)
        {
            Configuration.Reload();
            var configuredKey = AddinFacade.GetApiKey();
            var form = new SettingForm(configuredKey);
            form.ShowDialog();
            if (form.DialogResult == DialogResult.OK)
            {
                var inputtedKey = form.GetAPIKey();
                if (!string.IsNullOrEmpty(inputtedKey) && !inputtedKey.Equals(configuredKey))
                {
                    AddinFacade.UpdateApiKey(inputtedKey);
                }
            }
        }

        private void CSVButton_Click(object sender, RibbonControlEventArgs e)
        {
            string apiKey = AddinFacade.GetApiKey();
            CSVForm form = new CSVForm(apiKey);
            form.ShowDialog();
            if (form.DialogResult == DialogResult.OK)
            {

            }
        }

        private void RefreshButton_Click(object sender, RibbonControlEventArgs e)
        {
            AddinFacade.ClearCache();
            BuffettCode.Globals.ThisAddIn.Application.CalculateFullRebuild();
        }

        [Obsolete]
        private void SaveApiKeyToRegistry(string apiKey)
        {
            RegistryKey registryKey = Registry.CurrentUser.CreateSubKey(@"Software\BuffettCode");
            registryKey.SetValue("ApiKey", apiKey);
            registryKey.Close();
        }

        [Obsolete]
        private void UpdateApiKeyByFunction(string key)
        {
            var activeSheet = BuffettCode.Globals.ThisAddIn.Application.ActiveSheet as Microsoft.Office.Interop.Excel.Worksheet;
            activeSheet.Cells[1, 1] = "=BCODE_KEY(\"" + key + "\")";
            activeSheet.Cells[1, 1] = "";
            BuffettCodeAddin.Configuration.ApiKey = key;
        }

    }
}
