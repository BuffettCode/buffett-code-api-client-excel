using System.Collections.Generic;
using System.Linq;

namespace BuffettCodeIO.Property
{

    public class PropertyDescriptionDictionary
    {
        private readonly IDictionary<string, PropertyDescription> descriptions;

        public PropertyDescriptionDictionary(IDictionary<string, PropertyDescription> descriptions)
        {
            this.descriptions = descriptions;
        }

        public static PropertyDescriptionDictionary Empty()
        {
            var empty = new Dictionary<string, PropertyDescription>();
            // return empty container
            return new PropertyDescriptionDictionary(empty);
        }

        public PropertyDescription Get(string propertyName)
        {
            return descriptions.Keys.Contains(propertyName) ? descriptions[propertyName] : PropertyDescription.Empty();
        }
        public override bool Equals(object obj)
        {
            if (obj is null)
            {
                return false;
            }
            else if (this.GetType() != obj.GetType())
            {
                return false;
            }
            else if (this.GetHashCode() != obj.GetHashCode())
            {
                return false;
            }
            else
            {
                var pdc = (PropertyDescriptionDictionary)obj;
                return this.descriptions.SequenceEqual(pdc.descriptions);
            }
        }

        public override int GetHashCode()
        =>
            descriptions.Sum(d => d.GetHashCode() / 1024).GetHashCode();

        public int Count => descriptions.Count;
    }
}