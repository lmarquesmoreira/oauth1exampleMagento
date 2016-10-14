using Newtonsoft.Json;
using StackExchange.Redis;
using System;
using System.Configuration;

namespace BlackBoxModuleApi.Cache
{

    public class CacheManager : ICacheManager
    {

        protected readonly IDatabase Cache;

        public CacheManager()
        {
            Cache = Connection.GetDatabase();
        }

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

        public void Set<T>(string key, T data)
        {
            Cache.StringSet(key, JsonConvert.SerializeObject(data));
        }

        public T Get<T>(string key)
        {
            return JsonConvert.DeserializeObject<T>(Cache.StringGet(key));
        }

    }
}