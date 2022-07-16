namespace BuffettCodeCommon.Exception
{
    using System;

    public class UDFUnsupportedSyntaxException : BaseBuffettCodeException
    {
        public UDFUnsupportedSyntaxException() : base()
        {
        }

        public UDFUnsupportedSyntaxException(string message) : base(message) { }


        public UDFUnsupportedSyntaxException(string message, Exception inner) : base(message, inner) { }


    }
}