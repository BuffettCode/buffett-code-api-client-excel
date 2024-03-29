namespace BuffettCodeCommon.Config
{
    public enum BuffettCodeApiVersion
    {
        Version3 = 3,
    }
    public static class BuffettCodeApiKeyConfig
    {
        public static string TestApiKey = "sAJGq9JH193KiwnF947v74KnDYkO7z634LWQQfPY";
    }

    public static class BuffettCodeApiV2Config
    {
        public static readonly string BASE_URL = "https://api.buffett-code.com/api/v2/";
        public static readonly string ENDPOINT_QUARTER = "quarter";
        public static readonly string ENDPOINT_ONDEMAND_QUARTER = "ondemand/quarter";
        public static readonly string ENDPOINT_INDICATOR = "indicator";
        public static readonly string ENDPOINT_DAILY = "daily";
        public static readonly string ENDPOINT_COMPANY = "company";
    }

    public static class BuffettCodeApiV3Config
    {
        public static readonly string BASE_URL = "https://api.buffett-code.com/api/v3/";
        public static readonly string ENDPOINT_DAILY = "daily";
        public static readonly string ENDPOINT_ONDEMAND_DAILY = "ondemand/daily";
        public static readonly string ENDPOINT_BULK_DAILY = "bulk/daily";
        public static readonly string ENDPOINT_QUARTER = "quarter";
        public static readonly string ENDPOINT_ONDEMAND_QUARTER = "ondemand/quarter";
        public static readonly string ENDPOINT_BULK_QUARTER = "bulk/quarter";
        public static readonly string ENDPOINT_COMPANY = "company";
        public static readonly string ENDPOINT_MONTHLY = "monthly";
    }


    public static class ApiErrorMessageConfig
    {
        public static readonly string TEST_API_CONSTRAINT = "Testing Apikey is only allowed to ticker ending with";
    }

    public static class ApiRelatedUrlConfig
    {
        public static readonly string API_SPECIAL_NOTES = "https://www.buffett-code.com/legal/web_api/special_notes";
        public static readonly string ONDEMAND_API_USAGE_ENTRY = "https://blog.buffett-code.com/entry/ondemand_api_usage";
    }

}