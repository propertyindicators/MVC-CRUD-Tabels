using System;

namespace Crud.Bll
{
	public class CrudServiceSupportRemoved<TBase, TRemoved>
		: CrudServiceBase<TBase>
		where TBase: IHasId
		where TRemoved : TBase, ICrudRemovedEntity, new()
	{
		private readonly ICrudRepository<TRemoved> repositoryRemoved;

		public CrudServiceSupportRemoved(
			ICrudRepository<TBase> mainRep,
			ICrudRepository<TRemoved> removedRep)
			: base (mainRep)
		{
			repositoryRemoved = removedRep
				?? throw new ArgumentNullException("Injected repository DAL for removed entities is null");
		}

		public override TBase DeleteItem(int id)
		{
			TBase deletedItem = BaseRepository.DeleteItem(id);
			if (deletedItem == null) return default(TBase);
			TRemoved removedItem = new TRemoved();
			removedItem.InitWithBase(deletedItem);
			removedItem.Id = 0;
			repositoryRemoved.SaveItem(removedItem);
			return deletedItem;
		}
	}
}
