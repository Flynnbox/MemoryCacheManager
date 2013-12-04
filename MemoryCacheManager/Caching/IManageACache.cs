namespace MemoryCacheManager.Caching
{
	public interface IManageACache<T, U> where U : class
	{
		U GetData(T cacheKeyComponent);

		void ClearCache(T cacheKeyComponent);
	}
}