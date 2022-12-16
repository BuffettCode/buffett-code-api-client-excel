using BuffettCodeCommon.Period;
using System.Collections.Generic;
namespace BuffettCodeIO.Property
{
    public interface IApiResource
    {
        string GetValue(string propertyName);

        PropertyDescription GetDescription(string propertyName);

        ICollection<string> GetPropertyNames();

        IIntent GetPeriod();

    }

}