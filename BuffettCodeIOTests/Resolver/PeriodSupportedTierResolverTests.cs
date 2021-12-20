using BuffettCodeCommon.Config;
using BuffettCodeCommon.Period;
using BuffettCodeIO.Property;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BuffettCodeIO.Resolver.Tests
{
    [TestClass()]
    public class PeriodSupportedTierResolverTests
    {
        private static readonly string ticker = "1234";
        private static readonly FiscalQuarterPeriod ondemandOldestQuarter = FiscalQuarterPeriod.Create(2010, 1);
        private static readonly FiscalQuarterPeriod ondemandLatestQuarter = FiscalQuarterPeriod.Create(2021, 3);

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


        private static readonly Company company = Company.Create(ticker, fixedTierQuarterRange, ondemandTierQuarterRange, fixedTierDayRange, ondemandTierDayRange, PropertyDictionary.Empty(), PropertyDescriptionDictionary.Empty());


        [TestMethod()]
        public void ResolveTest()
        {
            var quarterDict = new SupportedTierDictionary<FiscalQuarterPeriod>();
            var dayDict = new SupportedTierDictionary<DayPeriod>();

            quarterDict.Add(company.Ticker, company.SupportedQuarterRanges);
            dayDict.Add(company.Ticker, company.SupportedDailyRanges);
            var resolver = new PeriodSupportedTierResolver(null, null, quarterDict, dayDict);

            // snapshot
            Assert.AreEqual(SupportedTier.FixedTier, resolver.Resolve(ticker, Snapshot.GetInstance(), false, false));

            // relative quarter periods
            Assert.AreEqual(SupportedTier.FixedTier, resolver.Resolve(ticker, RelativeFiscalQuarterPeriod.CreateLatest(), false, false));
            Assert.AreEqual(SupportedTier.FixedTier, resolver.Resolve(ticker, RelativeFiscalQuarterPeriod.Create(5, 1), false, false));
            Assert.AreEqual(SupportedTier.OndemandTier, resolver.Resolve(ticker, RelativeFiscalQuarterPeriod.Create(5, 2), false, false));
            Assert.AreEqual(SupportedTier.OndemandTier, resolver.Resolve(ticker, RelativeFiscalQuarterPeriod.Create(5, 3), false, false));
            Assert.AreEqual(SupportedTier.OndemandTier, resolver.Resolve(ticker, RelativeFiscalQuarterPeriod.Create(11, 2), false, false));
            Assert.AreEqual(SupportedTier.None, resolver.Resolve(ticker, RelativeFiscalQuarterPeriod.Create(11, 3), false, false));
            Assert.AreEqual(SupportedTier.None, resolver.Resolve(ticker, RelativeFiscalQuarterPeriod.Create(11, 4), false, false));

            // latest day
            Assert.AreEqual(SupportedTier.FixedTier, resolver.Resolve(ticker, LatestDayPeriod.GetInstance(), false, false));


            // quarter
            Assert.AreEqual(SupportedTier.FixedTier, resolver.Resolve(ticker, fixedOldestQuarter, false, false));
            Assert.AreEqual(SupportedTier.FixedTier, resolver.Resolve(ticker, fixedOldestQuarter.Next() as FiscalQuarterPeriod, false, false));
            Assert.AreEqual(SupportedTier.FixedTier, resolver.Resolve(ticker, fixedLatestQuarter, false, false));
            Assert.AreEqual(SupportedTier.OndemandTier, resolver.Resolve(ticker, ondemandOldestQuarter, false, false));
            Assert.AreEqual(SupportedTier.OndemandTier, resolver.Resolve(ticker, ondemandOldestQuarter.Next() as FiscalQuarterPeriod, false, false));
            Assert.AreEqual(SupportedTier.FixedTier, resolver.Resolve(ticker, ondemandLatestQuarter, false, false));
            Assert.AreEqual(SupportedTier.None, resolver.Resolve(ticker, ondemandLatestQuarter.Next() as FiscalQuarterPeriod, false, false));

            // day
            Assert.AreEqual(SupportedTier.FixedTier, resolver.Resolve(ticker, ondemandLatestDay, false, false));
            Assert.AreEqual(SupportedTier.FixedTier, resolver.Resolve(ticker, fixedTierLatestDay, false, false));
            Assert.AreEqual(SupportedTier.FixedTier, resolver.Resolve(ticker, fixedTierOldestDay, false, false));
            Assert.AreEqual(SupportedTier.FixedTier, resolver.Resolve(ticker, fixedTierOldestDay.Next(), false, false));
            Assert.AreEqual(SupportedTier.OndemandTier, resolver.Resolve(ticker, fixedTierOldestDay.Prev(), false, false));
            Assert.AreEqual(SupportedTier.OndemandTier, resolver.Resolve(ticker, ondemandOldestDay, false, false));
            Assert.AreEqual(SupportedTier.OndemandTier, resolver.Resolve(ticker, ondemandOldestDay.Next(), false, false));
            Assert.AreEqual(SupportedTier.None, resolver.Resolve(ticker, ondemandOldestDay.Prev(), false, false));
            Assert.AreEqual(SupportedTier.None, resolver.Resolve(ticker, fixedTierLatestDay.Next(), false, false));
            Assert.AreEqual(SupportedTier.None, resolver.Resolve(ticker, ondemandLatestDay.Next(), false, false));
        }

    }
}