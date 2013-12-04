using System;
using System.Runtime.Caching;

namespace MemoryCacheManager.Caching
{
	public class CacheManager<T, U> : IManageACache<T, U> where U : class
	{
		//private readonly ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
		private readonly ObjectCache cache;
		private readonly CacheItemConfiguration configuration;
		private readonly Func<T, string> getCacheKey;
		private readonly Func<T, U> loadData;

		public CacheManager(ObjectCache objectCache, string cacheItemConfigKey, Func<T, string> computeCacheKey, Func<T, U> loadFromRepository)
		{
			cache = objectCache;
			configuration = new CacheItemConfiguration(cacheItemConfigKey);
			getCacheKey = computeCacheKey;
			loadData = loadFromRepository;
		}

		/// <summary>
		///   Returns cached data or, if expired, refreshes cache and returns data
		///   Details of caching is configured via the web.config ServiceCachingSection
		/// </summary>
		/// <param name="cacheKeyComponent"></param>
		/// <returns></returns>
		public U GetData(T cacheKeyComponent)
		{
			return cache.GetCachedData(getCacheKey(cacheKeyComponent), configuration, (() => loadData(cacheKeyComponent)));
		}

		/// <summary>
		///   Clears the cache for this key
		/// </summary>
		/// <param name="cacheKeyComponent"></param>
		public void ClearCache(T cacheKeyComponent)
		{
			string cacheKey = getCacheKey(cacheKeyComponent);
			cache.Remove(cacheKey);
			//if (log.IsInfoEnabled)
			//{
			//	log.InfoFormat("Cleared cache for cacheKey {0}", cacheKey);
			//}
		}
	}
}