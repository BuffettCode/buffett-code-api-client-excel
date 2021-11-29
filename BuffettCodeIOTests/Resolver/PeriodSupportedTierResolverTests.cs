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
 

        private static readonly Company company = Company.Create(ticker, fixedTierQuarterRange, ondemandTierQuarterRange, fixedTierDayRange, ondemandTierDayRange, PropertyDictionary.Empty(), PropertyDescriptionDictionary.Empty());


        [TestMethod()]
        public void ResolveQuarterTest()
        {
            var supportedDict = new SupportedTierDictionary();
            supportedDict.Add(company);
            var resolver = new PeriodSupportedTierResolver(null, null, supportedDict);

            Assert.AreEqual(SupportedTier.FixedTier, resolver.Resolve(DataTypeConfig.Quarter, ticker, fixedOldestQuarter, false, false));
            Assert.AreEqual(SupportedTier.FixedTier, resolver.Resolve(DataTypeConfig.Quarter, ticker, fixedOldestQuarter.Next() as FiscalQuarterPeriod, false, false));
            Assert.AreEqual(SupportedTier.FixedTier, resolver.Resolve(DataTypeConfig.Quarter, ticker, fixedLatestQuarter, false, false));
            Assert.AreEqual(SupportedTier.OndemandTier, resolver.Resolve(DataTypeConfig.Quarter, ticker, ondemandOldestQuarter, false, false));
            Assert.AreEqual(SupportedTier.OndemandTier, resolver.Resolve(DataTypeConfig.Quarter, ticker, ondemandOldestQuarter.Next() as FiscalQuarterPeriod, false, false));
            Assert.AreEqual(SupportedTier.OndemandTier, resolver.Resolve(DataTypeConfig.Quarter, ticker, ondemandLatestQuarter, false, false));
            Assert.AreEqual(SupportedTier.None, resolver.Resolve(DataTypeConfig.Quarter, ticker, ondemandLatestQuarter.Next() as FiscalQuarterPeriod, false, false));
        }

    }
}