namespace BuffettCodeCommon.Exception
{
    using System;
    public class AggregationNotFoundException : BaseBuffettCodeException
    {

        public AggregationNotFoundException() : base()
        {
        }


        public AggregationNotFoundException(string message) : base(message)
        {
        }

        public AggregationNotFoundException(string message, Exception inner) : base(message, inner)
        {

        }
    }
}