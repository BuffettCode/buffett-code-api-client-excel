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
                var properties = jProperties.ToDictionary(p => p.Name, p => ValueNormalizer.Normalize(p.Value.ToString()));
                return new PropertyDictionary(properties);
            }
            catch (Exception e)
            {
                throw new ApiResponseParserException("parse properties failed", e);
            }
        }
    }
}