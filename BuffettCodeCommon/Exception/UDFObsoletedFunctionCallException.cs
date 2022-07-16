namespace BuffettCodeCommon.Exception
{
    using System;

    public class UDFObsoletedFunctionCallException : BaseBuffettCodeException
    {
        public UDFObsoletedFunctionCallException() : base()
        {
        }

        public UDFObsoletedFunctionCallException(string message) : base(message) { }


        public UDFObsoletedFunctionCallException(string message, Exception inner) : base(message, inner) { }


    }
}