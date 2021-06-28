using System.Collections.Generic;
namespace BuffettCodeIO.Property
{
    public class EmptyResource : IApiResource
    {
        private static readonly PropertyDescription emptyDescription = new PropertyDescription("", "", "");

        private static readonly List<string> emptyPropertyNames = new List<string>();

        private static readonly EmptyResource singleton = new EmptyResource();

        private EmptyResource() { }

        public PropertyDescription GetDescription(string propertyName) => emptyDescription;
        public ICollection<string> GetPropertyNames() => emptyPropertyNames;
        public string GetValue(string propertyName) => "";

        public static EmptyResource GetInstance() => singleton;

    }
}