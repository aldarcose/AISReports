using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Caching;

namespace DbCaching
{
    public static class DbCache
    {
        private static object _lock = new Object();
        private static MemoryCache _cache = new MemoryCache("DbCache");

        public static object GetItem(string key)
        {
            return _cache.Get(key);
        }

        public static void SetItem(string key, object value, double secCount)
        {
            var policy = new CacheItemPolicy();
            policy.AbsoluteExpiration = DateTimeOffset.Now.AddSeconds(secCount);
            _cache.AddOrGetExisting(key, value, policy);
        }

        private static T AddOrGetExisting<T>(string key, Func<T> valueFactory)
        {
            var newValue = new Lazy<T>(valueFactory);
            var oldValue = _cache.AddOrGetExisting(key, newValue, new CacheItemPolicy()) as Lazy<T>;
            try
            {
                return (oldValue ?? newValue).Value;
            }catch
            {
                _cache.Remove(key);
                throw;
            }
        }

        private static object InitItem(string key)
        {
            return new { Value = key.ToUpper() };
        }

    }
}
