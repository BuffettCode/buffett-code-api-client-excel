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
            var baseUrl = "https://example.com";
            var clientCore = ApiClientCore.Create(apiKey, baseUrl);

            // to test private method
            var privateObj = new PrivateObject(clientCore).Invoke("NewHttpClient");

            Assert.IsInstanceOfType(privateObj, typeof(HttpClient));
            HttpClient httpClient = (HttpClient)privateObj;
            var xApiKeyHeaders = new List<string>(httpClient.DefaultRequestHeaders.GetValues("x-api-key"));
            Assert.AreEqual(new Uri(baseUrl), httpClient.BaseAddress);
            Assert.AreEqual(1, xApiKeyHeaders.Count);
            Assert.AreEqual(apiKey, xApiKeyHeaders[0]);
        }
    }
}