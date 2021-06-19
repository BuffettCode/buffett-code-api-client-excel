using BuffettCodeCommon.Config;
using BuffettCodeCommon.Exception;
using System.Text;

namespace BuffettCodeCommon.Validator
{
    public class CSVOutputEncodingValidator
    {
        public static void Validate(Encoding encoding)
        {
            if (!CSVOutputEncoding.SupportedEncodings.Contains(encoding))
            {
                throw new ValidationError($"encoding={encoding} is not supported.");
            }
        }
    }
}