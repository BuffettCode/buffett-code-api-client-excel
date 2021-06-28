namespace BuffettCodeCommon.Exception
{
    using System;
    public class NotSupportedTierException : BaseBuffettCodeException
    {
        public NotSupportedTierException() : base() { }

        public NotSupportedTierException(string message) : base(message) { }

        public NotSupportedTierException(string message, Exception inner) : base(message, inner) { }
    }
}