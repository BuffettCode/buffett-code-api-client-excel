using BuffettCodeCommon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace BuffettCodeAPIClient
{
    public class ApiRequestLock
    {
        private static readonly Dictionary<string, object> lockObjects = new Dictionary<string, object>();


        [MethodImpl(MethodImplOptions.Synchronized)]
        public static object GetLockObject(ApiGetRequest request)
        {
            string lockKey = request.EndPoint;
            lockKey += String.Join("&", request.Parameters.Select(kv => $"{kv.Key}={kv.Value}").OrderBy(str => str));

            if (!lockObjects.ContainsKey(lockKey))
            {
                lockObjects[lockKey] = new object();
            }

            return lockObjects[lockKey];
        }
    }
}