using System;
using System.Collections.Generic;
using System.Runtime.Caching;
using Crud.Bll;

namespace Crud.Dal
{
	public class MemoryCacheRepository<T> : ICrudRepository<T>
		where T : class, IHasId
	{
		private static readonly MemoryCache Cache = MemoryCache.Default;
		private readonly string EntityName;

		private readonly int ExpirationPeriodSeconds = 1800;
		public static int TestExpirationPeriodSeconds = 3;

		public MemoryCacheRepository(bool testing = false)
		{
			EntityName = typeof(T).GetType().Name;
			if (testing) ExpirationPeriodSeconds = TestExpirationPeriodSeconds;
		}

		public IEnumerable<T> GetItems()
		{
			throw new InvalidOperationException("Unexpected MemoryCacheRepository.GetItems() call");
		}

		public T GetItem(int id) => Cache.Get(GetCacheKey(id)) as T;

		public int SaveItem(T e)
		{
			if (e.Id == 0)
				throw new InvalidOperationException("Id of cached entity equals 0");
			if (Contains(e.Id))
			{
				Cache.Set(GetCacheKey(e.Id), e, ExpiresOn());
			}
			else
			{
				Cache.Add(GetCacheKey(e.Id), e, ExpiresOn());
			}
			return e.Id;
		}

		public T DeleteItem(int id)
		{
			if (!Contains(id)) return null;
			var removed = GetItem(id);
			Cache.Remove(GetCacheKey(id));
			return removed;
		}

		private string GetCacheKey(int Id) => EntityName + Id.ToString();

		private bool Contains(int id) => Cache.Contains(GetCacheKey(id));

		private DateTime ExpiresOn() => DateTime.Now.AddSeconds(ExpirationPeriodSeconds);
	}
}