using BuffettCodeCommon.Config;
using BuffettCodeIO.Property;
using System;
using System.Collections.Generic;
using System.Linq;


namespace BuffettCodeAddinRibbon
{
    public class CsvPropertyHelper
    {
        public static IList<string> CreatePropertyNameList(Quarter quarter) =>
    CreatePropertyList(CSVOutputProperties.QuarterExcludeProperties, CSVOutputProperties.QuarterOrderedProperties, quarter);

        private static IList<string> CreatePropertyList(string[] excludeProperties, string[] orderedProperties, IApiResource apiResource)
        {
            var result = orderedProperties.ToList<string>();
            foreach (var propertyName in apiResource.GetPropertyNames())
            {
                if (!orderedProperties.Contains(propertyName) && !excludeProperties.Contains(propertyName))
                {
                    result.Add(propertyName);
                }
            }
            return result;
        }


    }
}
