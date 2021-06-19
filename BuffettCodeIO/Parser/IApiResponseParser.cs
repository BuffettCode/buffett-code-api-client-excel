using BuffettCodeCommon.Config;
using BuffettCodeIO.Property;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;

namespace BuffettCodeIO.Parser
{
    interface IApiResponseParser
    {
        IApiResource Parse(DataTypeConfig dataType, JObject json);
        IList<IApiResource> ParseRange(DataTypeConfig dataType, JObject json);
    }
}