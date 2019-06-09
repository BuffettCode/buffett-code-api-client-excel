using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuffettCodeAddin.Client
{
    /// <summary>
    /// テストAPIキーの機能制限に引っかかったときにthrowされる例外
    /// </summary>
    public class TestAPIConstraintException : BuffettCodeException
    {
    }
}
