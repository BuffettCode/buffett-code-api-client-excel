using BuffettCodeIO.Property;
using System.Collections.Generic;
using System.Linq;

namespace BuffettCodeIO.TabularOutput

{
    public class Tabular<T> where T : IApiResource
    {
        private readonly TabularRow header = TabularRow.Create("キー", "項目名", "単位");
        private readonly IDictionary<string, TabularRow> rowDict = new Dictionary<string, TabularRow>();
        private readonly List<string> keys = new List<string>();


        public Tabular<T> Add(T apiResource)
        {
            // append header column using period
            header.Add(apiResource.GetPeriod().ToString());
            var selector = new FormattedValueSelector(apiResource);
            // setup keys and values if empty
            if (keys.Count == 0)
            {
                // nest された json は対象外にする
                apiResource.GetPropertyNames().Where(name => !name.Contains(".")).ToList().ForEach(p =>
                {
                    keys.Add(p);
                    rowDict.Add(p, TabularRow.Create(apiResource.GetDescription(p)));
                });
            }

            // add each formatted values to the row
            keys.ForEach(p => rowDict[p].Add(selector.Select(p)));
            return this;
        }


        public IEnumerable<TabularRow> ToRows()
        {
            // 1st row is header
            IList<TabularRow> rows = new List<TabularRow> { header };
            // append rows by keys
            keys.ForEach(key => rows.Add(rowDict[key]));
            return rows;
        }
    }
}