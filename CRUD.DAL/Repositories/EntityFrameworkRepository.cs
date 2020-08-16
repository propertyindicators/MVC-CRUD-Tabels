using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using Crud.Bll;

namespace Crud.Dal
{
	public class EntityFrameworkRepository<TBll, TDal> : ICrudRepository<TBll>
		where TBll : class, IHasId
		where TDal: class, IConvertable<TBll>, IHasId, new()
	{
		private readonly string ConnectionString;

		public EntityFrameworkRepository(string connectionString)
		{
			ConnectionString = connectionString;
		}

		public IEnumerable<TBll> GetItems()
		{
			using (var c = new ContextWithTable<TDal>(ConnectionString))
			{
				return c.Table.AsEnumerable().Select(i => i.ConvertToBll()).ToList();
			}
		}

		public TBll GetItem(int id)
		{
			using (var c = new ContextWithTable<TDal>(ConnectionString))
			{
				return c.Table.Find(id)?.ConvertToBll();
			}
		}


		public int SaveItem(TBll e)
		{
			using (var c = new ContextWithTable<TDal>(ConnectionString))
			{
				TDal item;
				if (e.Id == 0)
				{
					item = new TDal();
					item.InitWith(e);
					c.Table.Add(item);
				}
				else
				{
					item = c.Table.FirstOrDefault(x => x.Id == e.Id);
					if (item == null) return 0;
					item.InitWith(e);
					c.Entry(item).State = EntityState.Modified;
				}
				c.SaveChanges();
				return item.Id;
			}
		}

		public TBll DeleteItem(int id)
		{
			using (var c = new ContextWithTable<TDal>(ConnectionString))
			{
				var item = c.Table.SingleOrDefault(x => x.Id == id);
				if (item == null) return null;
				c.Entry(item).State = EntityState.Deleted;
				c.SaveChanges();
				return item.ConvertToBll();
			}
		}
	}

	public class EntityFrameworkRepositoryOld<TBll, TDal> : ICrudRepository<TBll>
		where TBll : class, IHasId
		where TDal : class, IConvertable<TBll>, IHasId, new()
	{
		private readonly ContextWithTable<TDal> ContextWithTable;

		public EntityFrameworkRepositoryOld(ContextWithTable<TDal> c)
		{
			ContextWithTable = c;
		}

		public IEnumerable<TBll> GetItems() =>
			ContextWithTable.Table.AsEnumerable().Select(i => i.ConvertToBll());

		public TBll GetItem(int id) => ContextWithTable.Table.Find(id)?.ConvertToBll();


		public int SaveItem(TBll e)
		{
			TDal item;
			if (e.Id == 0)
			{
				item = new TDal();
				item.InitWith(e);
				ContextWithTable.Table.Add(item);
			}
			else
			{
				item = ContextWithTable.Table.FirstOrDefault(x => x.Id == e.Id);
				if (item == null) return 0;
				item.InitWith(e);
				ContextWithTable.Entry(item).State = EntityState.Modified;
			}
			ContextWithTable.SaveChanges();
			return item.Id;
		}

		public TBll DeleteItem(int id)
		{
			var item = ContextWithTable.Table.SingleOrDefault(x => x.Id == id);
			if (item == null) return null;
			ContextWithTable.Entry(item).State = EntityState.Deleted;
			ContextWithTable.SaveChanges();
			return item.ConvertToBll();
		}
	}
}