namespace BuffettCodeIO.Property
{
    public interface IApiSchema
    {
        string GetValue(string propertyName);

        PropertyDescription GetDescription(string propertyName);
    }
}