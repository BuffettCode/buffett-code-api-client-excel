namespace BuffettCodeCommon.Period
{
    public class LatestDayPeriod : IDailyPeriod
    {
        private static readonly LatestDayPeriod instance = new LatestDayPeriod();
        private LatestDayPeriod() { }

        public static LatestDayPeriod GetInstance() => instance;

        public override string ToString() => "LatestDayPeriod";
    }
}