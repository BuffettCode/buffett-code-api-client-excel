using System.Collections.Generic;

namespace BuffettCodeIO
{
    public class IPropertyAggregationComparer : IEqualityComparer<IPropertyAggregation>
    {
        public bool Equals(IPropertyAggregation left, IPropertyAggregation right)
        {
            return left.GetIdentifier().Equals(right.GetIdentifier());
        }

        public int GetHashCode(IPropertyAggregation aggregation)
        {
            return aggregation.GetIdentifier().GetHashCode();
        }
    }
}