using BuffettCodeCommon.Config;
using BuffettCodeCommon.Exception;
using BuffettCodeCommon.Period;

namespace BuffettCodeExcelFunctions
{
    public class PeriodBuilderForLegacy
    {
        private DataTypeConfig dataType;
        private string parameter1;
        private string parameter2;

        public PeriodBuilderForLegacy SetDataType(DataTypeConfig dataType)
        {
            this.dataType = dataType;
            return this;
        }

        public PeriodBuilderForLegacy SetParameters(string parameter1, string parameter2)
        {
            this.parameter1 = parameter1;
            this.parameter2 = parameter2;
            return this;
        }

        public IPeriod Build()
        {
            switch (dataType)
            {
                case DataTypeConfig.Indicator:
                    return Snapshot.GetInstance();
                case DataTypeConfig.Quarter:
                    if (string.IsNullOrWhiteSpace(parameter1) && string.IsNullOrWhiteSpace(parameter2))
                    {
                        return RelativeFiscalQuarterPeriod.CreateLatest();
                    }
                    else
                    {
                        return FiscalQuarterPeriod.Create(parameter1, parameter2);
                    }
                case DataTypeConfig.Daily:
                    return LatestDayPeriod.GetInstance();
                default:
                    throw new NotSupportedDataTypeException($"dataType ={dataType} is not supported.");
            }
        }
    }
}