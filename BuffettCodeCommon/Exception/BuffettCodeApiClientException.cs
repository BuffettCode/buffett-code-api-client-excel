namespace BuffettCodeCommon.Exception
{
    public class BuffettCodeApiClientException : BaseBuffettCodeException
    {
        public BuffettCodeApiClientException() : base() { }

        public BuffettCodeApiClientException(string message) : base(message) { }

    }


    public class InvalidAPIKeyException : BuffettCodeApiClientException
    {
    }

    public class QuotaException : BuffettCodeApiClientException
    {
    }

    public class TestAPIConstraintException : BuffettCodeApiClientException
    {
    }

    public class UnsupportedTypeException : BuffettCodeApiClientException
    {
    }
}