using BuffettCodeAPIClient;
using BuffettCodeCommon.Config;
using BuffettCodeCommon.Period;
using BuffettCodeIO.Property;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;


namespace BuffettCodeIO.Parser.Tests
{
    [TestClass()]
    public class ApiV3ResponseParserTests
    {

        private static readonly ApiV3ResponseParser parser = new ApiV3ResponseParser();

        [TestMethod()]
        [DeploymentItem(@"TestData\ApiV3Company.json", @"TestData")]
        public void ParseCompanyTest()
        {
            var json = ApiGetResponseBodyParser.Parse(File.ReadAllText(@"TestData/ApiV3Company.json"));
            var company = (Company)parser.Parse(DataTypeConfig.Company, json);
            Assert.AreEqual(company.Ticker, "2801");
            Assert.AreEqual(company.GetDescription("tosyo_33category").JpName, "東証33業種");
            Assert.AreEqual(company.GetDescription("url").Unit, "");
            Assert.AreEqual(company.GetValue("url"), @"http://www.kikkoman.co.jp/");
            Assert.AreEqual(company.GetValue("accounting_standard"), "IFRS");
            var supportedQuarterRanges = company.SupportedQuarterRanges;

            Assert.AreEqual((uint)2004, supportedQuarterRanges.OndemandTierRange.From.Year);
            Assert.AreEqual((uint)1, supportedQuarterRanges.OndemandTierRange.From.Quarter);
            Assert.AreEqual((uint)2021, supportedQuarterRanges.OndemandTierRange.To.Year);
            Assert.AreEqual((uint)2, supportedQuarterRanges.OndemandTierRange.To.Quarter);
            Assert.AreEqual((uint)2016, supportedQuarterRanges.FixedTierRange
                .From.Year);
            Assert.AreEqual((uint)3, supportedQuarterRanges.FixedTierRange.From.Quarter);

            var supportedDailyRanges = company.SupportedDailyRanges;
            var today = DayPeriod.Create(DateTime.Today);
            Assert.AreEqual(DayPeriod.Create(2016, 11, 15), supportedDailyRanges.FixedTierRange.From);
            Assert.AreEqual(today, supportedDailyRanges.FixedTierRange.To);

            Assert.AreEqual(DayPeriod.Create(2000, 4, 3), supportedDailyRanges.OndemandTierRange.From);
            Assert.AreEqual(today, supportedDailyRanges.FixedTierRange.To);
        }

        [TestMethod()]
        [DeploymentItem(@"TestData\ApiV3Quarter.json", @"TestData")]
        public void ParseQuarterTest()
        {
            var json = ApiGetResponseBodyParser.Parse(File.ReadAllText(@"TestData\ApiV3Quarter.json"));
            var quarter = (Quarter)parser.Parse(DataTypeConfig.Quarter, json);
            Assert.AreEqual(quarter.Ticker, "6501");
            Assert.AreEqual(quarter.GetDescription("ceo_name").JpName, "代表者名");
            Assert.AreEqual(quarter.GetDescription("employee_num").Unit, "人");
            Assert.AreEqual(quarter.GetValue("company_name"), "株式会社日立製作所");
            Assert.AreEqual(quarter.GetValue("current_liabilities"), "4596930000000");
        }
        [TestMethod()]
        [DeploymentItem(@"TestData\ApiV3Daily.json", @"TestData")]
        public void ParseDailyTest()
        {
            var json = ApiGetResponseBodyParser.Parse(File.ReadAllText(@"TestData\ApiV3Daily.json"));
            var daily = (Daily)parser.Parse(DataTypeConfig.Daily, json);
            Assert.AreEqual(daily.GetDescription("day").JpName, "日付");
            Assert.AreEqual(daily.GetDescription("trading_volume").Unit, "株");
            Assert.AreEqual(daily.GetValue("market_capital"), "4464984517983");
            Assert.AreEqual(daily.GetValue("cash_market_capital_ratio"), "15.55");
        }

        [TestMethod()]
        [DeploymentItem(@"TestData\ApiV3BulkQuarter.json", @"TestData")]
        public void ParseBulkQuarter()
        {
            var json = ApiGetResponseBodyParser.Parse(File.ReadAllText(@"TestData\ApiV3BulkQuarter.json"));
            var quarters = parser.ParseRange(DataTypeConfig.Quarter, json);
            Assert.AreEqual(4, quarters.Count);
            // check ticker, fy, fq
            Assert.AreEqual("6501", ((Quarter)quarters[0]).Ticker);
            Assert.AreEqual(FiscalQuarterPeriod.Create(2020, 1), quarters[0].GetPeriod());
            Assert.AreEqual("6501", ((Quarter)quarters[1]).Ticker);
            Assert.AreEqual(FiscalQuarterPeriod.Create(2020, 2), quarters[1].GetPeriod());
            Assert.AreEqual("6501", ((Quarter)quarters[2]).Ticker);
            Assert.AreEqual(FiscalQuarterPeriod.Create(2020, 3), quarters[2].GetPeriod());
            Assert.AreEqual("6501", ((Quarter)quarters[3]).Ticker);
            Assert.AreEqual(FiscalQuarterPeriod.Create(2020, 4), quarters[3].GetPeriod());

            // check value
            Assert.AreEqual(quarters[0].GetDescription("ceo_name").JpName, "代表者名");
            Assert.AreEqual(quarters[0].GetDescription("employee_num").Unit, "人");
            Assert.AreEqual(quarters[0].GetValue("company_name"), "株式会社日立製作所");
        }
    }

}