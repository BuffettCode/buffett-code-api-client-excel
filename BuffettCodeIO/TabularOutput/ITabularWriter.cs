using BuffettCodeIO.Property;

namespace BuffettCodeIO.TabluarOutput
{
    interface ITabularWriter<T> where T : IApiResource
    {

        void Write(Tabular<T> tabular);
    }
}