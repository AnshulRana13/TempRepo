using System;
using Newtonsoft.Json;
using StackExchange.Redis;
using System.Collections.Generic;
using System.Text;
using AssignmentDemo.Entities.API.AppConfig;
using System.Threading.Tasks;
using System.Diagnostics.CodeAnalysis;
using System.Collections;

namespace AssignmentDemo.Provider.Cache
{
    public class RedisCacheManager : ICacheManager
    {
        private readonly IDatabase _cache;

        private readonly Lazy<ConnectionMultiplexer> LazyRedisConnection = new Lazy<ConnectionMultiplexer>(() =>
        {
            string cacheConnection = Environment.GetEnvironmentVariable("RedisConnection", EnvironmentVariableTarget.Process);         
            return ConnectionMultiplexer.Connect(cacheConnection);
        });


        public RedisCacheManager()
        {
            _cache = LazyRedisConnection.Value.GetDatabase();
           
        }

        public bool CheckIfKeyExists(string key)
        {
            return _cache.KeyExists(key);
        }

        public T Get<T>(string key)
        {
            if (this.CheckIfKeyExists(key))
                return JsonConvert.DeserializeObject<T>(_cache.StringGet(key));
            else
                return default;
        }

        public List<T> GetAll<T>(string key)
        {
            if (this.CheckIfKeyExists(key))
                return JsonConvert.DeserializeObject<List<T>>(_cache.StringGet(key));
            else
                return default;
        }

        public void Put<T>(string key, T data)
        {
            if (this.CheckIfKeyExists(key))
                this.Remove(key);
            _cache.StringSet(key, JsonConvert.SerializeObject(data));
            _cache.KeyExpire(key, new TimeSpan(0, 5, 20));
        }

        public void PutAll<T>(string key, List<T> data)
        {
            if (this.CheckIfKeyExists(key))
                this.Remove(key);
            _cache.StringSet(key, JsonConvert.SerializeObject(data));
            _cache.KeyExpire(key, new TimeSpan(0, 5, 20));
        }

        public void Remove(string key)
        {
            _cache.KeyDelete(key);
        }
    }
 
}
