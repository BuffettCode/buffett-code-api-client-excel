namespace BuffettCodeCommon.Exception
{
    using System;
    public class NotSupportedDataTypeException : BaseBuffettCodeException
    {
        public NotSupportedDataTypeException() : base()
        {
        }

        public NotSupportedDataTypeException(string message) : base(message)
        {
        }

        public NotSupportedDataTypeException(string message, Exception inner) : base(message, inner)
        {

        }
    }
}