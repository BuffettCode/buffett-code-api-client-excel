using BuffettCodeAPIClient;
using BuffettCodeCommon.Config;
using BuffettCodeIO.Property;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json.Linq;
using System.IO;

namespace BuffettCodeIO.Parser.Tests
{
    [TestClass()]
    public class ApiV2ResponseParserTests
    {
        private static readonly ApiV2ResponseParser parser = new ApiV2ResponseParser();

        [TestMethod()]
        [DeploymentItem(@"TestData\ApiV2Quarter.json", @"TestData")]
        public void ParseQuarterTest()
        {
            var json = ApiGetResponseBodyParser.Parse(File.ReadAllText(@"TestData\ApiV2Quarter.json"));
            var quarter = (Quarter)parser.Parse(DataTypeConfig.Quarter, json);
            Assert.AreEqual(quarter.Ticker, "2371");
            Assert.AreEqual(quarter.GetDescription("ceo_name").Label, "代表者名");
            Assert.AreEqual(quarter.GetDescription("employee_num").Unit, "人");
            Assert.AreEqual(quarter.GetValue("company_name"), "株式会社カカクコム");
            Assert.AreEqual(quarter.GetValue("current_liabilities"), "7132000000");
        }

        [TestMethod()]
        [DeploymentItem(@"TestData\ApiV2QuarterRange.json", @"TestData")]
        public void ParseQuarterRangeTest()
        {
            var json = ApiGetResponseBodyParser.Parse(File.ReadAllText(@"TestData\ApiV2QuarterRange.json"));
            var quarters = parser.ParseRange(DataTypeConfig.Quarter, json);
            Assert.AreEqual(2, quarters.Count);
            var quarter1 = (Quarter)quarters[0];
            var quarter2 = (Quarter)quarters[1];

            Assert.AreEqual(quarter1.Ticker, "2371");
            Assert.AreEqual(quarter1.GetDescription("ceo_name").Label, "代表者名");
            Assert.AreEqual(quarter1.GetDescription("employee_num").Unit, "人");
            Assert.AreEqual(quarter1.GetValue("company_name"), "株式会社カカクコム");
            Assert.AreEqual(quarter1.GetValue("fiscal_year"), "2018");
            Assert.AreEqual(quarter1.GetValue("fiscal_quarter"), "1");
            Assert.AreEqual(quarter1.GetValue("current_liabilities"), "7132000000");
            Assert.AreEqual(quarter1.GetValue("company_name"), "株式会社カカクコム");
            Assert.AreEqual(quarter2.GetValue("fiscal_year"), "2018");
            Assert.AreEqual(quarter2.GetValue("fiscal_quarter"), "2");

            Assert.AreEqual(quarter2.GetValue("current_liabilities"), "8380000000");
            Assert.AreEqual(quarter2.GetValue("lease_and_guarantee_deposits"), "");

        }

        [TestMethod()]
        [DeploymentItem(@"TestData\ApiV2Indicator.json", @"TestData")]
        public void ParseIndicatorTest()
        {
            var json = ApiGetResponseBodyParser.Parse(File.ReadAllText(@"TestData/ApiV2Indicator.json"));
            var indicator = (Indicator)parser.Parse(DataTypeConfig.Indicator, json);
            Assert.AreEqual(indicator.Ticker, "2371");
            Assert.AreEqual(indicator.GetDescription("eps_actual").Label, "EPS（実績）");
            Assert.AreEqual(indicator.GetDescription("pbr").Unit, "倍");
            Assert.AreEqual(indicator.GetValue("stockprice"), "3450");
            Assert.AreEqual(indicator.GetValue("num_of_shares"), "206003242");
        }

        [TestMethod()]
        [DeploymentItem(@"TestData\ApiV2Company.json", @"TestData")]
        public void ParseCompanyTest()
        {
            var json = ApiGetResponseBodyParser.Parse(File.ReadAllText(@"TestData/ApiV2Company.json"));
            var company = (Company)parser.Parse(DataTypeConfig.Company, json);
            Assert.AreEqual(company.Ticker, "2371");
            Assert.AreEqual(company.GetDescription("tosyo_33category").Label, "東証33業種");
            Assert.AreEqual(company.GetDescription("url").Unit, "");
            Assert.AreEqual(company.GetValue("url"), @"http://corporate.kakaku.com/");
            Assert.AreEqual(company.GetValue("accounting_standard"), "IFRS");
            var supportedQuarterRange = company.SupportedQuarterRanges;
            Assert.AreEqual((uint)2001, supportedQuarterRange.OndemandTierRange.From.Year);
            Assert.AreEqual((uint)4, supportedQuarterRange.OndemandTierRange.From.Quarter);
            Assert.AreEqual((uint)2020, supportedQuarterRange.OndemandTierRange.To.Year);
            Assert.AreEqual((uint)4, supportedQuarterRange.OndemandTierRange.To.Quarter);
            Assert.AreEqual((uint)2016, supportedQuarterRange.FixedTierRange
                .From.Year);
            Assert.AreEqual((uint)1, supportedQuarterRange.FixedTierRange.From.Quarter);
        }

        [TestMethod()]
        [DeploymentItem(@"TestData\ApiV2Company2.json", @"TestData")]
        public void ParseCompany2Test()
        {
            var json = ApiGetResponseBodyParser.Parse(File.ReadAllText(@"TestData/ApiV2Company2.json"));
            var company = (Company)parser.Parse(DataTypeConfig.Company, json);
            Assert.AreEqual(company.Ticker, "2801");
            Assert.AreEqual(company.GetDescription("tosyo_33category").Label, "東証33業種");
            Assert.AreEqual(company.GetDescription("url").Unit, "");
            Assert.AreEqual(company.GetValue("url"), @"http://www.kikkoman.co.jp/");
            Assert.AreEqual(company.GetValue("accounting_standard"), "日本");
            var supportedQuarterRange = company.SupportedQuarterRanges;
            Assert.AreEqual((uint)2004, supportedQuarterRange.OndemandTierRange.From.Year);
            Assert.AreEqual((uint)1, supportedQuarterRange.OndemandTierRange.From.Quarter);
            Assert.AreEqual((uint)2021, supportedQuarterRange.OndemandTierRange.To.Year);
            Assert.AreEqual((uint)1, supportedQuarterRange.OndemandTierRange.To.Quarter);
            Assert.AreEqual((uint)2016, supportedQuarterRange.FixedTierRange
                .From.Year);
            Assert.AreEqual((uint)3, supportedQuarterRange.FixedTierRange.From.Quarter);
        }
        [TestMethod()]
        public void ParseEmpty()
        {
            var json = new JObject();
            Assert.AreEqual(parser.Parse(DataTypeConfig.Quarter, json), EmptyResource.GetInstance());
        }
    }
}