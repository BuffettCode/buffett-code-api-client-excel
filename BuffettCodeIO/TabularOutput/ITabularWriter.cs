using BuffettCodeIO.Property;
using System;

namespace BuffettCodeIO.TabularOutput
{
    public interface ITabularWriter<T> : IDisposable where T : IApiResource
    {
        void Write(Tabular<T> tabular);
    }
}