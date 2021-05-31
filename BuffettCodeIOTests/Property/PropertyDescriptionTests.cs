using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BuffettCodeIO.Property.Tests
{
    [TestClass()]
    public class PropertyDescriptionTests
    {
        private static readonly string name = "test_name";
        private static readonly string label = "test_label";
        private static readonly string unit = "test_unit";

        [TestMethod()]
        public void EmptyTest()
        {
            var empty = PropertyDescription.Empty();
            Assert.AreEqual("", empty.Name);
            Assert.AreEqual("", empty.Label);
            Assert.AreEqual("", empty.Unit);
        }

        [TestMethod()]
        public void GetHashCodeTest()
        {
            var a = new PropertyDescription(name, label, unit).GetHashCode();
            var b = new PropertyDescription(name, label, unit).GetHashCode();
            var c = new PropertyDescription("dummy", label, unit).GetHashCode();
            var d = new PropertyDescription(name, "dummy", unit).GetHashCode();
            var e = new PropertyDescription(name, label, "dummy").GetHashCode();
            Assert.AreEqual(a, (name, label, unit).GetHashCode());
            Assert.AreEqual(a, b);
            Assert.AreNotEqual(a, c);
            Assert.AreNotEqual(a, d);
            Assert.AreNotEqual(a, e);
        }

        [TestMethod()]
        public void EqualsTest()
        {
            var a = new PropertyDescription(name, label, unit);
            var b = new PropertyDescription(name, label, unit);
            var c = new PropertyDescription("dummy", label, unit);
            var d = new PropertyDescription(name, "dummy", unit);
            var e = new PropertyDescription(name, label, "dummy");
            Assert.AreEqual(a, b);
            Assert.AreNotEqual(a, c);
            Assert.AreNotEqual(a, d);
            Assert.AreNotEqual(a, e);

        }
    }
}