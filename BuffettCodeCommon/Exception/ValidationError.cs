namespace BuffettCodeCommon.Exception
{
    using System;

    /// <summary>
    /// Exception class for Validation Error.
    /// </summary>
    public class ValidationError : BaseBuffettCodeException
    {
        public ValidationError()
        {
        }

        public ValidationError(string message)
            : base(message)
        {
        }

        public ValidationError(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}