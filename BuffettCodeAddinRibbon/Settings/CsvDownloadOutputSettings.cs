using BuffettCodeCommon.Config;
using BuffettCodeCommon.Validator;
using System.Text;
namespace BuffettCodeAddinRibbon.Settings
{

    public class CsvDownloadOutputSettings
    {
        public static CsvDownloadOutputSettings Create(Encoding encoding, TabularOutputDestination destination)
        {
            CSVOutputEncodingValidator.Validate(encoding);
            return new CsvDownloadOutputSettings(encoding, destination);
        }

        private CsvDownloadOutputSettings(Encoding encoding, TabularOutputDestination destination)
        {
            Encoding = encoding;
            Destination = destination;
        }
        public Encoding Encoding { get; set; }
        public TabularOutputDestination Destination { get; set; }

    }
}
