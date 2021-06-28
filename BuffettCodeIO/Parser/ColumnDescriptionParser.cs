using BuffettCodeCommon.Exception;
using BuffettCodeIO.Property;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BuffettCodeIO.Parser
{
    public static class ColumnDescriptionParser
    {
        public static PropertyDescriptionDictionary Parse(IList<JToken> columnDescription)
        {
            if (columnDescription.Count > 0)
            {
                try
                {
                    var dict = columnDescription.First().SelectMany(t => t.Children()).Where(t => t is JProperty).Cast<JProperty>().Where(_ => !PropertyNames.IgnoredPropertyNames.Contains(_.Name)).Select(t => ToPropertyDescription(t)).ToDictionary(p => p.Name, p => p);
                    return new PropertyDescriptionDictionary(dict);
                }
                catch (Exception e)
                {
                    throw new ApiResponseParserException("parse column definitions failed", e);
                }
            }
            else
            {
                return PropertyDescriptionDictionary.Empty();
            }
        }

        private static PropertyDescription ToPropertyDescription(JProperty property)
        {
            var name = property.Name;
            var label = property.Value[PropertyNames.NameJp].ToString();
            var unit = property.Value[PropertyNames.Unit].ToString();

            return new PropertyDescription(name, label, unit);
        }

    }
}