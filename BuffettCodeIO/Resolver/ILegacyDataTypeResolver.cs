using BuffettCodeCommon.Config;
namespace BuffettCodeIO.Resolver
{

    public interface ILegacyDataTypeResolver
    {
        DataTypeConfig Resolve(string propertyName);
    }
}