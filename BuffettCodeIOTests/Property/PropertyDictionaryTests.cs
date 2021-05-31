using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace BuffettCodeIO.Property.Tests
{
    [TestClass()]
    public class PropertyContainerTests
    {

        private static readonly IDictionary<string, string> properties = new Dictionary<string, string> { { "key", "value" } };


        [TestMethod()]
        public void EmptyTest()
        {
            var empty = PropertyDictionary.Empty();
            Assert.AreEqual(0, empty.Count);

        }

        [TestMethod()]
        public void GetTest()
        {
            var container = new PropertyDictionary(properties);
            Assert.AreEqual("value", container.Get("key"));
        }

        [TestMethod()]
        public void EqualsTest()
        {
            var a = new PropertyDictionary(properties);
            var b = new PropertyDictionary(properties);
            var c = PropertyDictionary.Empty();
            Assert.AreEqual(a, b);
            Assert.AreNotEqual(a, c);
            Assert.AreNotEqual(a, null);
        }

        [TestMethod()]
        public void GetHashCodeTest()
        {
            var a = new PropertyDictionary(properties).GetHashCode();
            var b = new PropertyDictionary(properties).GetHashCode();
            var c = PropertyDictionary.Empty().GetHashCode();
            Assert.AreEqual(a, b);
            Assert.AreNotEqual(a, c);
        }
    }
}