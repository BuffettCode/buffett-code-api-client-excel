using BuffettCodeCommon.Exception;
using BuffettCodeCommon.Validator;
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
            var configuredKey = AddinFacade.GetApiKey();
            var maxDegreeOfParallelism = AddinFacade.GetMaxDegreeOfParallelism();
            var debugMode = AddinFacade.IsDebugMode();
            var form = new SettingForm(configuredKey, maxDegreeOfParallelism, debugMode);
            form.ShowDialog();
            if (form.DialogResult == DialogResult.OK)
            {
                var apiKey = form.GetAPIKey();
                try
                {
                    ApiKeyValidator.Validate(apiKey);
                    AddinFacade.UpdateApiKey(apiKey);
                }
                catch (AddinConfigurationException)
                {
                    MessageBox.Show("API KEYが間違っています", "設定", MessageBoxButtons.OK);
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
            CSVForm form = new CSVForm();
            form.ShowDialog();
        }

        private void RefreshButton_Click(object sender, RibbonControlEventArgs e)
        {
            AddinFacade.ClearCache();
            Globals.ThisAddIn.Application.CalculateFullRebuild();
        }
    }
}
