using BuffettCodeCommon.Exception;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BuffettCodeCommon.Period.Tests
{
    [TestClass()]
    public class RelativeFiscalQuarterPeriodTests
    {
        [TestMethod()]
        public void CreateLatestTest()
        {
            Assert.AreEqual(RelativeFiscalQuarterPeriod.Create(0, 0), RelativeFiscalQuarterPeriod.CreateLatest());
        }

        [TestMethod()]
        public void FiscalYearAsStringTest()
        {
            var period0 = RelativeFiscalQuarterPeriod.Create(0, 1);
            Assert.AreEqual("LY", period0.FiscalYearAsString());

            var period1 = RelativeFiscalQuarterPeriod.Create(1, 2);
            Assert.AreEqual("LY-1", period1.FiscalYearAsString());
        }

        [TestMethod()]
        public void FiscalQuarterAsStringTest()
        {
            var period0 = RelativeFiscalQuarterPeriod.Create(0, 0);
            Assert.AreEqual("LQ", period0.FiscalQuarterAsString());

            var period1 = RelativeFiscalQuarterPeriod.Create(1, 2);
            Assert.AreEqual("LQ-2", period1.FiscalQuarterAsString());

        }

        [TestMethod()]
        public void CompareToTest()
        {
            var period0 = RelativeFiscalQuarterPeriod.Create(0, 1);
            var period1 = RelativeFiscalQuarterPeriod.Create(1, 2);
            var period2 = RelativeFiscalQuarterPeriod.Create(1, 2);
            var period3 = RelativeFiscalQuarterPeriod.Create(0, 6);

            Assert.IsTrue(period0.CompareTo(period1) < 0);
            Assert.IsTrue(period0.CompareTo(period2) < 0);
            Assert.IsTrue(period1.CompareTo(period2) == 0);
            Assert.IsTrue(period1.CompareTo(period3) == 0);
        }

        [TestMethod()]
        public void NextTest()
        {
            var period = RelativeFiscalQuarterPeriod.Create(0, 2);
            Assert.AreEqual(
                RelativeFiscalQuarterPeriod.Create(0, 1),
                period.Next()
                );
            Assert.AreEqual(
                RelativeFiscalQuarterPeriod.Create(0, 0),
                period.Next().Next()
                );
            Assert.IsNull(period.Next().Next().Next());
        }

        [TestMethod()]
        public void ToV2ParameterTest()
        {
            var param1 = RelativeFiscalQuarterPeriod.Create(0, 1).ToV2Parameter();
            Assert.AreEqual("LY", param1["fy"]);
            Assert.AreEqual("LQ-1", param1["fq"]);
            var param2 = RelativeFiscalQuarterPeriod.Create(1, 2).ToV2Parameter
                ();
            Assert.AreEqual("LY-1", param2["fy"]);
            Assert.AreEqual("LQ-2", param2["fq"]);
        }

        [TestMethod()]
        public void ToV3ParameterTest()
        {
            var param1 = RelativeFiscalQuarterPeriod.Create(0, 1).ToV2Parameter();
            Assert.AreEqual("LY", param1["fy"]);
            Assert.AreEqual("LQ-1", param1["fq"]);
            var param2 = RelativeFiscalQuarterPeriod.Create(1, 2).ToV2Parameter
                ();
            Assert.AreEqual("LY-1", param2["fy"]);
            Assert.AreEqual("LQ-2", param2["fq"]);
        }

        [TestMethod()]
        public void ToStringTest()
        {
            Assert.AreEqual("LYLQ-1", RelativeFiscalQuarterPeriod.Create(0, 1).ToString());
            Assert.AreEqual("LY-1LQ-2", RelativeFiscalQuarterPeriod.Create(1, 2).ToString());
        }

        [TestMethod()]
        public void GetHashCodeTest()
        {
            var hashCode = RelativeFiscalQuarterPeriod.Create(0, 1).GetHashCode();
            Assert.AreEqual((0, 1).GetHashCode(), hashCode);
            Assert.AreNotEqual((1, 2).GetHashCode(), hashCode);
        }

        [TestMethod()]
        public void EqualsTest()
        {
            var a = RelativeFiscalQuarterPeriod.Create(0, 1);
            var b = RelativeFiscalQuarterPeriod.Create(1, 2);
            var c = RelativeFiscalQuarterPeriod.Create(1, 2);
            Assert.IsFalse(a.Equals(b));
            Assert.IsFalse(b.Equals(a));
            Assert.IsFalse(a.Equals(c));
            Assert.IsFalse(a.Equals(null));
            Assert.IsTrue(b.Equals(c));
        }

        [TestMethod()]
        public void SortTest()
        {
            var a = RelativeFiscalQuarterPeriod.Create(0, 1);
            var b = RelativeFiscalQuarterPeriod.Create(0, 3);
            var c = RelativeFiscalQuarterPeriod.Create(1, 0);
            var d = RelativeFiscalQuarterPeriod.Create(1, 1);
            var e = RelativeFiscalQuarterPeriod.Create(0, 6);

            var random = new Random();

            var randomOrdreList = new List<RelativeFiscalQuarterPeriod> { a, b, c, d, e }.OrderBy(_ => random.Next());

            var sorted = new List<RelativeFiscalQuarterPeriod> { a, b, c, d, e }.OrderBy(_ => _).ToArray();

            Assert.AreEqual(a, sorted[0]);
            Assert.AreEqual(b, sorted[1]);
            Assert.AreEqual(c, sorted[2]);
            Assert.AreEqual(d, sorted[3]);
            Assert.AreEqual(e, sorted[4]);
        }

        [TestMethod()]
        public void ParseTest()
        {
            var period0 = RelativeFiscalQuarterPeriod.Parse("LYLQ");
            Assert.AreEqual(RelativeFiscalQuarterPeriod.Create(0, 0), period0);
            var period1 = RelativeFiscalQuarterPeriod.Parse("LY-1LQ");
            Assert.AreEqual(RelativeFiscalQuarterPeriod.Create(1, 0), period1);
            var period2 = RelativeFiscalQuarterPeriod.Parse("LYLQ-20");
            Assert.AreEqual(RelativeFiscalQuarterPeriod.Create(0, 20), period2);
            var period3 = RelativeFiscalQuarterPeriod.Parse("LY-10LQ-2");
            Assert.AreEqual(RelativeFiscalQuarterPeriod.Create(10, 2), period3);
            Assert.ThrowsException<ValidationError>(() => RelativeFiscalQuarterPeriod.Parse("dummy"));
            Assert.ThrowsException<ValidationError>(() => RelativeFiscalQuarterPeriod.Parse("LY-0LQ"));
            Assert.ThrowsException<ValidationError>(() => RelativeFiscalQuarterPeriod.Parse("LYLQ-0"));
        }

        [TestMethod()]
        public void TotalPrevQuartersTest()
        {
            Assert.AreEqual((uint)1, RelativeFiscalQuarterPeriod.Create(0, 1).TotalPrevQuarters);
            Assert.AreEqual((uint)3, RelativeFiscalQuarterPeriod.Create(0, 3).TotalPrevQuarters);
            Assert.AreEqual((uint)4, RelativeFiscalQuarterPeriod.Create(1, 0).TotalPrevQuarters);
            Assert.AreEqual((uint)5, RelativeFiscalQuarterPeriod.Create(1, 1).TotalPrevQuarters);
            Assert.AreEqual((uint)6, RelativeFiscalQuarterPeriod.Create(0, 6).TotalPrevQuarters);
        }
    }


}