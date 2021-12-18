using BuffettCodeIO.Property;
using System.Collections.Generic;

namespace BuffettCodeIO.TabularOutput
{
    public class TabularRow
    {
        private readonly string key;
        private readonly string name;
        private readonly string unit;
        private readonly IList<string> values = new List<string>();

        private TabularRow(string key, string name, string unit)
        {
            this.key = key;
            this.name = name;
            this.unit = unit;
        }

        public string Key => key;

        public string Name => name;

        public string Unit => unit;

        public static TabularRow Create(string key, string name, string unit) => new TabularRow(key, name, unit);

        public static TabularRow Create(PropertyDescription desc) => new TabularRow(desc.Name, desc.JpName, desc.Unit);


        public TabularRow Add(string value)
        {
            values.Add(value);
            return this;
        }

        public IList<string> Values => values;
    }
}