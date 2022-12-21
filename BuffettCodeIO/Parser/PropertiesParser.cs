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
                var properties = FlattenProperties(jProperties);
                return new PropertyDictionary(properties);
            }
            catch (Exception e)
            {
                throw new ApiResponseParserException("parse properties failed", e);
            }
        }

        private static Dictionary<string, string> FlattenProperties(IEnumerable<JProperty> jProperties)
        {
            var props = new List<JProperty>();
            foreach (JToken root in jProperties)
            {
                Walk(root, n => props.Add(n as JProperty));
            }
            return props.ToDictionary(p => p.Path, p => ValueNormalizer.Normalize(p.Value.ToString()));
        }

        private static void Walk(JToken node, Action<JToken> action)
        {
            if (node is JProperty)
            {
                action(node);
            }
            foreach (JToken child in node.Children())
            {
                Walk(child, action);
            }
        }
    }
}