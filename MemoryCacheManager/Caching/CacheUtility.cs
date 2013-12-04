using System;
using System.Runtime.Caching;

namespace MemoryCacheManager.Caching
{
	public static class CacheUtility
	{
		//private static readonly ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

		/// <summary>
		///   Keys will be joined using a delimiter character.  Ensure that the result will be unique to the cached object, but reproducible so that the cached object can be retrieved reliably
		/// </summary>
		/// <param name="keys"></param>
		/// <returns></returns>
		public static string GetCacheKey(params string[] keys)
		{
			if (keys == null || keys.Length == 0)
			{
				throw new ArgumentException("You must specify one or more keys to create a cache key.", "keys");
			}
			return string.Join("-", keys);
		}

		/// <summary>
		///   Returns cached data or, if expired, refreshes cache and returns
		///   Details of caching is configured via the web.config ServiceCachingSection
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="cache"></param>
		/// <param name="cacheItemKey"></param>
		/// <param name="configuration"></param>
		/// <param name="reload"></param>
		/// <returns></returns>
		public static T GetCachedData<T>(this ObjectCache cache, string cacheItemKey, CacheItemConfiguration configuration, Func<T> reload) where T : class
		{
			bool enableCache = configuration.EnableCache && configuration.EnableItemCache;
			if (enableCache)
			{
				var cachedData = (T) cache.Get(cacheItemKey);

				if (cachedData != null)
				{
					return cachedData;
				}
				//if (log.IsInfoEnabled)
				//{
				//	log.InfoFormat("Cached key {0} is expired; Reloading from repository.", cacheItemKey);
				//}
			}
			var data = reload();

			if (enableCache)
			{
				DateTimeOffset expiration = DateTimeOffset.Now.AddSeconds(configuration.AbsoluteExpirationInSeconds);
				cache.Add(cacheItemKey, data, new CacheItemPolicy { AbsoluteExpiration = expiration });
				
				//if (log.IsInfoEnabled)
				//{
				//	log.InfoFormat("Cached key {0} to expire at {1}", cacheItemKey, expiration.DateTime);
				//}
			}
			return data;
		}
	}
}