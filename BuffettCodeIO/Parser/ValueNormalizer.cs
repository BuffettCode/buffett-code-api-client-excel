namespace BuffettCodeIO.Parser
{
    public static class ValueNormalizer
    {
        public static string Normalize(string value)
        {
            switch (value)
            {
                case null: return "";
                case "None": return "";
                default: return value;

            }

        }
    }
}