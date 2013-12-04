using System;
using System.Runtime.Caching;
using MemoryCacheManager.Caching;

namespace MemoryCacheManager.Example
{
	public class ExampleCacheManager : IManageACache<int, ExampleExpensiveClass>
	{
		private const string CACHE_ITEM_CONFIG_KEY = "exampleExpensiveClass";
		private const string CACHE_ITEM_KEY_BASE = "exampleExpensiveClass";

		private readonly CacheManager<int, ExampleExpensiveClass> cacheManager;

		public ExampleCacheManager(ObjectCache objectCache)
		{
			cacheManager = new CacheManager<int, ExampleExpensiveClass>(
				objectCache, CACHE_ITEM_CONFIG_KEY, GetCacheKey, LoadResource);
		}

		public ExampleExpensiveClass GetData(int identity)
		{
			return cacheManager.GetData(identity);
		}

		public string GetCacheKey(int identity)
		{
			return CacheUtility.GetCacheKey(CACHE_ITEM_KEY_BASE, identity.ToString());
		}

		public void ClearCache(int identity)
		{
			cacheManager.ClearCache(identity);
		}

		private ExampleExpensiveClass LoadResource(int identity)
		{
			return new ExampleExpensiveClass(identity);
		}
	}

	/// note that normally the class to be cached would reside within the business tier of the appropriate solution
	public class ExampleExpensiveClass
	{
		public int Identity { get; private set; }
		public DateTime CreationTime { get; private set; }

		public ExampleExpensiveClass(int identity)
		{
			Identity = identity;
			CreationTime = DateTime.Now;
		}
	}
}