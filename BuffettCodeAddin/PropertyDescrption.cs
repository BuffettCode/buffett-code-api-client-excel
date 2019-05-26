using System.Linq;

namespace BuffettCodeAddin
{
    public class PropertyDescrption
    {
        private static readonly string[] INVALID_UNITS = { "なし", "単位無し" };

        public string Name { get; set; }

        public string Label { get; set; }

        private string unit;

        public string Unit
        {
            get { return INVALID_UNITS.Contains(unit) ? "" : unit; }
            set { unit = value; }
        }

        public PropertyDescrption(string name, string label, string unit)
        {
            Name = name;
            Label = label;
            this.unit = unit;
        }
    }
}
