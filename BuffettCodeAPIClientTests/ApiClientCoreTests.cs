namespace BuffettCodeAPIClient.Tests
{
    using BuffettCodeAPIClient;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System;
    using System.Collections.Generic;
    using System.Net.Http;

    /// <summary>
    /// Defines the <see cref="ApiClientCoreTests" />.
    /// </summary>
    [TestClass()]
    public class ApiClientCoreTests
    {
        /// <summary>
        /// The newHttpClientTest.
        /// </summary>
        [TestMethod()]
        public void NewHttpClientTest()
        {
            var apiKey = "dummy_api_key";
            var baseUri = new Uri(@"https://example.com");
            var clentCore
                = new ApiClientCore(apiKey, baseUri);
            // to test private method
            var privateObj = new PrivateObject(clentCore).Invoke("NewHttpClient");

            Assert.IsInstanceOfType(privateObj, typeof(HttpClient));
            HttpClient httpClient = (HttpClient)privateObj;
            var xApiKeyHeaders = new List<string>(httpClient.DefaultRequestHeaders.GetValues("x-api-key"));
            Assert.AreEqual(baseUri, httpClient.BaseAddress);
            Assert.AreEqual(1, xApiKeyHeaders.Count);
            Assert.AreEqual(apiKey, xApiKeyHeaders[0]);
        }

        [TestMethod()]
        public void SetGetApiKeyTest()
        {
            var initKey = "init_api_key";
            var newKey = "new_api_key";
            var baseUri = new Uri(@"https://example.com");
            var clientCore
                = new ApiClientCore(initKey, baseUri);
            Assert.AreEqual(initKey, clientCore.GetApiKey());
            Assert.AreEqual(newKey, clientCore.SetApiKey(newKey).GetApiKey());
        }
    }
}