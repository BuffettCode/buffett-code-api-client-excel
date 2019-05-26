namespace BuffettCodeAddin
{
    interface IPropertyAggregation
    {
        string GetIdentifier();

        string GetValue(string propertyName);

        PropertyDescrption GetDescription(string propertyName);
    }
}
