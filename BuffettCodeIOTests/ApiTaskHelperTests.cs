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
        private static readonly FiscalQuarterPeriod ondemandOldest = FiscalQuarterPeriod.Create(2010, 1);
        private static readonly FiscalQuarterPeriod ondemandLatest = FiscalQuarterPeriod.Create(2021, 4);
        private static readonly PeriodRange<FiscalQuarterPeriod> ondemandTierRange = PeriodRange<FiscalQuarterPeriod>.Create(ondemandOldest, ondemandLatest);
        private static readonly FiscalQuarterPeriod fixedOldest = FiscalQuarterPeriod.Create(2016, 2);
        private static readonly FiscalQuarterPeriod fixedLatest = FiscalQuarterPeriod.Create(2021, 3);
        private static readonly PeriodRange<FiscalQuarterPeriod> fixedTierRange = PeriodRange<FiscalQuarterPeriod>.Create(fixedOldest, fixedLatest);

        private static readonly Company company = Company.Create(ticker, fixedTierRange, ondemandTierRange, PropertyDictionary.Empty(), PropertyDescriptionDictionary.Empty());

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
            Assert.AreEqual(SupportedTier.FixedTier, helper.FindAvailableTier(DataTypeConfig.Quarter, ticker, fixedOldest, true, true, true));
            Assert.AreEqual(SupportedTier.FixedTier, helper.FindAvailableTier(DataTypeConfig.Quarter, ticker, fixedOldest.Next() as FiscalQuarterPeriod, true, true, true));
            Assert.AreEqual(SupportedTier.FixedTier, helper.FindAvailableTier(DataTypeConfig.Quarter, ticker, fixedLatest, true, true, true));
            Assert.AreEqual(SupportedTier.OndemandTier, helper.FindAvailableTier(DataTypeConfig.Quarter, ticker, ondemandOldest, true, true, true));
            Assert.AreEqual(SupportedTier.OndemandTier, helper.FindAvailableTier(DataTypeConfig.Quarter, ticker, ondemandOldest.Next() as FiscalQuarterPeriod, true, true, true));
            Assert.AreEqual(SupportedTier.OndemandTier, helper.FindAvailableTier(DataTypeConfig.Quarter, ticker, ondemandLatest, true, true, true));
            Assert.ThrowsException<NotSupportedTierException>(() => helper.FindAvailableTier(DataTypeConfig.Quarter, ticker, ondemandLatest.Next() as FiscalQuarterPeriod, true, true, true));

            // disabled ondemand endpoint
            Assert.AreEqual(SupportedTier.FixedTier, helper.FindAvailableTier(DataTypeConfig.Quarter, ticker, fixedOldest, false, true, true));
            Assert.AreEqual(SupportedTier.FixedTier, helper.FindAvailableTier(DataTypeConfig.Quarter, ticker, fixedOldest.Next() as FiscalQuarterPeriod, false, true, true));
            Assert.AreEqual(SupportedTier.FixedTier, helper.FindAvailableTier(DataTypeConfig.Quarter, ticker, fixedLatest, false, true, true));
            Assert.ThrowsException<NotSupportedTierException>(() => helper.FindAvailableTier(DataTypeConfig.Quarter, ticker, ondemandOldest, false, true, true));
            Assert.ThrowsException<NotSupportedTierException>(() => helper.FindAvailableTier(DataTypeConfig.Quarter, ticker, ondemandOldest.Next() as FiscalQuarterPeriod, false, true, true));
            Assert.ThrowsException<NotSupportedTierException>(() => helper.FindAvailableTier(DataTypeConfig.Quarter, ticker, ondemandLatest, false, true, true));
            Assert.ThrowsException<NotSupportedTierException>(() => helper.FindAvailableTier(DataTypeConfig.Quarter, ticker, ondemandLatest.Next() as FiscalQuarterPeriod, false, true, true));
        }

        [TestMethod()]
        public void ShouldUseOndemandEndpointTest()
        {
            var tierResolver = CreatTierResolver();
            var helper = new ApiTaskHelper(tierResolver);

            // enable ondemand endpoint
            Assert.IsFalse(helper.ShouldUseOndemandEndpoint(DataTypeConfig.Quarter, ticker, fixedOldest, true, true, true));
            Assert.IsFalse(helper.ShouldUseOndemandEndpoint(DataTypeConfig.Quarter, ticker, fixedOldest.Next() as FiscalQuarterPeriod, true, true, true));
            Assert.IsFalse(helper.ShouldUseOndemandEndpoint(DataTypeConfig.Quarter, ticker, fixedLatest, true, true, true));
            Assert.IsTrue(helper.ShouldUseOndemandEndpoint(DataTypeConfig.Quarter, ticker, ondemandOldest, true, true, true));
            Assert.IsTrue(helper.ShouldUseOndemandEndpoint(DataTypeConfig.Quarter, ticker, ondemandOldest.Next() as FiscalQuarterPeriod, true, true, true));
            Assert.IsTrue(helper.ShouldUseOndemandEndpoint(DataTypeConfig.Quarter, ticker, ondemandLatest, true, true, true));
            Assert.ThrowsException<NotSupportedTierException>(() => helper.ShouldUseOndemandEndpoint(DataTypeConfig.Quarter, ticker, ondemandLatest.Next() as FiscalQuarterPeriod, true, true, true));

            // disabled ondemand endpoint
            Assert.IsFalse(helper.ShouldUseOndemandEndpoint(DataTypeConfig.Quarter, ticker, fixedOldest, false, true, true));
            Assert.IsFalse(helper.ShouldUseOndemandEndpoint(DataTypeConfig.Quarter, ticker, fixedOldest.Next() as FiscalQuarterPeriod, false, true, true));
            Assert.IsFalse(helper.ShouldUseOndemandEndpoint(DataTypeConfig.Quarter, ticker, fixedLatest, false, true, true));
            Assert.ThrowsException<NotSupportedTierException>(() => helper.ShouldUseOndemandEndpoint(DataTypeConfig.Quarter, ticker, ondemandOldest, false, true, true));
            Assert.ThrowsException<NotSupportedTierException>(() => helper.ShouldUseOndemandEndpoint(DataTypeConfig.Quarter, ticker, ondemandOldest.Next() as FiscalQuarterPeriod, false, true, true));
            Assert.ThrowsException<NotSupportedTierException>(() => helper.ShouldUseOndemandEndpoint(DataTypeConfig.Quarter, ticker, ondemandLatest, false, true, true));
            Assert.ThrowsException<NotSupportedTierException>(() => helper.ShouldUseOndemandEndpoint(DataTypeConfig.Quarter, ticker, ondemandLatest.Next() as FiscalQuarterPeriod, false, true, true));

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
            var tierRange = PeriodRange<IComparablePeriod>.Create(fixedOldest, fixedLatest);
            Assert.IsNull(helper.FindEndOfOndemandPeriod(DataTypeConfig.Quarter, ticker, tierRange, true, true, true) as FiscalQuarterPeriod);
            Assert.IsNull(helper.FindEndOfOndemandPeriod(DataTypeConfig.Quarter, ticker, tierRange, false, true, true) as FiscalQuarterPeriod);

            // for ondemand range
            tierRange
                 = PeriodRange<IComparablePeriod>.Create(ondemandOldest, fixedLatest);
            Assert.AreEqual(FiscalQuarterPeriod.Create(2016, 1), helper.FindEndOfOndemandPeriod(DataTypeConfig.Quarter, ticker, tierRange, true, true, true) as FiscalQuarterPeriod);
            Assert.ThrowsException<NotSupportedTierException>(() => helper.FindEndOfOndemandPeriod(DataTypeConfig.Quarter, ticker, tierRange, false, true, true));
        }
    }
}