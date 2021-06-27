using BuffettCodeAPIClient;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;

namespace BuffettCodeIO.Parser.Tests
{
    [TestClass()]
    public class ApiV2ResponseParserTests
    {

        [TestMethod()]
        [DeploymentItem(@"TestData\ApiV2Quarter.json", @"TestData")]
        public void ParseQuarterTest()
        {
            var json = ApiGetResponseBodyParser.Parse(File.ReadAllText(@"TestData\ApiV2Quarter.json"));
            var quarter = ApiV2ResponseParser.ParseQuarter(json);
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
            var quarters = ApiV2ResponseParser.ParseQuarterRange(json);
            Assert.AreEqual(2, quarters.Count);
            var quarter1 = quarters[0];
            var quarter2 = quarters[1];

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
            var indicator = ApiV2ResponseParser.ParseIndicator(json);
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
            var company = ApiV2ResponseParser.ParseCompany(json);
            Assert.AreEqual(company.Ticker, "2371");
            Assert.AreEqual(company.GetDescription("tosyo_33category").Label, "東証33業種");
            Assert.AreEqual(company.GetDescription("url").Unit, "");
            Assert.AreEqual(company.GetValue("url"), @"http://corporate.kakaku.com/");
            Assert.AreEqual(company.GetValue("accounting_standard"), "IFRS");
            Assert.AreEqual((uint)2001, company.OndemandTireRange.From.Year);
            Assert.AreEqual((uint)4, company.OndemandTireRange.From.Quarter);
            Assert.AreEqual((uint)2020, company.OndemandTireRange.To.Year);
            Assert.AreEqual((uint)4, company.OndemandTireRange.To.Quarter);
            Assert.AreEqual((uint)2016, company.FlatTierRange.From.Year);
            Assert.AreEqual((uint)1, company.FlatTierRange.From.Quarter);
        }



    }
}