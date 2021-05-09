using System;
using System.Linq;
using System.Web;

namespace BuffettCodeAPIClient
{
    public class CacheKeyCreator
    {
        public static string Create(ApiGetRequest request)
        {

            var paramsStr = String.Join("&", request.Parameters.Select(kv => $"{HttpUtility.UrlEncode(kv.Key)}={HttpUtility.UrlEncode(kv.Value)}").OrderBy(str => str));
            return $"{request.EndPoint}?{paramsStr}";
        }

    }
}