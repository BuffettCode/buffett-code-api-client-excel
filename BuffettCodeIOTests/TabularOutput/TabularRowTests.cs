using BuffettCodeIO.Property;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace BuffettCodeIO.TabluarOutput.Tests
{
    [TestClass()]
    public class TabularRowTests
    {
        private static readonly string key = "test_key";
        private static readonly string name = "名前";
        private static readonly string unit = "円";

        [TestMethod()]
        public void CreateTest()
        {
            var row = TabularRow.Create(key, name, unit);
            Assert.AreEqual(key, row.Key);
            Assert.AreEqual(name, row.Name);
            Assert.AreEqual(unit, row.Unit);
        }

        [TestMethod()]
        public void CreateTest1()
        {
            var desc = new PropertyDescription(key, name, unit);
            var row = TabularRow.Create(desc);
            Assert.AreEqual(key, row.Key);
            Assert.AreEqual(name, row.Name);
            Assert.AreEqual(unit, row.Unit);
        }

        [TestMethod()]
        public void AddTest()
        {
            // add succeeded
            var row = TabularRow.Create(key, name, unit);
            var value = "dummy";
            Assert.AreEqual(0, row.Values.Count);
            row.Add(value);
            Assert.AreEqual(1, row.Values.Count);
            Assert.AreEqual(value, row.Values[0]);

            // value must not contain "," 
            Assert.ThrowsException<ArgumentException>(() => row.Add("dummy, dummy"));
        }
    }
}