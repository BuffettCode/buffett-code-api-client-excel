using BuffettCodeCommon.Config;
using BuffettCodeCommon.Exception;
using BuffettCodeCommon.Period;
using BuffettCodeIO.Property;
using BuffettCodeIO.Resolver;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BuffettCodeIO.Tests
{
    [TestClass()]
    public class ApiTaskHelperTests
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

        private static PeriodSupportedTierResolver CreatTierResolver()
        {
            var supportedDict = new SupportedTierDictionary();
            supportedDict.Add(company);
            return new PeriodSupportedTierResolver(null, null, supportedDict);
        }

        [TestMethod()]
        public void FindAvailableTierTest()
        {
            var tierResolver = CreatTierResolver();
            var helper = new ApiTaskHelper(tierResolver);

            // enable ondemand endpoint
            Assert.AreEqual(SupportedTier.FixedTier, helper.FindAvailableTier(DataTypeConfig.Quarter, ticker, fixedOldestQuarter, true, true, true));
            Assert.AreEqual(SupportedTier.FixedTier, helper.FindAvailableTier(DataTypeConfig.Quarter, ticker, fixedOldestQuarter.Next() as FiscalQuarterPeriod, true, true, true));
            Assert.AreEqual(SupportedTier.FixedTier, helper.FindAvailableTier(DataTypeConfig.Quarter, ticker, fixedLatestQuarter, true, true, true));
            Assert.AreEqual(SupportedTier.OndemandTier, helper.FindAvailableTier(DataTypeConfig.Quarter, ticker, ondemandOldestQuarter, true, true, true));
            Assert.AreEqual(SupportedTier.OndemandTier, helper.FindAvailableTier(DataTypeConfig.Quarter, ticker, ondemandOldestQuarter.Next() as FiscalQuarterPeriod, true, true, true));
            Assert.AreEqual(SupportedTier.OndemandTier, helper.FindAvailableTier(DataTypeConfig.Quarter, ticker, ondemandLatestQuarter, true, true, true));
            Assert.ThrowsException<NotSupportedTierException>(() => helper.FindAvailableTier(DataTypeConfig.Quarter, ticker, ondemandLatestQuarter.Next() as FiscalQuarterPeriod, true, true, true));

            // disabled ondemand endpoint
            Assert.AreEqual(SupportedTier.FixedTier, helper.FindAvailableTier(DataTypeConfig.Quarter, ticker, fixedOldestQuarter, false, true, true));
            Assert.AreEqual(SupportedTier.FixedTier, helper.FindAvailableTier(DataTypeConfig.Quarter, ticker, fixedOldestQuarter.Next() as FiscalQuarterPeriod, false, true, true));
            Assert.AreEqual(SupportedTier.FixedTier, helper.FindAvailableTier(DataTypeConfig.Quarter, ticker, fixedLatestQuarter, false, true, true));
            Assert.ThrowsException<NotSupportedTierException>(() => helper.FindAvailableTier(DataTypeConfig.Quarter, ticker, ondemandOldestQuarter, false, true, true));
            Assert.ThrowsException<NotSupportedTierException>(() => helper.FindAvailableTier(DataTypeConfig.Quarter, ticker, ondemandOldestQuarter.Next() as FiscalQuarterPeriod, false, true, true));
            Assert.ThrowsException<NotSupportedTierException>(() => helper.FindAvailableTier(DataTypeConfig.Quarter, ticker, ondemandLatestQuarter, false, true, true));
            Assert.ThrowsException<NotSupportedTierException>(() => helper.FindAvailableTier(DataTypeConfig.Quarter, ticker, ondemandLatestQuarter.Next() as FiscalQuarterPeriod, false, true, true));
        }

        [TestMethod()]
        public void ShouldUseOndemandEndpointTest()
        {
            var tierResolver = CreatTierResolver();
            var helper = new ApiTaskHelper(tierResolver);

            // enable ondemand endpoint
            Assert.IsFalse(helper.ShouldUseOndemandEndpoint(DataTypeConfig.Quarter, ticker, fixedOldestQuarter, true, true, true));
            Assert.IsFalse(helper.ShouldUseOndemandEndpoint(DataTypeConfig.Quarter, ticker, fixedOldestQuarter.Next() as FiscalQuarterPeriod, true, true, true));
            Assert.IsFalse(helper.ShouldUseOndemandEndpoint(DataTypeConfig.Quarter, ticker, fixedLatestQuarter, true, true, true));
            Assert.IsTrue(helper.ShouldUseOndemandEndpoint(DataTypeConfig.Quarter, ticker, ondemandOldestQuarter, true, true, true));
            Assert.IsTrue(helper.ShouldUseOndemandEndpoint(DataTypeConfig.Quarter, ticker, ondemandOldestQuarter.Next() as FiscalQuarterPeriod, true, true, true));
            Assert.IsTrue(helper.ShouldUseOndemandEndpoint(DataTypeConfig.Quarter, ticker, ondemandLatestQuarter, true, true, true));
            Assert.ThrowsException<NotSupportedTierException>(() => helper.ShouldUseOndemandEndpoint(DataTypeConfig.Quarter, ticker, ondemandLatestQuarter.Next() as FiscalQuarterPeriod, true, true, true));

            // disabled ondemand endpoint
            Assert.IsFalse(helper.ShouldUseOndemandEndpoint(DataTypeConfig.Quarter, ticker, fixedOldestQuarter, false, true, true));
            Assert.IsFalse(helper.ShouldUseOndemandEndpoint(DataTypeConfig.Quarter, ticker, fixedOldestQuarter.Next() as FiscalQuarterPeriod, false, true, true));
            Assert.IsFalse(helper.ShouldUseOndemandEndpoint(DataTypeConfig.Quarter, ticker, fixedLatestQuarter, false, true, true));
            Assert.ThrowsException<NotSupportedTierException>(() => helper.ShouldUseOndemandEndpoint(DataTypeConfig.Quarter, ticker, ondemandOldestQuarter, false, true, true));
            Assert.ThrowsException<NotSupportedTierException>(() => helper.ShouldUseOndemandEndpoint(DataTypeConfig.Quarter, ticker, ondemandOldestQuarter.Next() as FiscalQuarterPeriod, false, true, true));
            Assert.ThrowsException<NotSupportedTierException>(() => helper.ShouldUseOndemandEndpoint(DataTypeConfig.Quarter, ticker, ondemandLatestQuarter, false, true, true));
            Assert.ThrowsException<NotSupportedTierException>(() => helper.ShouldUseOndemandEndpoint(DataTypeConfig.Quarter, ticker, ondemandLatestQuarter.Next() as FiscalQuarterPeriod, false, true, true));

            // indicator endpoint
            Assert.IsFalse(helper.ShouldUseOndemandEndpoint(DataTypeConfig.Indicator, ticker, null, false, true, true));
            Assert.IsFalse(helper.ShouldUseOndemandEndpoint(DataTypeConfig.Indicator, ticker, null, true, true, true));
        }

        [TestMethod()]
        public void FindEndOfOndemandPeriodTest()
        {
            var tierResolver = CreatTierResolver();
            var helper = new ApiTaskHelper(tierResolver);

            // for fixed tier range, return null
            var tierRange = PeriodRange<IComparablePeriod>.Create(fixedOldestQuarter, fixedLatestQuarter);
            Assert.IsNull(helper.FindEndOfOndemandPeriod(DataTypeConfig.Quarter, ticker, tierRange, true, true, true) as FiscalQuarterPeriod);
            Assert.IsNull(helper.FindEndOfOndemandPeriod(DataTypeConfig.Quarter, ticker, tierRange, false, true, true) as FiscalQuarterPeriod);

            // for ondemand range
            tierRange
                 = PeriodRange<IComparablePeriod>.Create(ondemandOldestQuarter, fixedLatestQuarter);
            Assert.AreEqual(FiscalQuarterPeriod.Create(2016, 1), helper.FindEndOfOndemandPeriod(DataTypeConfig.Quarter, ticker, tierRange, true, true, true) as FiscalQuarterPeriod);
            Assert.ThrowsException<NotSupportedTierException>(() => helper.FindEndOfOndemandPeriod(DataTypeConfig.Quarter, ticker, tierRange, false, true, true));
        }
    }
}