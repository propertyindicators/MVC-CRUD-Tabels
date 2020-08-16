using System;

namespace Crud.Bll
{
	public class CrudServiceSupportCache<T> : CrudServiceBase<T> where T : IHasId
	{
		protected readonly ICrudRepository<T>  CacheRepository;

		public CrudServiceSupportCache(ICrudRepository<T> mainRep, ICrudRepository<T> cacheRep) : base(mainRep)
		{
			CacheRepository = cacheRep ?? throw new ArgumentNullException("Injected cache repository is null");
		}

		public override T GetItem(int id)
		{
			T cachedItem = CacheRepository.GetItem(id);
			if (cachedItem != null)
			{
				CacheRepository.SaveItem(cachedItem); // Expiration time updating
				return cachedItem;
			}
			T item = BaseRepository.GetItem(id);
			if (item == null) return item;
			CacheRepository.SaveItem(item);
			return item;
		}

		public override int SaveItem(T item)
		{
			int id = BaseRepository.SaveItem(item);
			if (item.Id == 0) item.Id = id;
			CacheRepository.SaveItem(item);
			return id;
		}

		public override T DeleteItem(int id)
		{
			CacheRepository.DeleteItem(id);
			return BaseRepository.DeleteItem(id);
		}
	}
}
