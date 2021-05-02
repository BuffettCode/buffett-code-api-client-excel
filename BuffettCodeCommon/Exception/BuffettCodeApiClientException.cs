using System;
namespace BuffettCodeCommon.Exception
{
    /// <summary>
    /// Default Exception on BuffettCode Api Client
    /// </summary>
    public class BuffettCodeApiClientException : System.Exception
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
}