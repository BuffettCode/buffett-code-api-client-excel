namespace BuffettCodeCommon.Exception
{
    using System;
    public class PropertyNotFoundException : BaseBuffettCodeException
    {
        public PropertyNotFoundException() : base()
        {
        }


        public PropertyNotFoundException(string message) : base(message)
        {
        }

        public PropertyNotFoundException(string message, Exception inner) : base(message, inner)
        {

        }

    }
}