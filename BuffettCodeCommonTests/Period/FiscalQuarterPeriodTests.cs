using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;


namespace BuffettCodeCommon.Period.Tests
{
    [TestClass()]
    public class FiscalQuarterPeriodTests
    {
        [TestMethod()]
        public void CreateFromStringTest()
        {
            var fyFqStr = "2018Q1";
            var period = FiscalQuarterPeriod.Parse(fyFqStr);
            Assert.AreEqual((uint)2018, period.Year);
            Assert.AreEqual((uint)1, period.Quarter);
            Assert.AreEqual(fyFqStr, period.ToString());
        }

        [TestMethod()]
        public void EqualsTest()
        {
            var a = FiscalQuarterPeriod.Create(2018, 1);
            var b = FiscalQuarterPeriod.Create("2018", "1");
            var c = FiscalQuarterPeriod.Create(2018, 2);
            Assert.IsTrue(a.Equals(b));
            Assert.IsTrue(b.Equals(a));
            Assert.IsFalse(a.Equals(c));
            Assert.IsFalse(b.Equals(c));
            Assert.IsFalse(a.Equals(null));
        }

        [TestMethod()]
        public void GetHashCodeTest()
        {
            var hashCode = FiscalQuarterPeriod.Create(2018, 1).GetHashCode();
            Assert.AreEqual((2018, 1).GetHashCode(), hashCode);
            Assert.AreNotEqual((2018, 2).GetHashCode(), hashCode);
        }

        [TestMethod()]
        public void CompareToTest()
        {
            var a = FiscalQuarterPeriod.Create(2018, 2);
            var b = FiscalQuarterPeriod.Create(2019, 1);
            var c = FiscalQuarterPeriod.Create("2017", "1");
            var d = FiscalQuarterPeriod.Create(2018, 2);

            Assert.AreEqual(-1, a.CompareTo(b));
            Assert.AreEqual(1, a.CompareTo(c));
            Assert.AreEqual(0, a.CompareTo(d));
            Assert.AreEqual(1, b.CompareTo(c));
            Assert.AreEqual(1, b.CompareTo(d));
            // null given
            Assert.ThrowsException<ArgumentNullException>(() => a.CompareTo(null));
        }

        [TestMethod()]
        public void SortTest()
        {
            var a = FiscalQuarterPeriod.Create(2018, 2);
            var b = FiscalQuarterPeriod.Create(2019, 1);
            var c = FiscalQuarterPeriod.Create("2017", "1");
            var d = FiscalQuarterPeriod.Create(2018, 3);
            var sorted = new List<FiscalQuarterPeriod> { a, b, c, d }.OrderBy(_ => _).ToArray();

            Assert.AreEqual(c, sorted[0]);
            Assert.AreEqual(a, sorted[1]);
            Assert.AreEqual(d, sorted[2]);
            Assert.AreEqual(b, sorted[3]);
        }

        [TestMethod()]
        public void NextTest()
        {
            Assert.AreEqual(FiscalQuarterPeriod.Create(2020, 2), FiscalQuarterPeriod.Create(2020, 1).Next());
            Assert.AreEqual(FiscalQuarterPeriod.Create(2021, 1), FiscalQuarterPeriod.Create(2020, 4).Next());
            Assert.AreEqual(FiscalQuarterPeriod.Create(2021, 1), FiscalQuarterPeriod.Create(2020, 5).Next());
        }

        [TestMethod()]
        public void BeforeTest()
        {
            Assert.AreEqual(FiscalQuarterPeriod.Create(2020, 3), FiscalQuarterPeriod.Create(2021, 1).Before(0, 2));
            Assert.AreEqual(FiscalQuarterPeriod.Create(2020, 3), FiscalQuarterPeriod.Create(2022, 3).Before(1, 4));
            Assert.AreEqual(FiscalQuarterPeriod.Create(2020, 2), FiscalQuarterPeriod.Create(2022, 3).Before(1, 5));
            Assert.AreEqual(FiscalQuarterPeriod.Create(2020, 1), FiscalQuarterPeriod.Create(2021, 3).Before(1, 2));
            Assert.AreEqual(FiscalQuarterPeriod.Create(2020, 1), FiscalQuarterPeriod.Create(2021, 3).Before(0, 6));
        }

    }
}