using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace BuffettCodeAPIClient.Tests
{
    [TestClass()]
    public class CacheKeyCreatorTests
    {
        [TestMethod()]
        public void CreateTest()
        {
            var endPoint = "dummy";
            var parameters = new Dictionary<string, string> { { "key", "value" }, { "キー", "バリュー" } };
            var request = new ApiGetRequest(endPoint, parameters);
            Assert.AreEqual("dummy?%e3%82%ad%e3%83%bc=%e3%83%90%e3%83%aa%e3%83%a5%e3%83%bc&key=value", CacheKeyCreator.Create(request));
        }
    }
}