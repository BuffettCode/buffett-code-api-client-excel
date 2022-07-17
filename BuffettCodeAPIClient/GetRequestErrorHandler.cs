using BuffettCodeCommon.Exception;
using System.Collections.Immutable;
using System.Net;
using System.Net.Http;

namespace BuffettCodeAPIClient
{
    enum GetHttpStatusErrorCode
    {
        Forbidden = HttpStatusCode.Forbidden,
        NotFound = HttpStatusCode.NotFound,
        BadRequest = HttpStatusCode.BadRequest,
        TooManyRequests = 429,
    }

    static class TestApiTokenErrorMessage
    {
        const string ExceedingTestingApiTickerLimist = @"{""message"":""Testing Apikey is only allowed to ticker ending with \""01\""""}";
        const string ExceedingTraialApiTickerLimit = "{\"message\":\"Trial request only supports ticker ending '01'\"}";
        const string TestingApiKeyIsNotAllowed = "{\"message\":\"Testing apikey is not allowed\"}";
        public static ImmutableHashSet<string> KnownErrorMessages = ImmutableHashSet.Create<string>(new string[] { ExceedingTestingApiTickerLimist, ExceedingTraialApiTickerLimit, TestingApiKeyIsNotAllowed });
    }

    static class InvalidAPIKeyErrorMessage
    {
        public const string ApiGatewayDefault = "{\"message\":\"Forbidden\"}";

    }


    public class GetRequestErrorHandler
    {
        public static void Handle(ApiGetRequest request, HttpResponseMessage errorResponse)
        {
            switch ((int)errorResponse.StatusCode)
            {
                case (int)GetHttpStatusErrorCode.Forbidden:
                    throw CreateExceptionForForbidden(request, errorResponse);
                case (int)GetHttpStatusErrorCode.NotFound:
                    throw new ResourceNotFoundException($"request={request}");
                case (int)GetHttpStatusErrorCode.TooManyRequests:
                    throw new DailyQuotaException($"request={request}");
                case (int)GetHttpStatusErrorCode.BadRequest:
                    throw CreateExceptionforBadRequest(request,
                        errorResponse);
                default:
                    throw new BuffettCodeApiClientException($"request={request}");
            }
        }

        private static BuffettCodeApiClientException CreateExceptionForForbidden(ApiGetRequest request, HttpResponseMessage errorResponse)
        {
            switch (errorResponse.Content.ReadAsStringAsync().Result)
            {
                case InvalidAPIKeyErrorMessage.ApiGatewayDefault:
                    return new InvalidAPIKeyException($"request={request}");
                default:
                    return new ApiMonthlyLimitExceededException($"request={request}");
            }
        }

        private static BuffettCodeApiClientException CreateExceptionforBadRequest(ApiGetRequest request, HttpResponseMessage errorResponse)
        {
            if (TestApiTokenErrorMessage.KnownErrorMessages.Contains(errorResponse.Content.ReadAsStringAsync().Result))
            {
                return new TestAPIConstraintException($"request={request}");
            }
            else
            {
                return new BuffettCodeApiClientException($"request={request}");

            }

        }
    }
}