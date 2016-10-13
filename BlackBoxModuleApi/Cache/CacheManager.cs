using Newtonsoft.Json;
using StackExchange.Redis;
using System;
using System.Configuration;

namespace BlackBoxModuleApi.Cache
{
    public class CacheManager
    {

        private readonly IDatabase cache;

        private static Lazy<ConnectionMultiplexer> lazyConnection = new Lazy<ConnectionMultiplexer>(() =>
        {
            return ConnectionMultiplexer.Connect(ConfigurationManager.
                AppSettings["CacheConnection"]);
        });

        public static ConnectionMultiplexer Connection
        {
            get
            {
                return lazyConnection.Value;
            }
        }

        public CacheManager()
        {
            cache = Connection.GetDatabase();
        }

        public void Set<T>(string key, T data)
        {
            cache.StringSet(key, JsonConvert.SerializeObject(data));
        }

        public T Get<T>(string key)
        {
            return JsonConvert.DeserializeObject<T>(cache.StringGet(key));
        }

    }
}