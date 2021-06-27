using BuffettCodeCommon.Exception;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace BuffettCodeCommon.Period.Tests
{
    [TestClass()]
    public class PeriodRangeTests
    {
        [TestMethod()]
        public void CreateForFiscalQuarterPeriodTest()
        {
            var from = FiscalQuarterPeriod.Create(2020, 1);
            var to = FiscalQuarterPeriod.Create(2020, 2);
            var fqRange = PeriodRange<FiscalQuarterPeriod>.Create(from, to);
            Assert.AreEqual(from, fqRange.From);
            Assert.AreEqual(to, fqRange.To);
            Assert.ThrowsException<ValidationError>(() => PeriodRange<FiscalQuarterPeriod>.Create(to, from));
        }

        [TestMethod()]
        public void CreateForDayPeriodTest()
        {
            var from = DayPeriod.Create(2020, 1, 1);
            var to = DayPeriod.Create(2020, 1, 2);
            var fqRange = PeriodRange<DayPeriod>.Create(from, to);
            Assert.AreEqual(from, fqRange.From);
            Assert.AreEqual(to, fqRange.To);
            Assert.ThrowsException<ValidationError>(() => PeriodRange<DayPeriod>.Create(to, from));
        }

        [TestMethod()]
        public void SliceFiscalQuarterTest()
        {
            var from = FiscalQuarterPeriod.Create(2016, 1);
            var to = FiscalQuarterPeriod.Create(2020, 4);
            var quarterRange = PeriodRange<FiscalQuarterPeriod>.Create(from, to);

            // size = 1
            var chunks = PeriodRange<FiscalQuarterPeriod>.Slice(quarterRange, 1).ToArray();
            Assert.AreEqual(20, chunks.Length);
            Assert.AreEqual(from, chunks[0].From);
            Assert.AreEqual(from, chunks[0].To);
            Assert.AreEqual(from.Next(), chunks[1].From);
            Assert.AreEqual(to, chunks[19].From);
            Assert.AreEqual(to, chunks[19].To);
            for (uint i = 0; i < chunks.Length; i++)
            {
                Assert.AreEqual(0, ComparablePeriodUtil.GetGap(chunks[i].From, chunks[i].To));
            }

            // size = 2
            chunks = PeriodRange<FiscalQuarterPeriod>.Slice(quarterRange, 2).ToArray();
            Assert.AreEqual(10, chunks.Length);
            Assert.AreEqual(from, chunks[0].From);
            Assert.AreEqual(from.Next(), chunks[0].To);
            Assert.AreEqual(from.Next().Next(), chunks[1].From);
            Assert.AreEqual(to, chunks[9].To);
            for (uint i = 0; i < chunks.Length; i++)
            {
                Assert.AreEqual(1, ComparablePeriodUtil.GetGap(chunks[i].From, chunks[i].To));
            }

            // size = 3
            chunks = PeriodRange<FiscalQuarterPeriod>.Slice(quarterRange, 3).ToArray();
            Assert.AreEqual(7, chunks.Length);
            Assert.AreEqual(from, chunks[0].From);
            Assert.AreEqual(from.Next().Next(), chunks[0].To);
            Assert.AreEqual(from.Next().Next().Next(), chunks[1].From);
            Assert.AreEqual(to, chunks[6].To);
            for (uint i = 0; i < chunks.Length - 1; i++)
            {
                Assert.AreEqual(2, ComparablePeriodUtil.GetGap(chunks[i].From, chunks[i].To));
            }
            Assert.AreEqual(1, ComparablePeriodUtil.GetGap(chunks.Last().From, chunks.Last().To));
        }

        [TestMethod()]
        public void SliceDayTest()
        {
            var from = DayPeriod.Create(2016, 1, 1);
            var to = DayPeriod.Create(2016, 2, 1);
            var dayRange = PeriodRange<DayPeriod>.Create(from, to);

            // size = 1
            var chunks = PeriodRange<DayPeriod>.Slice(dayRange, 1).ToArray();
            Assert.AreEqual(32, chunks.Length);
            Assert.AreEqual(from, chunks[0].From);
            Assert.AreEqual(from, chunks[0].To);
            Assert.AreEqual(from.Next(), chunks[1].From);
            Assert.AreEqual(to, chunks[31].From);
            Assert.AreEqual(to, chunks[31].To);
            for (uint i = 0; i < chunks.Length; i++)
            {
                Assert.AreEqual(0, ComparablePeriodUtil.GetGap(chunks[i].From, chunks[i].To));
            }

            // size = 2 
            chunks = PeriodRange<DayPeriod>.Slice(dayRange, 2).ToArray();
            Assert.AreEqual(16, chunks.Length);
            Assert.AreEqual(from, chunks[0].From);
            Assert.AreEqual(from.Next(), chunks[0].To);
            Assert.AreEqual(from.Next().Next(), chunks[1].From);
            Assert.AreEqual(to, chunks[15].To);
            for (uint i = 0; i < chunks.Length; i++)
            {
                Assert.AreEqual(1, ComparablePeriodUtil.GetGap(chunks[i].From, chunks[i].To));
            }


            // size = 3
            chunks = PeriodRange<DayPeriod>.Slice(dayRange, 3).ToArray();
            Assert.AreEqual(11, chunks.Length);
            Assert.AreEqual(from, chunks[0].From);
            Assert.AreEqual(from.Next().Next(), chunks[0].To);
            Assert.AreEqual(from.Next().Next().Next(), chunks[1].From);
            Assert.AreEqual(to, chunks[10].To);
            for (uint i = 0; i < chunks.Length - 1; i++)
            {
                Assert.AreEqual(2, ComparablePeriodUtil.GetGap(chunks[i].From, chunks[i].To));
            }
            Assert.AreEqual(1, ComparablePeriodUtil.GetGap(chunks.Last().From, chunks.Last().To));
        }

        [TestMethod()]
        public void IncludesTest()
        {
            var from = FiscalQuarterPeriod.Create(2020, 1);
            var to = FiscalQuarterPeriod.Create(2020, 3);
            var range = PeriodRange<FiscalQuarterPeriod>.Create(from, to);
            Assert.IsTrue(range.Includes(from));
            Assert.IsTrue(range.Includes(FiscalQuarterPeriod.Create(2020, 2)));
            Assert.IsTrue(range.Includes(to));
            Assert.IsFalse(range.Includes(FiscalQuarterPeriod.Create(2020, 4)));
            Assert.IsFalse(range.Includes(FiscalQuarterPeriod.Create(2019, 4)));
        }
    }


}