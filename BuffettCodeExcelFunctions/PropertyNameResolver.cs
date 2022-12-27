using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuffettCodeExcelFunctions
{
    public class PropertyNameResolver
    {

        private static readonly ReadOnlyDictionary<string, string> ITEM_NAME_ALIASES = new ReadOnlyDictionary<string, string>(new Dictionary<string, string>
        {
            {"2y_beta", "beta.years_2.beta"},
            {"3y_beta", "beta.years_3.beta"},
            {"5y_beta", "beta.years_5.beta"},
            {"2y_beta_r2", "beta.years_2.r_squared"},
            {"3y_beta_r2", "beta.years_3.r_squared"},
            {"5y_beta_r2", "beta.years_5.r_squared"},
            {"2y_beta_count", "beta.years_2.count"},
            {"3y_beta_count", "beta.years_3.count"},
            {"5y_beta_count", "beta.years_5.count"}
        });


        public static string Resolve(string rawPropertyName)
        {
            if (ITEM_NAME_ALIASES.ContainsKey(rawPropertyName))
            {
                return ITEM_NAME_ALIASES[rawPropertyName];
            }
            else
            {
                return rawPropertyName;
            }
        }
    }
}