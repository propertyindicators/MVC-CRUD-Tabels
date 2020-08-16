using System;
using System.Collections.Generic;

namespace Crud.Bll
{
	public class CrudServiceBase<T> : ICrudRepository<T> where T : IHasId
	{
		protected readonly ICrudRepository<T>  BaseRepository;

		public CrudServiceBase(ICrudRepository<T> r)
		{
			BaseRepository = r ?? throw new ArgumentNullException("Injected repository DAL is null");
		}

		public IEnumerable<T> GetItems() => BaseRepository.GetItems();

		public virtual T GetItem(int id) => BaseRepository.GetItem(id);

		public virtual int SaveItem(T item) => BaseRepository.SaveItem(item);

		public virtual T DeleteItem(int id) => BaseRepository.DeleteItem(id);
	}
}
