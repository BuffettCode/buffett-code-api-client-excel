namespace BuffettCodeCommon.Exception
{
    using System;
    public class NotSupportedCSVOutputDestinationException : BaseBuffettCodeException
    {
        public NotSupportedCSVOutputDestinationException() { }
        public NotSupportedCSVOutputDestinationException(string message) : base(message) { }
        public NotSupportedCSVOutputDestinationException(string message, Exception inner) : base(message, inner) { }

    }
}