using BuffettCodeCommon.Validator;
using System.Text;
namespace BuffettCodeAddinRibbon.Settings
{
    public enum CSVOutputDestination
    {
        NewFile = 0,
        NewSheet = 1,
    }

    public class CSVOutputSettings
    {
        public static CSVOutputSettings Create(Encoding encoding, CSVOutputDestination destination)
        {
            CSVOutputEncodingValidator.Validate(encoding);
            return new CSVOutputSettings(encoding, destination);
        }

        private CSVOutputSettings(Encoding encoding, CSVOutputDestination destination)
        {
            Encoding = encoding;
            Destination = destination;
        }
        public Encoding Encoding { get; set; }
        public CSVOutputDestination Destination { get; set; }

    }
}
