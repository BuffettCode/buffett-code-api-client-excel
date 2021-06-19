using BuffettCodeCommon.Exception;
using System.Collections.Generic;
using System.Linq;
namespace BuffettCodeIO.Property
{
    public class PropertyDictionary
    {
        private readonly IDictionary<string, string> properties;
        public PropertyDictionary(IDictionary<string, string> properties)
        {
            this.properties = properties;
        }

        public static PropertyDictionary Empty()
        {
            var empty = new Dictionary<string, string>();
            return new PropertyDictionary(empty);
        }

        public string Get(string propertyName)
        {
            if (!properties.Keys.Contains(propertyName))
            {
                throw new PropertyNotFoundException($"{propertyName} is not in {properties}");
            }
            return properties[propertyName];
        }

        public ICollection<string> Names => properties.Keys;

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
            {
                var pc = (PropertyDictionary)obj;
                return this.properties.SequenceEqual(pc.properties);
            }
        }

        public override int GetHashCode()
        {
            var kvStrArray = properties.OrderBy(p => p.Key).Select(p => $"{p.Key}:{p.Value}").ToArray();
            return string.Join(", ", kvStrArray).GetHashCode();
        }

        public int Count => properties.Count;

    }
}