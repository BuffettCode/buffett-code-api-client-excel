namespace BuffettCodeCommon.Exception
{
    using System;
    public class ApiResponseParserException : BaseBuffettCodeException
    {
        public ApiResponseParserException() : base()
        {
        }


        public ApiResponseParserException(string message) : base(message)
        {
        }

        public ApiResponseParserException(string message, Exception inner) : base(message, inner)
        {

        }
    }
}