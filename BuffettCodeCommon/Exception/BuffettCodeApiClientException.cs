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

    public class QuotaException : BuffettCodeApiClientException
    {
        public QuotaException() : base() { }

        public QuotaException(string message) : base(message) { }

        public QuotaException(string message, Exception inner) : base(message, inner) { }

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
    public class UnsupportedTypeException : BuffettCodeApiClientException
    {
    }
}