using System.Threading.Tasks;

namespace BuffettCodeAddin.Client
{
    public interface IBuffettCodeClient
    {
        Task<string> GetQuarter(string apiKey, string ticker, string fiscalYear, string fiscalQuarter, bool isConfigureAwait = true);

        Task<string> GetQuarterRange(string apiKey, string ticker, string from, string to, bool isConfigureAwait = true);

        Task<string> GetIndicator(string apiKey, string ticker, bool isConfigureAwait = true);
    }
}
