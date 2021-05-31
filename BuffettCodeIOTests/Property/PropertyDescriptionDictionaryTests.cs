using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace BuffettCodeIO.Property.Tests
{
    [TestClass()]
    public class PropertyDescriptionContainerTests
    {
        private static readonly string name = "test_name";
        private static readonly string label = "test_label";
        private static readonly string unit = "test_unit";
        private static readonly PropertyDescription description = new PropertyDescription(name, label, unit);
        private static readonly IDictionary<string, PropertyDescription> descriptions = new Dictionary<string, PropertyDescription> { { "key", description } };

        [TestMethod()]
        public void EmptyTest()
        {
            var empty = PropertyDescriptionDictionary.Empty();
            Assert.AreEqual(0, empty.Count);
        }

        [TestMethod()]
        public void GetTest()
        {
            var container = new PropertyDescriptionDictionary(descriptions);
            Assert.AreEqual(description, container.Get("key"));
        }

        [TestMethod()]
        public void EqualsTest()
        {
            var a = new PropertyDescriptionDictionary(descriptions);
            var b = new PropertyDescriptionDictionary(descriptions);
            var c = PropertyDescriptionDictionary.Empty();

            Assert.AreEqual(a, b);
            Assert.AreNotEqual(a, c);
        }

        [TestMethod()]
        public void GetHashCodeTest()
        {
            var a = new PropertyDescriptionDictionary(descriptions).GetHashCode();
            var b = new PropertyDescriptionDictionary(descriptions).GetHashCode();
            var c = PropertyDescriptionDictionary.Empty().GetHashCode();

            Assert.AreEqual(a, b);
            Assert.AreNotEqual(a, c);
        }
    }
}