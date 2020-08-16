using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using Crud.Bll;
using Dapper;

namespace Crud.Dal
{
	public class DapperRepository<T> : ICrudRepository<T> where T : class, IHasId, new()
	{
		private readonly string ConnectionString;
		private readonly string TableName;

		public DapperRepository(string connectionString, string tableName)
		{
			ConnectionString = connectionString;
			TableName = tableName;
		}

		public IEnumerable<T> GetItems()
		{
			using (var c = new SqlConnection(ConnectionString))
			{
				return c.Query<T>("SELECT * FROM [" + TableName + "]");
			}
		}

		public T GetItem(int id)
		{
			using (var c = new SqlConnection(ConnectionString))
			{
				return c.Query<T>("SELECT * FROM [" + TableName + "] WHERE [Id]=" + id.ToString()).FirstOrDefault();
			}
		}

		public int SaveItem(T e)
		{
			var propertyContainer = ObjectParser.ParseProperties(e);
			propertyContainer.DeleteIdProp();
			string sql = e.Id == 0
				? CookInsertSql(propertyContainer)
				: CookUpdateSql(propertyContainer, e.Id);
			using (var c = new SqlConnection(ConnectionString))
			{
				int? resId = c.Query<int?>
					(sql, propertyContainer.NameValuePairs, commandType: CommandType.Text).FirstOrDefault();
				return resId ?? 0;
			}
		}

		public T DeleteItem(int id)
		{
			using (var c = new SqlConnection(ConnectionString))
			{
				return c.Query<T>("DELETE [" + TableName + "] OUTPUT DELETED.* WHERE [Id]=" + id.ToString()).FirstOrDefault();
			}
		}

		private string CookInsertSql(ObjectPropertiesContainer c) =>
			string.Format("INSERT INTO [{0}] ({1}) OUTPUT INSERTED.[Id] VALUES(@{2})",
				TableName,
				string.Join(", ", c.ValueNames),
				string.Join(", @", c.ValueNames));

		private string CookUpdateSql(ObjectPropertiesContainer c, int id)
		{
			List<string> fields = new List<string>();
			foreach (string s in c.ValueNames)
			{
				fields.Add("[" + s + "]=@" + s);
			}
			return string.Format("UPDATE [{0}] SET {1} OUTPUT DELETED.[Id] WHERE [Id]={2}",
				TableName,
				string.Join(",", fields),
				id.ToString());
		}
	}
}
