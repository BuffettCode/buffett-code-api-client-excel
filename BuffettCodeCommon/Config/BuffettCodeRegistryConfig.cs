using System.Collections.Generic;
namespace BuffettCodeCommon.Registry
{
    public static class BuffettCodeRegistryConfig
    {

        public static readonly string SubKeyBuffettCodeExcelAddinRelease = @"Software\BuffettCode\ExcelAddin";
        public static readonly string SubKeyBuffettCodeExcelAddinDev = @"Software\BuffettCode\ExcelAddinDev";

        public static string SubKeyBuffettCodeExcelAddinTest
            = @"Software\BuffettCode\ExcelAddinTest";

        public static readonly string NameApiKey = "ApiKey";

        public static readonly string NameIsOndemandEndpointEnabled = "IsOndemandEndpointEnabled";
        public static readonly string NameClearCache = "ClearCache";

        public static readonly string NameDebugMode = "DebugMode";

        public static readonly ISet<string> SubKeys = new HashSet<string>
        {
            SubKeyBuffettCodeExcelAddinRelease,
            SubKeyBuffettCodeExcelAddinDev,
            SubKeyBuffettCodeExcelAddinTest,
        };

        public static readonly ISet<string> SupportedValueNames = new HashSet<string>
        {
            NameApiKey,
            NameIsOndemandEndpointEnabled,
            NameClearCache,
            NameDebugMode,
        };

    }
}