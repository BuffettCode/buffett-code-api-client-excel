using BuffettCodeCommon;
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
                    var descs = new List<JProperty>();
                    foreach (JToken root in columnDescription)
                    {
                        WalkDescription(root, n => descs.Add(n as JProperty));
                    }

                    var dict = descs.Select(t => ToPropertyDescription(t))
                                    .ToDictionary(p => p.Name, p => p);
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

        private static void WalkDescription(JToken node, Action<JToken> action)
        {
            if (node is JProperty && IsDescriptionColumn(node as JProperty))
            {
                action(node);
            }
            else
            {
                foreach (JToken child in node.Children())
                {
                    WalkDescription(child, action);
                }
            }
        }

        private static bool IsDescriptionColumn(JProperty property)
        {
            return property.Value[PropertyNames.NameJp] != null && property.Value[PropertyNames.Unit] != null;
        }

        private static PropertyDescription ToPropertyDescription(JProperty property)
        {
            var propLen = PropertyNames.ColumnDescription.Length + 1;
            var path = property.Path;
            var name = path.Substring(path.IndexOf(PropertyNames.ColumnDescription) + propLen);
            var label = property.Value[PropertyNames.NameJp].ToString();
            var unit = property.Value[PropertyNames.Unit].ToString();
            return new PropertyDescription(name, label, unit);
        }
    }
}