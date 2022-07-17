namespace BuffettCodeCommon.Exception
{
    using System;
    public class BuffettCodeApiClientException : BaseBuffettCodeException
    {
        public BuffettCodeApiClientException() : base() { }

        public BuffettCodeApiClientException(string message) : base(message) { }

        public BuffettCodeApiClientException(string message, Exception inner) : base(message, inner) { }
    }


    public class InvalidAPIKeyException : BuffettCodeApiClientException
    {
        public InvalidAPIKeyException() : base() { }

        public InvalidAPIKeyException(string message) : base(message) { }

        public InvalidAPIKeyException(string message, Exception inner) : base(message, inner) { }
    }

    public class DailyQuotaException : BuffettCodeApiClientException
    {
        public DailyQuotaException() : base() { }

        public DailyQuotaException(string message) : base(message) { }

        public DailyQuotaException(string message, Exception inner) : base(message, inner) { }

    }

    public class TestAPIConstraintException : BuffettCodeApiClientException
    {
        public TestAPIConstraintException() : base() { }

        public TestAPIConstraintException(string message) : base(message) { }

        public TestAPIConstraintException(string message, Exception inner) : base(message, inner) { }

    }

    public class ResourceNotFoundException : BuffettCodeApiClientException
    {
        public ResourceNotFoundException() : base() { }

        public ResourceNotFoundException(string message) : base(message) { }

        public ResourceNotFoundException(string message, Exception inner) : base(message, inner) { }

    }

    public class ApiMonthlyLimitExceededException : BuffettCodeApiClientException
    {
        public ApiMonthlyLimitExceededException() : base() { }

        public ApiMonthlyLimitExceededException(string message) : base(message) { }

        public ApiMonthlyLimitExceededException(string message, Exception inner) : base(message, inner) { }

    }

}