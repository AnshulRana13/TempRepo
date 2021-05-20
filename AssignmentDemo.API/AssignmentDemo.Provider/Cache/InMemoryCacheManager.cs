using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Text;

namespace AssignmentDemo.Provider.Cache
{
    public class InMemoryCacheManager : ICacheManager
    {
        private IMemoryCache _cache;
       
        public InMemoryCacheManager(IMemoryCache memoryCache)
        {
            _cache = memoryCache;
        }

        public bool CheckIfKeyExists(string key)
        {
            object list;         
            return _cache.TryGetValue(key, out  list);
        }

        public T Get<T>(string key)
        {
            throw new NotImplementedException();
        }

        public List<T> GetAll<T>(string key)
        {
            var result = _cache.Get<List<T>>(key);
            return result;
            
        }

        public void Put<T>(string key, T data)
        {
            T cacheEntry;
            if (!_cache.TryGetValue(key, out cacheEntry))
            {               
                var cacheEntryOptions = new MemoryCacheEntryOptions()
                    .SetSlidingExpiration(TimeSpan.FromSeconds(60));
                _cache.Set(key, data, cacheEntryOptions);
            }
        }

        public void PutAll<T>(string key, List<T> data)
        {
            List<T> cacheEntry;
            if (!_cache.TryGetValue(key, out cacheEntry))
            {            
               var cacheEntryOptions = new MemoryCacheEntryOptions()
                    .SetSlidingExpiration(TimeSpan.FromMinutes(10));
                _cache.Set(key, data, cacheEntryOptions);
            }
        }

        public void Remove(string key)
        {
            _cache.Remove(key);
        }
    }
}
