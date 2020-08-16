using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;

namespace Crud.Dal
{
	public class ContextWithTable<T> : DbContext where T : class
	{
		public ContextWithTable(string connectionString) : base (connectionString)
		{
			if (Attribute.GetCustomAttribute(typeof(T), typeof(TableAttribute)) == null)
				throw new InvalidOperationException("Injected EntityFramework entity type does not have TableAttribute");
		}

		public DbSet<T> Table { get; set; }
	}
}