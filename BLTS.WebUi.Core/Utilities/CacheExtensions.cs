using System;
using System.Runtime.Caching;

namespace BLTS.WebUi.Utilities
{
    public static class CacheExtensions
    {
        /// <summary>
        /// either returns an exesiting cache value or it creates a new one and returns the value
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="cache"></param>
        /// <param name="key"></param>
        /// <param name="valueFactory"></param>
        /// <param name="policy"></param>
        /// <returns></returns>
        public static T AddOrGetExistingCacheEntry<T>(this ObjectCache cache, string key, Func<T> valueFactory, CacheItemPolicy policy)
        {
            Lazy<T> newValue = new Lazy<T>(valueFactory);
            Lazy<T> oldValue = (Lazy<T>)cache.AddOrGetExisting(key, newValue, policy);

            try
            {
                return oldValue != null ? oldValue.Value : newValue.Value;
            }
            catch
            {
                cache.Remove(key);
                throw;
            }
        }
    }
}
