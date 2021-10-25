using System.Collections.Generic;
using System.Linq;

namespace BuffettCodeIO.Property
{
    public class CsvOutput<T> where T : IApiResource
    {
        private readonly CsvOutputRow header = CsvOutputRow.Create("キー", "項目名", "単位");
        private readonly IDictionary<string, CsvOutputRow> rowDict = new Dictionary<string, CsvOutputRow>();
        private readonly List<string> keys = new List<string>();


        public CsvOutput<T> Add(T apiResource)
        {
            // append header column using period
            header.Add(apiResource.GetPeriod().ToString());
            // setup keys and values if empty
            if (keys.Count == 0)
            {
                apiResource.GetPropertyNames().ToList().ForEach(p =>
                {
                    keys.Add(p);
                    rowDict.Add(p, CsvOutputRow.Create(apiResource.GetDescription(p)));
                });
            }

            // add each values to the row
            keys.ForEach(p => rowDict[p].Add(apiResource.GetValue(p)));
            return this;
        }

        public IEnumerable<CsvOutputRow> ToRows()
        {
            // 1st row is header
            IList<CsvOutputRow> rows = new List<CsvOutputRow> { header };
            // append rows by keys
            keys.ForEach(key => rows.Add(rowDict[key]));
            return rows;
        }

    }
}