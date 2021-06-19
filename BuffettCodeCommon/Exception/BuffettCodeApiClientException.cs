namespace BuffettCodeCommon.Exception
{
    public class BuffettCodeApiClientException : BaseBuffettCodeException
    {
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