using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;

namespace BuffettCodeCommon.Period.Tests
{
    [TestClass()]
    public class DayPeriodTests
    {
        [TestMethod()]
        public void CreateFromStringTest()
        {
            // yyyy-MM-dd
            var dateString = "2012-01-02";
            var period = DayPeriod.Parse(dateString);
            Assert.AreEqual(2012, period.Value.Year);
            Assert.AreEqual(1, period.Value.Month);
            Assert.AreEqual(2, period.Value.Day);
            Assert.AreEqual(dateString, period.ToString());

            // ISO 8601
            var iso8601String = "2016-11-30T00:00:00.000Z";
            var period2 = DayPeriod.Parse(iso8601String);
            Assert.AreEqual(2016, period2.Value.Year);
            Assert.AreEqual(11, period2.Value.Month);
            Assert.AreEqual(30, period2.Value.Day);
            Assert.AreEqual("2016-11-30", period2.ToString());
        }

        [TestMethod()]
        public void CreateTest()
        {
            uint y = 2011;
            uint m = 1;
            uint d = 1;

            var day = DayPeriod.Create(y, m, d);
            Assert.AreEqual((int)y, day.Value.Year);
            Assert.AreEqual((int)m, day.Value.Month);
            Assert.AreEqual((int)y, day.Value.Year);
        }

        [TestMethod()]
        public void EqualsTest()
        {
            var a = DayPeriod.Create(2020, 1, 1);
            var b = DayPeriod.Create(2020, 1, 1);
            var c = DayPeriod.Create(2021, 1, 1);
            Assert.AreEqual(a, b);
            Assert.AreNotEqual(a, c);
        }

        [TestMethod()]
        public void GetHashCodeTest()
        {
            var a = DayPeriod.Create(2020, 1, 1).GetHashCode();
            var b = DayPeriod.Create(2020, 1, 1).GetHashCode();
            var c = DayPeriod.Create(2021, 1, 1).GetHashCode();
            var d = DayPeriod.Create(2020, 2, 1).GetHashCode();
            var e = DayPeriod.Create(2020, 1, 2).GetHashCode();
            Assert.AreEqual(a, b);
            Assert.AreNotEqual(a, c);
            Assert.AreNotEqual(a, d);
            Assert.AreNotEqual(a, e);

        }

        [TestMethod()]
        public void CompareToTest()
        {
            var a = DayPeriod.Create(2020, 1, 1);
            var b = DayPeriod.Create(2020, 1, 1);
            var c = DayPeriod.Create(2021, 1, 1);
            var d = DayPeriod.Create(2020, 2, 3);
            Assert.AreEqual(0, a.CompareTo(b));
            Assert.AreEqual(-1, a.CompareTo(c));
            Assert.AreEqual(-1, a.CompareTo(d));
            Assert.AreEqual(1, c.CompareTo(d));
        }

        [TestMethod()]
        public void SortTest()
        {
            var a = DayPeriod.Create(2020, 1, 1);
            var b = DayPeriod.Create(2021, 1, 1);
            var c = DayPeriod.Create(2020, 2, 3);
            var sorted = new List<DayPeriod> { a, b, c }.OrderBy(_ => _).ToArray();

            Assert.AreEqual(a, sorted[0]);
            Assert.AreEqual(c, sorted[1]);
            Assert.AreEqual(b, sorted[2]);
        }

        [TestMethod()]
        public void ToStringTest()
        {
            Assert.AreEqual("2020-01-01", DayPeriod.Create(2020, 1, 1).ToString());
        }

        [TestMethod()]
        public void NextTest()
        {
            var day = DayPeriod.Create(2020, 12, 31);
            Assert.AreEqual(day.Next(), DayPeriod.Create(2021, 1, 1));
        }

        [TestMethod()]
        public void PrevTest()
        {
            var day = DayPeriod.Create(2021, 1, 1);
            Assert.AreEqual(day.Prev(), DayPeriod.Create(2020, 12, 31));
        }
    }
}