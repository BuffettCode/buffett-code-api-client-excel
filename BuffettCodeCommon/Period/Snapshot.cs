namespace BuffettCodeCommon.Period
{
    public class Snapshot : IIntent
    {
        private static readonly Snapshot instance = new Snapshot();
        private Snapshot() { }

        public static Snapshot GetInstance() => instance;

        public override string ToString() => "Snapshot";
    }
}