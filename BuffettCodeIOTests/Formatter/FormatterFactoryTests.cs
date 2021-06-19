using BuffettCodeIO.Property;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BuffettCodeIO.Formatter.Tests
{
    [TestClass()]
    public class FormatterFactoryTests
    {
        [TestMethod]
        public void TestCreate()
        {
            IPropertyFormatter formatter;

            // 通貨
            formatter = CreateFormatter("dividend", "配当金", "円");
            Assert.IsTrue(formatter is CurrencyFormatter);
            formatter = CreateFormatter("assets", "総資産", "百万円");
            Assert.IsTrue(formatter is CurrencyFormatter);

            // 数値
            formatter = CreateFormatter("issued_share_num", "発行済株式総数", "株");
            Assert.IsTrue(formatter is NumericFormatter);
            formatter = CreateFormatter("pbr", "PBR", "倍");
            Assert.IsTrue(formatter is NumericFormatter);
            formatter = CreateFormatter("inventory_turnover", "棚卸資産回転期間", "日");
            Assert.IsTrue(formatter is NumericFormatter);
            formatter = CreateFormatter("debt_monthly_sales_ratio", "有利子負債/月商比率", "ヶ月");
            Assert.IsTrue(formatter is NumericFormatter);
            formatter = CreateFormatter("employee_num", "従業員数", "人");
            Assert.IsTrue(formatter is NumericFormatter);

            // 割合
            formatter = CreateFormatter("tangible_fixed_assets_turnover", "有形固定資産回転率", "%");
            Assert.IsTrue(formatter is RatioFormatter);

            // その他
            formatter = CreateFormatter("company_name", "社名", "なし");
            Assert.IsTrue(formatter is InactionFormatter);

            // プロパティの定義を渡さなかったら常にInactionFormatter
            formatter = PropertyFormatterFactory.Create(null);
            Assert.IsTrue(formatter is InactionFormatter);
        }

        private IPropertyFormatter CreateFormatter(string name, string label, string unit)
        {
            PropertyDescription description = new PropertyDescription(name, label, unit);
            return PropertyFormatterFactory.Create(description);
        }


    }
}