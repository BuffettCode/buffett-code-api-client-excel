namespace BuffettCodeCommon.Exception
{
    using System;
    public class AddinConfigurationException : BaseBuffettCodeException
    {
        public AddinConfigurationException()
        {
        }

        public AddinConfigurationException(string message)
             : base(message)
        {
        }

        public AddinConfigurationException(string message, Exception inner)
            : base(message, inner)
        {
        }

    }
}