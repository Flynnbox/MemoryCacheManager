using System;
using System.Runtime.Caching;
using System.Threading;

namespace MemoryCacheManager.Example
{
	public class RunTest
	{
		private static MemoryCache cache;
		private static ExampleCacheManager exampleCache;

		public static void Main()
		{
			using (ExecutionContext.SuppressFlow())
			{
				cache = new MemoryCache("mobileEventsData");
			}
			exampleCache = new ExampleCacheManager(cache);
			Console.WriteLine("Retrieve Creation Time for Object 1: {0}", exampleCache.GetData(1).CreationTime.Ticks);
			Console.WriteLine("Retrieve Creation Time for Object 1: {0}", exampleCache.GetData(1).CreationTime.Ticks);
			exampleCache.ClearCache(1);
			Console.WriteLine("Cache Cleared for Object 1");
			Console.WriteLine("Retrieve Creation Time for Object 1: {0}", exampleCache.GetData(1).CreationTime.Ticks);
		}
	}
}