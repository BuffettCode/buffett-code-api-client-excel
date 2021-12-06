using BuffettCodeCommon.Config;
using BuffettCodeCommon.Exception;

namespace BuffettCodeIO.Resolver
{
    static public class LegacyDataTypeResolver
    {
        public static ILegacyDataTypeResolver GetInstance(BuffettCodeApiVersion version)
        {
            switch (version)
            {
                case BuffettCodeApiVersion.Version2:
                    return ApiV2LegacyDataTypeResolver.GetInstance();
                case BuffettCodeApiVersion.Version3:
                    return ApiV3LegacyDataTypeResolver.GetInstance();
                default:
                    throw new NonSupportedApiVersionException($"api version={version} is not supported.");
            }
        }
    }
}