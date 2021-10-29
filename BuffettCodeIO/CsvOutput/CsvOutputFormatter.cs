using BuffettCodeIO.Property;
using System.Collections.Generic;
using System.Linq;

namespace BuffettCodeIO.CsvOutput
{
    public static class CsvOutputFormatter<T> where T : IApiResource
    {
        public static CsvOutput<T> Format(IEnumerable<T> apiResources)
        {
            var csvOutput = new CsvOutput<T>();
            apiResources.ToList().ForEach(r => csvOutput.Add(r));
            return csvOutput;
        }

    }
}