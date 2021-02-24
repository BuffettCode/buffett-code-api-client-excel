using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
