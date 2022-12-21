using BuffettCodeCommon.Exception;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuffettCodeCommon.Validator
{
    public static class MonthValidator
    {
        public static void Validate(uint maybeMonth)
        {
            if (maybeMonth <= 0)
            {
                throw new ValidationError($"month={maybeMonth} is too small.");
            }
            else if (maybeMonth > 13)
            {
                throw new ValidationError($"month={maybeMonth} is too large.");
            }
        }
    }
}