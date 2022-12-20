using BuffettCodeCommon;
using BuffettCodeCommon.Exception;
using BuffettCodeIO.Property;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BuffettCodeIO.Parser
{
    public class PropertiesParser
    {
        public static PropertyDictionary Parse(IEnumerable<JProperty> jProperties)
        {
            try
            {
                var props = new List<JProperty>();
                foreach (JToken root in jProperties)
                {
                    CollectJProperties(root, n => props.Add(n as JProperty));
                }
                var properties = props.ToDictionary(p => p.Path, p => ValueNormalizer.Normalize(p.Value.ToString()));
                return new PropertyDictionary(properties);
            }
            catch (Exception e)
            {
                throw new ApiResponseParserException("parse properties failed", e);
            }
        }


        private static void CollectJProperties(JToken node, Action<JToken> action)
        {
            if (node is JProperty)
            {
                action(node);
            }
            foreach (JToken child in node.Children())
            {
                CollectJProperties(child, action);
            }
        }

        private static string GetName(JProperty property)
        {
            var propLen = PropertyNames.Data.Length + 1;
            var path = property.Path;
            return path.Substring(path.IndexOf(PropertyNames.Data) + propLen);
        }
    }
}