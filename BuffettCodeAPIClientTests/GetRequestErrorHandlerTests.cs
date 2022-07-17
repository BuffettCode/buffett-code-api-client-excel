using BuffettCodeCommon.Exception;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;

namespace BuffettCodeAPIClient.Tests
{
    [TestClass()]
    public class GetRequestErrorHandlerTests
    {

        private readonly ApiGetRequest Request = new ApiGetRequest("dummy", new Dictionary<string, string>());

        private static HttpResponseMessage CreateResponseMessage(HttpStatusCode statusCode, string content)
        {
            var response = new HttpResponseMessage(statusCode);
            response.Content = new StringContent(content);
            return response;
        }

        [TestMethod()]
        public void HandleInvalidApiKeyError()
        {
            var response = CreateResponseMessage(HttpStatusCode.Forbidden, "{\"message\":\"Forbidden\"}");
            Assert.ThrowsException<InvalidAPIKeyException>(() => GetRequestErrorHandler.Handle(Request, response));
        }

        [TestMethod()]
        public void HandleInvalidApiMonthlyLimitExceededException()
        {
            var response = CreateResponseMessage(HttpStatusCode.Forbidden, "dummy");
            Assert.ThrowsException<ApiMonthlyLimitExceededException>(() => GetRequestErrorHandler.Handle(Request, response));
        }

        [TestMethod()]
        public void HandleResourceNotFound()
        {
            var response = CreateResponseMessage(HttpStatusCode.NotFound, "");
            Assert.ThrowsException<ResourceNotFoundException>(() => GetRequestErrorHandler.Handle(Request, response));
        }


        [TestMethod()]
        public void HandleTestAPIConstraintException()
        {
            var response = CreateResponseMessage(HttpStatusCode.BadRequest, "{\"message\":\"Testing apikey is not allowed\"}");
            Assert.ThrowsException<TestAPIConstraintException>(() => GetRequestErrorHandler.Handle(Request, response));
        }

        [TestMethod()]
        public void HandleTestBadRequest()
        {
            var response = CreateResponseMessage(HttpStatusCode.BadRequest, "unknown error");
            Assert.ThrowsException<BuffettCodeApiClientException>(() => GetRequestErrorHandler.Handle(Request, response));
        }

        [TestMethod()]
        public void HandleTestDefaultException()
        {
            var response = CreateResponseMessage(HttpStatusCode.InternalServerError, "unknown error");
            Assert.ThrowsException<BuffettCodeApiClientException>(() => GetRequestErrorHandler.Handle(Request, response));
        }



    }
}