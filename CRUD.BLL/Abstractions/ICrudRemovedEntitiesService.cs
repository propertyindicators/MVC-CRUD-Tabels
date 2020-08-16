using System.Collections.Generic;

namespace Crud.Bll
{
	public interface ICrudRemovedEntitiesService<T> where T : IHasId, ICrudRemovedEntity
	{
		IEnumerable<T> GetItems(); // get all items
		T GetItem(int id);
		T DeleteItem(int id);
		int RevertItem(int id);
	}
}
