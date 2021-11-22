namespace BuffettCodeCommon.Period
{
    public class LatestFiscalQuarterPeriod : IQuarterlyPeriod
    {
        private static readonly LatestFiscalQuarterPeriod instance = new LatestFiscalQuarterPeriod();
        private LatestFiscalQuarterPeriod() { }

        public static LatestFiscalQuarterPeriod GetInstance() => instance;

        public override string ToString() => "LatestFiscalQuarter";

    }
}
