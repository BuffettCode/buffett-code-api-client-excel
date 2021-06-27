using BuffettCodeCommon.Validator;
using System.Text;
namespace BuffettCodeAddinRibbon.Settings
{
    public enum CSVOutputDestination
    {
        NewFile = 0,
        NewSheet = 1,
    }

    public class CsvFileOutputSettings
    {
        public static CsvFileOutputSettings Create(Encoding encoding, CSVOutputDestination destination)
        {
            CSVOutputEncodingValidator.Validate(encoding);
            return new CsvFileOutputSettings(encoding, destination);
        }

        private CsvFileOutputSettings(Encoding encoding, CSVOutputDestination destination)
        {
            Encoding = encoding;
            Destination = destination;
        }
        public Encoding Encoding { get; set; }
        public CSVOutputDestination Destination { get; set; }

    }
}
