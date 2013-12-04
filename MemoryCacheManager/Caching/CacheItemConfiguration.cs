using MemoryCacheManager.Configuration;

namespace MemoryCacheManager.Caching
{
	public class CacheItemConfiguration
	{
		public string CacheItemConfigKey { get; private set; }
		public bool EnableCache { get { return ServiceCachingSection.Settings.EnableCaching; }}
		public bool EnableItemCache { get { return ServiceCachingSection.Settings.CacheItems[CacheItemConfigKey].EnableCaching; } }
		public int AbsoluteExpirationInSeconds { get { return ServiceCachingSection.Settings.CacheItems[CacheItemConfigKey].AbsoluteExpirationInSeconds; } }

		public CacheItemConfiguration(string cacheItemConfigKey)
		{
			CacheItemConfigKey = cacheItemConfigKey;
		}
	}
}