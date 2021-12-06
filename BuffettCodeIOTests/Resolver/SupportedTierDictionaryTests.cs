using BuffettCodeCommon.Config;
using BuffettCodeCommon.Period;
using BuffettCodeIO.Property;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace BuffettCodeIO.Resolver.Tests
{
    [TestClass()]
    public class SupportedTierDictionaryTests
    {
        private static readonly string ticker = "1234";
        private static readonly FiscalQuarterPeriod ondemandOldestQuarter = FiscalQuarterPeriod.Create(2010, 1);
        private static readonly FiscalQuarterPeriod ondemandLatestQuarter = FiscalQuarterPeriod.Create(2021, 4);

        private static readonly DayPeriod ondemandOldestDay = DayPeriod.Create(2010, 1, 1);
        private static readonly DayPeriod ondemandLatestDay = DayPeriod.Create(2021, 1, 1);

        private static readonly PeriodRange<FiscalQuarterPeriod> ondemandTierQuarterRange = PeriodRange<FiscalQuarterPeriod>.Create(ondemandOldestQuarter, ondemandLatestQuarter);

        private static readonly PeriodRange<DayPeriod> ondemandTierDayRange = PeriodRange<DayPeriod>.Create(ondemandOldestDay, ondemandLatestDay);

        private static readonly FiscalQuarterPeriod fixedOldestQuarter = FiscalQuarterPeriod.Create(2016, 2);
        private static readonly FiscalQuarterPeriod fixedLatestQuarter = FiscalQuarterPeriod.Create(2021, 3);
        private static readonly DayPeriod fixedTierOldestDay = DayPeriod.Create(2016, 1, 1);
        private static readonly DayPeriod fixedTierLatestDay = DayPeriod.Create(2021, 1, 1);

        private static readonly PeriodRange<FiscalQuarterPeriod> fixedTierQuarterRange = PeriodRange<FiscalQuarterPeriod>.Create(fixedOldestQuarter, fixedLatestQuarter);
        private static readonly PeriodRange<DayPeriod> fixedTierDayRange = PeriodRange<DayPeriod>.Create(fixedTierOldestDay, fixedTierLatestDay);


        [TestMethod()]
        public void AddAndHasTest()
        {
            var dict = new SupportedTierDictionary<FiscalQuarterPeriod>();
            var company = Company.Create(ticker, fixedTierQuarterRange, ondemandTierQuarterRange, fixedTierDayRange, ondemandTierDayRange, PropertyDictionary.Empty(), PropertyDescriptionDictionary.Empty());

            // at first, not contained
            Assert.IsFalse(dict.Has(ticker));
            dict.Add(company.Ticker, company.SupportedQuarterRanges);
            // check added
            Assert.IsTrue(dict.Has(ticker));
        }

        [TestMethod()]
        public void GetQuarterTest()
        {
            var company = Company.Create(ticker, fixedTierQuarterRange, ondemandTierQuarterRange, fixedTierDayRange, ondemandTierDayRange, PropertyDictionary.Empty(), PropertyDescriptionDictionary.Empty());

            var dict = new SupportedTierDictionary<FiscalQuarterPeriod>();
            // throw Key NotFound 
            Assert.ThrowsException<KeyNotFoundException>(() => dict.Get(ticker, fixedLatestQuarter));

            dict.Add(company.Ticker, company.SupportedQuarterRanges);
            // quarter 
            Assert.AreEqual(SupportedTier.FixedTier, dict.Get(ticker, fixedOldestQuarter));
            Assert.AreEqual(SupportedTier.FixedTier, dict.Get(ticker, (FiscalQuarterPeriod)fixedOldestQuarter.Next()));
            Assert.AreEqual(SupportedTier.FixedTier, dict.Get(ticker, fixedLatestQuarter));
            Assert.AreEqual(SupportedTier.OndemandTier, dict.Get(ticker, ondemandOldestQuarter));
            Assert.AreEqual(SupportedTier.OndemandTier, dict.Get(ticker, (FiscalQuarterPeriod)ondemandOldestQuarter.Next()));
            Assert.AreEqual(SupportedTier.OndemandTier, dict.Get(ticker, ondemandLatestQuarter));
            Assert.AreEqual(SupportedTier.None, dict.Get(ticker, (FiscalQuarterPeriod)ondemandLatestQuarter.Next()));
        }

        public void GetDayTest()
        {
            var company = Company.Create(ticker, fixedTierQuarterRange, ondemandTierQuarterRange, fixedTierDayRange, ondemandTierDayRange, PropertyDictionary.Empty(), PropertyDescriptionDictionary.Empty());

            var dict = new SupportedTierDictionary<DayPeriod>();
            // throw Key NotFound 
            Assert.ThrowsException<KeyNotFoundException>(() => dict.Get(ticker, fixedTierLatestDay));

            // day
            Assert.AreEqual(SupportedTier.FixedTier, dict.Get(ticker, ondemandLatestDay));
            Assert.AreEqual(SupportedTier.FixedTier, dict.Get(ticker, fixedTierLatestDay));
            Assert.AreEqual(SupportedTier.FixedTier, dict.Get(ticker, fixedTierOldestDay));
            Assert.AreEqual(SupportedTier.FixedTier, dict.Get(ticker, (DayPeriod)fixedTierOldestDay.Next()));
            Assert.AreEqual(SupportedTier.OndemandTier, dict.Get(ticker, (DayPeriod)fixedTierOldestDay.Prev()));
            Assert.AreEqual(SupportedTier.OndemandTier, dict.Get(ticker, ondemandOldestDay));
            Assert.AreEqual(SupportedTier.OndemandTier, dict.Get(ticker, (DayPeriod)ondemandOldestDay.Next()));
            Assert.AreEqual(SupportedTier.None, dict.Get(ticker, (DayPeriod)ondemandOldestDay.Prev()));
            Assert.AreEqual(SupportedTier.None, dict.Get(ticker, (DayPeriod)fixedTierLatestDay.Next()));
            Assert.AreEqual(SupportedTier.None, dict.Get(ticker, (DayPeriod)ondemandLatestDay.Next()));
        }
    }
}