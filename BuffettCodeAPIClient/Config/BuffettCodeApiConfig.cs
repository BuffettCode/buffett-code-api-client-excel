namespace BuffettCodeAPIClient.Config
{

    public static class BuffettCodeApiConfig
    {
        public static readonly string TEST_API_KEY = "sAJGq9JH193KiwnF947v74KnDYkO7z634LWQQfPY";
        public static readonly string VERSION_V2 = "v2";
        public static readonly string VERSION_V3 = "v3";
    }

    public static class BuffettCodeApiV2Config
    {
        public static readonly string BASE_URL = "https://api.buffett-code.com/api/v2/";
        public static readonly string ENDPOINT_QUARTER = "quarter";
        public static readonly string ENDPOINT_ONDEMAND_QUARTER = "ondemand/quarter";
        public static readonly string ENDPOINT_INDICATOR = "indicator";
        public static readonly string ENDPOINT_DAILY = "daily";
    }

    public static class BuffettCodeApiV3Config
    {
        public static readonly string BASE_URL = "https://api.buffett-code.com/api/v3/";
        public static readonly string ENDPOINT_DAILY = "daily";
        public static readonly string ENDPOINT_ONDEMAND_DAILY = "ondemand/daily";
    }

    public static class ApiErrorMessageConfig
    {
        public static readonly string TEST_API_CONSTRAINT = "Testing Apikey is only allowed to ticker ending with";
    }
}