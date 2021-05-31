using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;

namespace BuffettCodeCommon.Period.Tests
{
    [TestClass()]
    public class DayTests
    {
        [TestMethod()]
        public void CreateTest()
        {
            uint y = 2011;
            uint m = 1;
            uint d = 1;

            var day = Day.Create(y, m, d);
            Assert.AreEqual((int)y, day.Value.Year);
            Assert.AreEqual((int)m, day.Value.Month);
            Assert.AreEqual((int)y, day.Value.Year);
        }

        [TestMethod()]
        public void EqualsTest()
        {
            var a = Day.Create(2020, 1, 1);
            var b = Day.Create(2020, 1, 1);
            var c = Day.Create(2021, 1, 1);
            Assert.AreEqual(a, b);
            Assert.AreNotEqual(a, c);
        }

        [TestMethod()]
        public void GetHashCodeTest()
        {
            var a = Day.Create(2020, 1, 1).GetHashCode();
            var b = Day.Create(2020, 1, 1).GetHashCode();
            var c = Day.Create(2021, 1, 1).GetHashCode();
            Assert.AreEqual(a, b);
            Assert.AreNotEqual(a, c);

        }

        [TestMethod()]
        public void CompareToTest()
        {
            var a = Day.Create(2020, 1, 1);
            var b = Day.Create(2020, 1, 1);
            var c = Day.Create(2021, 1, 1);
            var d = Day.Create(2020, 2, 3);
            Assert.AreEqual(0, a.CompareTo(b));
            Assert.AreEqual(-1, a.CompareTo(c));
            Assert.AreEqual(-1, a.CompareTo(d));
            Assert.AreEqual(1, c.CompareTo(d));
        }

        [TestMethod()]
        public void SortTest()
        {
            var a = Day.Create(2020, 1, 1);
            var b = Day.Create(2021, 1, 1);
            var c = Day.Create(2020, 2, 3);
            var sorted = new List<Day> { a, b, c }.OrderBy(_ => _).ToArray();

            Assert.AreEqual(a, sorted[0]);
            Assert.AreEqual(c, sorted[1]);
            Assert.AreEqual(b, sorted[2]);
        }
    }
}