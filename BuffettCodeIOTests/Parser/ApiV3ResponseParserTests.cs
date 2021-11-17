using BuffettCodeAPIClient;
using BuffettCodeCommon.Config;
using BuffettCodeIO.Property;
using Microsoft.VisualStudio.TestTools.UnitTesting;
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
            Assert.AreEqual(company.GetDescription("tosyo_33category").Label, "東証33業種");
            Assert.AreEqual(company.GetDescription("url").Unit, "");
            Assert.AreEqual(company.GetValue("url"), @"http://www.kikkoman.co.jp/");
            Assert.AreEqual(company.GetValue("accounting_standard"), "IFRS");
            var supportedQuarterRange = company.SupportedQuarterRanges;
            Assert.AreEqual((uint)2004, supportedQuarterRange.OndemandTierRange.From.Year);
            Assert.AreEqual((uint)1, supportedQuarterRange.OndemandTierRange.From.Quarter);
            Assert.AreEqual((uint)2021, supportedQuarterRange.OndemandTierRange.To.Year);
            Assert.AreEqual((uint)2, supportedQuarterRange.OndemandTierRange.To.Quarter);
            Assert.AreEqual((uint)2016, supportedQuarterRange.FixedTierRange
                .From.Year);
            Assert.AreEqual((uint)3, supportedQuarterRange.FixedTierRange.From.Quarter);
        }

        [TestMethod()]
        [DeploymentItem(@"TestData\ApiV3Quarter.json", @"TestData")]
        public void ParseQuarterTest()
        {
            var json = ApiGetResponseBodyParser.Parse(File.ReadAllText(@"TestData\ApiV3Quarter.json"));
            var quarter = (Quarter)parser.Parse(DataTypeConfig.Quarter, json);
            Assert.AreEqual(quarter.Ticker, "6501");
            Assert.AreEqual(quarter.GetDescription("ceo_name").Label, "代表者名");
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
            Assert.AreEqual(daily.GetDescription("day").Label, "日付");
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
            Assert.AreEqual(1, quarters.Count);
            Assert.AreEqual(((Quarter)quarters[0]).Ticker, "6501");
            Assert.AreEqual(quarters[0].GetDescription("ceo_name").Label, "代表者名");
            Assert.AreEqual(quarters[0].GetDescription("employee_num").Unit, "人");
            Assert.AreEqual(quarters[0].GetValue("company_name"), "株式会社日立製作所");
        }
    }

}