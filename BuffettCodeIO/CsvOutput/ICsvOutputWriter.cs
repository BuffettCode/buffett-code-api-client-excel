using BuffettCodeIO.Property;

namespace BuffettCodeIO.CsvOutput
{
    interface ICsvOutputWriter<T> where T : IApiResource
    {

        void Write(CsvOutput<T> csvOutput);
    }
}