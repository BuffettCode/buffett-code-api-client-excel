namespace BuffettCodeCommon.Period
{
    public class Snapshot : IPeriod
    {
        private static readonly Snapshot instance = new Snapshot();
        private Snapshot() { }

        public static Snapshot GetInstance() => instance;

        public override string ToString() => "Snapshot";
    }
}