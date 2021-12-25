using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BuffettCodeCommon.Period.Tests
{
    [TestClass()]
    public class ComparablePeriodUtilTests
    {
        [TestMethod()]
        public void GetGapTest()
        {
            // quarter
            var fq2020Q1 = FiscalQuarterPeriod.Create(2020, 1);
            var fq2020Q2 = FiscalQuarterPeriod.Create(2020, 2);
            Assert.AreEqual(1, ComparablePeriodUtil.GetGap(fq2020Q1, (IPeriod)fq2020Q2));

            // day
            var day20200101 = DayPeriod.Create(2020, 1, 1);
            var day20200102 = DayPeriod.Create(2020, 1, 2);
            Assert.AreEqual(1, ComparablePeriodUtil.GetGap(day20200101, (IPeriod)day20200102));
        }

        [TestMethod()]
        public void GetGapFiscalQuarterPeriodTest()
        {
            var fq2020Q1 = FiscalQuarterPeriod.Create(2020, 1);
            var fq2020Q2 = FiscalQuarterPeriod.Create(2020, 2);
            var fq2021Q1 = FiscalQuarterPeriod.Create(2021, 1);
            var fq2021Q4 = FiscalQuarterPeriod.Create(2021, 4);
            Assert.AreEqual(0, ComparablePeriodUtil.GetGap(fq2020Q1, fq2020Q1));
            Assert.AreEqual(1, ComparablePeriodUtil.GetGap(fq2020Q1, fq2020Q2));
            Assert.AreEqual(4, ComparablePeriodUtil.GetGap(fq2020Q1, fq2021Q1));
            Assert.AreEqual(7, ComparablePeriodUtil.GetGap(fq2020Q1, fq2021Q4));
        }

        [TestMethod()]
        public void GetGapDayPeriodTest()
        {
            var day20200101 = DayPeriod.Create(2020, 1, 1);
            var day20200102 = DayPeriod.Create(2020, 1, 2);
            var day20210101 = DayPeriod.Create(2021, 1, 1);
            Assert.AreEqual(0, ComparablePeriodUtil.GetGap(day20200101, day20200101));
            Assert.AreEqual(1, ComparablePeriodUtil.GetGap(day20200101, day20200102));
            Assert.AreEqual(366, ComparablePeriodUtil.GetGap(day20200101, day20210101));
        }
    }
}