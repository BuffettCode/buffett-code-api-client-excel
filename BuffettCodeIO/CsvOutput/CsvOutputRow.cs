using BuffettCodeIO.Property;
using System;
using System.Collections.Generic;

namespace BuffettCodeIO.CsvOutput
{
    public class CsvOutputRow
    {
        private readonly string key;
        private readonly string name;
        private readonly string unit;
        private readonly IList<string> values = new List<string>();

        private CsvOutputRow(string key, string name, string unit)
        {
            this.key = key;
            this.name = name;
            this.unit = unit;
        }

        public string Key => key;

        public string Name => name;

        public string Unit => unit;

        public static CsvOutputRow Create(string key, string name, string unit) => new CsvOutputRow(key, name, unit);

        public static CsvOutputRow Create(PropertyDescription desc) => new CsvOutputRow(desc.Name, desc.Label, desc.Unit);


        public CsvOutputRow Add(string value)
        {
            if (value.Contains(","))
            {
                throw new ArgumentException($"value={value} contains ','");
            }
            values.Add(value);
            return this;
        }

        public IList<string> Values => values;
    }
}