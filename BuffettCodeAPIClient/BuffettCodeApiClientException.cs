using System;
namespace BuffettCodeAPIClient
{
    /// <summary>
    /// Default Exception on BuffettCode Api Client
    /// </summary>
    public class BuffettCodeApiClientException : Exception
    {
    }

    public class InvalidAPIKeyException : BuffettCodeApiClientException
    {
    }

    public class QuotaException : BuffettCodeApiClientException
    {
    }


}