using System.Collections.Generic;

namespace Crud.Bll
{
	public interface ICrudRepository<T> where T : IHasId
	{
		IEnumerable<T> GetItems(); // get all items
		T GetItem(int id);
		int SaveItem(T item);
		T DeleteItem(int id);
	}
}
