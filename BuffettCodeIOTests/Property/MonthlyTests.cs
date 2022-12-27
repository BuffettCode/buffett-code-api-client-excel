using BuffettCodeCommon.Period;
using BuffettCodeIO.Property;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace BuffettCodeIOTests.Property
{
    [TestClass]
    public class MonthlyTests
    {
        private static Monthly CreateMonthly(string ticker, uint year, uint month, IDictionary<string, string> properties, IDictionary<string, PropertyDescription> descriptions)
        {
            return Monthly.Create(
                ticker,
                year,
                month,
                properties is null ? PropertyDictionary.Empty() : new PropertyDictionary(properties),
                descriptions is null ? PropertyDescriptionDictionary.Empty() : new PropertyDescriptionDictionary(descriptions)
            );
        }
        [TestMethod()]
        public void GetDescriptionTest()
        {
            var descriptions = new Dictionary<string, PropertyDescription> {
                { "beta.years_2.beta", new PropertyDescription("beta-name", "beta-label", "beta-unit") },
                { "month", new PropertyDescription("month-name", "month-label", "month-unit") }
            };
            var monthly = CreateMonthly("1234", 2022, 8, null, descriptions);

            var betaDescription = monthly.GetDescription("beta.years_2.beta");
            Assert.AreEqual("beta-name", betaDescription.Name);
            Assert.AreEqual("beta-label", betaDescription.JpName);
            Assert.AreEqual("beta-unit", betaDescription.Unit);
            Assert.AreEqual(betaDescription, betaDescription);

            var monthDescription = monthly.GetDescription("month");
            Assert.AreEqual("month-name", monthDescription.Name);
            Assert.AreEqual("month-label", monthDescription.JpName);
            Assert.AreEqual("month-unit", monthDescription.Unit);
        }

        [TestMethod()]
        public void GetValueTest()
        {
            var values = new Dictionary<string, string> {
                { "beta.years_2.beta", "beta-value" },
                { "month","month-value" }
            };
            var monthly = CreateMonthly("1234", 2022, 8, values, null);

            var betaValue = monthly.GetValue("beta.years_2.beta");
            Assert.AreEqual("beta-value", betaValue);

            var monthValue = monthly.GetValue("month");
            Assert.AreEqual("month-value", monthValue);
        }
    }
}