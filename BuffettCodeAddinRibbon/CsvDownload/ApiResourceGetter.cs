using System.Collections.Generic;
using System.Linq;
using BuffettCodeCommon;
using BuffettCodeCommon.Config;
using BuffettCodeCommon.Period;
using BuffettCodeIO;
using BuffettCodeIO.Property;
using BuffettCodeAddinRibbon.Settings;
using System.Threading.Tasks;

namespace BuffettCodeAddinRibbon.CsvDownload
{
    public class ApiResourceGetter
    {
        private readonly BuffettCodeApiTaskProcessor processor;

        private ApiResourceGetter(BuffettCodeApiTaskProcessor processor)
        {
            this.processor = processor;
        }

        public static ApiResourceGetter Create(Configuration config)
        {
            var processor = new BuffettCodeApiTaskProcessor(config.ApiVersion, config.ApiKey, config.MaxDegreeOfParallelism, config.IsOndemandEndpointEnabled);
            return new ApiResourceGetter(processor);
        }

        public static ApiResourceGetter Create()
        {
            return Create(Configuration.GetInstance());
        }

        public IEnumerable<Quarter> GetQuarters(CsvDownloadParameters parameters)
        {

            // set isConfigureAwait for performance
            // https://devblogs.microsoft.com/dotnet/configureawait-faq/#why-would-i-want-to-use-configureawaitfalse
            var quarters = PeriodRange<FiscalQuarterPeriod>.Slice(parameters.Range, 12)
                            .SelectMany
                            (r => processor.GetApiResources(DataTypeConfig.Quarter, parameters.Ticker, r.From, r.To, false, true)
                            ).Cast<Quarter>();
            return quarters.Distinct().OrderBy(q => q.Period);
        }
    }
}
