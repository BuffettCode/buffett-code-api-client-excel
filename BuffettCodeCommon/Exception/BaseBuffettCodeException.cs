namespace BuffettCodeCommon.Exception
{
    using System;
    public abstract class BaseBuffettCodeException : Exception
    {
        public BaseBuffettCodeException() : base()
        {
        }


        public BaseBuffettCodeException(string message) : base(message)
        {
        }

        public BaseBuffettCodeException(string message, Exception inner) : base(message, inner)
        {

        }


    }
}