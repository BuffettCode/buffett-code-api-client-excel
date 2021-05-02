using BuffettCodeCommon;
using Microsoft.Office.Tools.Ribbon;
using System.Windows.Forms;

namespace BuffettCodeAddinRibbon
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
            var maxDegreeOfParallelism = AddinFacade.GetMaxDegreeOfParallelism();
            var debugMode = AddinFacade.IsDebugMode();
            var form = new SettingForm(configuredKey, maxDegreeOfParallelism, debugMode);
            form.ShowDialog();
            if (form.DialogResult == DialogResult.OK)
            {
                var inputtedKey = form.GetAPIKey();
                if (!string.IsNullOrEmpty(inputtedKey) && !inputtedKey.Equals(configuredKey))
                {
                    AddinFacade.UpdateApiKey(inputtedKey);
                }
                var inputtedMaxDegreeOfParallelism = form.GetMaxDegreeOfParallelism();
                if (maxDegreeOfParallelism != inputtedMaxDegreeOfParallelism)
                {
                    AddinFacade.UpdateMaxDegreeOfParallelism(inputtedMaxDegreeOfParallelism);
                }
                var inputtedDebugMode = form.IsDebugMode();
                if (debugMode != inputtedDebugMode)
                {
                    AddinFacade.UpdateDebugMode(inputtedDebugMode);
                }
            }
        }

        private void CSVButton_Click(object sender, RibbonControlEventArgs e)
        {
            string apiKey = AddinFacade.GetApiKey();
            CSVForm form = new CSVForm(apiKey);
            form.ShowDialog();
        }

        private void RefreshButton_Click(object sender, RibbonControlEventArgs e)
        {
            AddinFacade.ClearCache();
            BuffettCodeAddinRibbon.Globals.ThisAddIn.Application.CalculateFullRebuild();
        }
    }
}
