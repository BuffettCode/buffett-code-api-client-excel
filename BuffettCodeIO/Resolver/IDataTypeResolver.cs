using BuffettCodeCommon.Config;
namespace BuffettCodeIO.Resolver
{

    public interface IDataTypeResolver
    {
        DataTypeConfig Resolve(string propertyName);
    }
}