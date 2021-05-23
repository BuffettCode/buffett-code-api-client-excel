namespace BuffettCodeAPIClient
{
    public interface IBuffettCodeApiClient
    {
        string GetApiKey();
        void UpdateApiKey(string apiKey);
        void ClearCache();
    }
}