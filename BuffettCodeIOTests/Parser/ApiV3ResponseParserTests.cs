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
            Assert.AreEqual(company.GetDescription("fixed_tier_range.oldest_date").JpName, "定額利用可能な下限の日付");
            Assert.AreEqual(company.GetDescription("fixed_tier_range.oldest_fiscal_year").JpName, "定額利用可能な下限の決算年度");
            Assert.AreEqual(company.GetDescription("fixed_tier_range.oldest_fiscal_quarter").JpName, "定額利用可能な下限の決算四半期");
            Assert.AreEqual(company.GetDescription("fixed_tier_range.latest_fiscal_year").JpName, "定額利用可能な上限の決算年度");
            Assert.AreEqual(company.GetDescription("fixed_tier_range.latest_fiscal_quarter").JpName, "定額利用可能な上限の決算四半期");

            Assert.AreEqual(company.GetValue("url"), @"http://www.kikkoman.co.jp/");
            Assert.AreEqual(company.GetValue("accounting_standard"), "IFRS");
            Assert.AreEqual(company.GetValue("fixed_tier_range.oldest_date"), "2016-11-15");
            Assert.AreEqual(company.GetValue("fixed_tier_range.oldest_fiscal_year"), "2016");
            Assert.AreEqual(company.GetValue("fixed_tier_range.oldest_fiscal_quarter"), "3");
            Assert.AreEqual(company.GetValue("fixed_tier_range.latest_fiscal_year"), "2021");
            Assert.AreEqual(company.GetValue("fixed_tier_range.latest_fiscal_quarter"), "2");

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

        [TestMethod()]
        [DeploymentItem(@"TestData\ApiV3Monthly.json", @"TestData")]
        public void ParseMonthlyTest()
        {
            var json = ApiGetResponseBodyParser.Parse(File.ReadAllText(@"TestData\ApiV3Monthly.json"));
            var monthly = (Monthly)parser.Parse(DataTypeConfig.Monthly, json);
            Assert.AreEqual(monthly.Ticker, "3501");
            Assert.AreEqual(monthly.GetPeriod(), YearMonthPeriod.Create(2022, 5));
            Assert.AreEqual(monthly.GetDescription("ticker").JpName, "ティッカー");
            Assert.AreEqual(monthly.GetDescription("year").JpName, "年");
            Assert.AreEqual(monthly.GetDescription("month").JpName, "月");
            Assert.AreEqual(monthly.GetDescription("beta.years_2.start_date").JpName, "開始日");
            Assert.AreEqual(monthly.GetDescription("beta.years_2.end_date").JpName, "終了日");
            Assert.AreEqual(monthly.GetDescription("beta.years_2.beta").JpName, "β");
            Assert.AreEqual(monthly.GetDescription("beta.years_2.alpha").JpName, "α");
            Assert.AreEqual(monthly.GetDescription("beta.years_2.r").JpName, "相関係数");
            Assert.AreEqual(monthly.GetDescription("beta.years_2.r_squared").JpName, "決定係数");
            Assert.AreEqual(monthly.GetDescription("beta.years_2.count").JpName, "利用データ数");
            Assert.AreEqual(monthly.GetDescription("beta.years_3.start_date").JpName, "開始日");
            Assert.AreEqual(monthly.GetDescription("beta.years_3.end_date").JpName, "終了日");
            Assert.AreEqual(monthly.GetDescription("beta.years_3.beta").JpName, "β");
            Assert.AreEqual(monthly.GetDescription("beta.years_3.alpha").JpName, "α");
            Assert.AreEqual(monthly.GetDescription("beta.years_3.r").JpName, "相関係数");
            Assert.AreEqual(monthly.GetDescription("beta.years_3.r_squared").JpName, "決定係数");
            Assert.AreEqual(monthly.GetDescription("beta.years_3.count").JpName, "利用データ数");
            Assert.AreEqual(monthly.GetDescription("beta.years_5.start_date").JpName, "開始日");
            Assert.AreEqual(monthly.GetDescription("beta.years_5.end_date").JpName, "終了日");
            Assert.AreEqual(monthly.GetDescription("beta.years_5.beta").JpName, "β");
            Assert.AreEqual(monthly.GetDescription("beta.years_5.alpha").JpName, "α");
            Assert.AreEqual(monthly.GetDescription("beta.years_5.r").JpName, "相関係数");
            Assert.AreEqual(monthly.GetDescription("beta.years_5.r_squared").JpName, "決定係数");
            Assert.AreEqual(monthly.GetDescription("beta.years_5.count").JpName, "利用データ数");

            Assert.AreEqual(monthly.GetValue("ticker"), "3501");
            Assert.AreEqual(monthly.GetValue("year"), "2022");
            Assert.AreEqual(monthly.GetValue("month"), "5");
            Assert.AreEqual(monthly.GetValue("beta.years_2.start_date"), "2020-06-01");
            Assert.AreEqual(monthly.GetValue("beta.years_2.end_date"), "2022-05-31");
            Assert.AreEqual(monthly.GetValue("beta.years_2.beta"), "0.27");
            Assert.AreEqual(monthly.GetValue("beta.years_2.alpha"), "0");
            Assert.AreEqual(monthly.GetValue("beta.years_2.r"), "0.11");
            Assert.AreEqual(monthly.GetValue("beta.years_2.r_squared"), "0.01");
            Assert.AreEqual(monthly.GetValue("beta.years_2.count"), "24");
            Assert.AreEqual(monthly.GetValue("beta.years_3.start_date"), "2019-06-01");
            Assert.AreEqual(monthly.GetValue("beta.years_3.end_date"), "2022-05-31");
            Assert.AreEqual(monthly.GetValue("beta.years_3.beta"), "0.84");
            Assert.AreEqual(monthly.GetValue("beta.years_3.alpha"), "-0.01");
            Assert.AreEqual(monthly.GetValue("beta.years_3.r"), "0.41");
            Assert.AreEqual(monthly.GetValue("beta.years_3.r_squared"), "0.17");
            Assert.AreEqual(monthly.GetValue("beta.years_3.count"), "36");
            Assert.AreEqual(monthly.GetValue("beta.years_5.start_date"), "2017-06-01");
            Assert.AreEqual(monthly.GetValue("beta.years_5.end_date"), "2022-05-31");
            Assert.AreEqual(monthly.GetValue("beta.years_5.beta"), "0.89");
            Assert.AreEqual(monthly.GetValue("beta.years_5.alpha"), "0");
            Assert.AreEqual(monthly.GetValue("beta.years_5.r"), "0.45");
            Assert.AreEqual(monthly.GetValue("beta.years_5.r_squared"), "0.2");
            Assert.AreEqual(monthly.GetValue("beta.years_5.count"), "60");

        }

    }

}