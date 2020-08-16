using System;
using System.Collections.Generic;

namespace Crud.Bll
{
	public class CrudServiceRemovedEntities<TRemoved, TMain> : ICrudRemovedEntitiesService<TRemoved>
		where TRemoved : TMain, ICrudRemovedEntity
		where TMain: IHasId
	{
		private readonly ICrudRepository<TRemoved> RepositoryWithRemovedEntities;
		private readonly ICrudRepository<TMain> RepositoryFromWhereRemoved;


		public CrudServiceRemovedEntities(
			ICrudRepository<TRemoved> removedRep,
			ICrudRepository<TMain> mainRep)
		{
			RepositoryWithRemovedEntities = removedRep
				?? throw new ArgumentNullException("Injected repository DAL for removed entities is null");
			RepositoryFromWhereRemoved = mainRep
				?? throw new ArgumentNullException("Injected repository DAL for active entities is null");
		}

		public IEnumerable<TRemoved> GetItems() => RepositoryWithRemovedEntities.GetItems();

		public TRemoved GetItem(int id) => RepositoryWithRemovedEntities.GetItem(id);

		public TRemoved DeleteItem(int id) => RepositoryWithRemovedEntities.DeleteItem(id);

		public int RevertItem(int id)
		{
			TRemoved removedItem = RepositoryWithRemovedEntities.DeleteItem(id);
			if (removedItem == null) return 0;
			removedItem.Id = 0;
			return RepositoryFromWhereRemoved.SaveItem(removedItem);
		}
	}
}
