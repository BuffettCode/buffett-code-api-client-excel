using BuffettCodeCommon.Period;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace BuffettCodeCommonTests.Period
{
    [TestClass]
    public class YearMontPeriodTest
    {
        [TestMethod]
        public void ParseTest()
        {
            var period = YearMonthPeriod.Parse("202202");
            Assert.AreEqual(period.Year, (uint)2022);
            Assert.AreEqual(period.Month, (uint)2);
        }

        [TestMethod]
        public void EqualsTest()
        {
            var period1 = YearMonthPeriod.Create(2022, 1);
            var period2 = YearMonthPeriod.Create(2022, 1);
            var period3 = YearMonthPeriod.Create(2022, 2);
            var period4 = YearMonthPeriod.Create(2020, 1);

            Assert.IsTrue(period1.Equals(period1));
            Assert.IsTrue(period1.Equals(period2));
            Assert.IsTrue(period2.Equals(period1));
            Assert.IsFalse(period1.Equals(period3));
            Assert.IsFalse(period1.Equals(period4));
            Assert.IsFalse(period3.Equals(period4));
        }


        [TestMethod()]
        public void GetHashCodeTest()
        {
            var hashCode = YearMonthPeriod.Create(2018, 5).GetHashCode();
            Assert.AreEqual((2018, 5).GetHashCode(), hashCode);
            Assert.AreNotEqual((2018, 6).GetHashCode(), hashCode);
        }

        [TestMethod()]
        public void CompareToTest()
        {
            var a = YearMonthPeriod.Create(2018, 2);
            var b = YearMonthPeriod.Create(2019, 1);
            var c = YearMonthPeriod.Create(2017, 1);
            var d = YearMonthPeriod.Create(2018, 2);

            Assert.AreEqual(-1, a.CompareTo(b));
            Assert.AreEqual(1, a.CompareTo(c));
            Assert.AreEqual(0, a.CompareTo(d));
            Assert.AreEqual(1, b.CompareTo(c));
            Assert.AreEqual(1, b.CompareTo(d));
            // null given
            Assert.ThrowsException<ArgumentNullException>(() => a.CompareTo(null));
        }
    }
}