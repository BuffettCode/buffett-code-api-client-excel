using BuffettCodeAddinRibbon.Settings;
using BuffettCodeCommon;
using BuffettCodeCommon.Exception;
using Microsoft.Office.Tools.Ribbon;
using System;
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
            var config = Configuration.GetInstance();
            var configSettings = AddinSettings.Create(config);
            var form = new SettingForm(configSettings);
            form.ShowDialog();
            if (form.DialogResult == DialogResult.OK)
            {
                try
                {
                    var newSettings = AddinSettings.Create(form.GetAPIKey(), form.IsOndemandEndpointEnabled(), form.IsDebugMode());
                    newSettings.SaveToConfiguration(config);
                }
                catch (Exception exp)
                {
                    var bce = BuffettCodeExceptionFinder.Find(exp);
                    if (bce is ValidationError)
                    {
                        MessageBox.Show("API KEYの値が間違っています", "設定", MessageBoxButtons.OK);
                    }
                    else
                    {
                        MessageBox.Show("設定が間違っています", "設定", MessageBoxButtons.OK);
                    }
                }
            }
        }

        private void CSVButton_Click(object sender, RibbonControlEventArgs e)
        {
            CsvDownloadForm form = new CsvDownloadForm();
            form.ShowDialog();
        }

        private void RefreshButton_Click(object sender, RibbonControlEventArgs e)
        {
            BuffettCodeAddinCache.Dispose();
            Globals.ThisAddIn.Application.CalculateFullRebuild();
        }
    }
}
