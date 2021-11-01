using BuffettCodeIO.Property;
using System.Collections.Generic;
using System.Linq;

namespace BuffettCodeIO.TabularOutput

{
    public static class TabularFormatter<T> where T : IApiResource
    {
        public static Tabular<T> Format(IEnumerable<T> apiResources)
        {
            var csvOutput = new Tabular<T>();
            apiResources.ToList().ForEach(r => csvOutput.Add(r));
            return csvOutput;
        }

    }
}