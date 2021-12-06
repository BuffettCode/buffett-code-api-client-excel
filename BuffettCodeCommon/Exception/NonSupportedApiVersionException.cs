namespace BuffettCodeCommon.Exception
{
    using System;
    public class NonSupportedApiVersionException : BaseBuffettCodeException
    {
        public NonSupportedApiVersionException() { }
        public NonSupportedApiVersionException(string message) : base(message) { }
        public NonSupportedApiVersionException(string message, Exception inner) : base(message, inner) { }
    }
}