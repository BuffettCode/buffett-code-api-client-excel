using BuffettCodeAPIClient;
using BuffettCodeCommon.Config;
using BuffettCodeCommon.Exception;
using BuffettCodeCommon.Period;
using BuffettCodeCommon.Validator;

namespace BuffettCodeExcelFunctions
{
    public class TickerParameterBuilderForLegacy : ITickerPeriodParameterBuilder
    {
        private DataTypeConfig dataType;
        private string ticker;
        private string parameter1;
        private string parameter2;

        public TickerParameterBuilderForLegacy SetDataType(DataTypeConfig dataType)
        {
            this.dataType = dataType;
            return this;
        }


        public TickerParameterBuilderForLegacy SetTicker(string ticker)
        {
            JpTickerValidator.Validate(ticker);
            this.ticker = ticker;
            return this;
        }

        public TickerParameterBuilderForLegacy SetParameters(string parameter1, string parameter2)
        {
            this.parameter1 = parameter1;
            this.parameter2 = parameter2;
            return this;
        }

        public ITickerPeriodParameter Build()
        {
            switch (dataType)
            {
                case DataTypeConfig.Indicator:
                    return TickerEmptyPeriodParameter.Create(ticker, Snapshot.GetInstance());
                case DataTypeConfig.Quarter:
                    if (string.IsNullOrWhiteSpace(parameter1) && string.IsNullOrWhiteSpace(parameter2))
                    {
                        return TickerQuarterParameter.Create(ticker, RelativeFiscalQuarterPeriod.CreateLatest());
                    }
                    else
                    {
                        return TickerQuarterParameter.Create(ticker, FiscalQuarterPeriod.Create(parameter1, parameter2));
                    }
                case DataTypeConfig.Daily:
                    return TickerDayParameter.Create(ticker, LatestDayPeriod.GetInstance());
                default:
                    throw new NotSupportedDataTypeException($"dataType ={dataType} is not supported.");
            }
        }
    }
}