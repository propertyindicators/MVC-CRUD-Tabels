using System.Collections.Generic;
using System.IO;
using Crud.Bll;
using Crud.Dal;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using UnitTests.RepositoriesTest;

namespace UnitTests.DALTests
{
	[TestClass]
	public class DbRepositoryTests
	{
		private readonly string ConnectionString = @"Data Source=(LocalDb)\MSSQLLocalDB;AttachDbFilename="
			+ Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName)
			+ @"\ProbMVC\App_Data\aspnet-ProbMVC-20170923120157.mdf;Initial Catalog=aspnet-ProbMVC-20170923120157;Integrated Security=True";

		[TestMethod]
		public void DapperRepositoryTest()
		{
			var repositoryDapper = new DapperRepository<EmployeeModel>(ConnectionString, "Employees");
			TestRepositoryScenario(repositoryDapper, TestCrudEntity.Create());
		}

		[TestMethod]
		public void EntityFrameworkRepositoryTest()
		{
			var repositoryEF = new EntityFrameworkRepository<EmployeeModel, Employee>(ConnectionString);
			TestRepositoryScenario(repositoryEF, TestCrudEntity.Create());
		}

		public static void TestRepositoryScenario(ICrudRepository<EmployeeModel> repository, EmployeeModel item)
		{
			IEnumerable<EmployeeModel> allItems = repository.GetItems();
			int recordsCountInit = CountRecords(allItems);
			int ageInit = item.Age;
			int itemId = repository.SaveItem(item);
			allItems = repository.GetItems();
			Assert.AreEqual(recordsCountInit + 1, CountRecords(allItems));
			EmployeeModel savedItem = repository.GetItem(itemId);
			Assert.IsNotNull(savedItem);
			savedItem.Age++;
			repository.SaveItem(savedItem);
			EmployeeModel updatedItem = repository.GetItem(itemId);
			Assert.AreEqual(ageInit + 1, updatedItem.Age);
			EmployeeModel deletedItem = repository.DeleteItem(itemId);
			Assert.IsNotNull(deletedItem);
			allItems = repository.GetItems();
			Assert.AreEqual(recordsCountInit, CountRecords(allItems));
			Assert.IsNull(repository.GetItem(0));
			Assert.IsNull(repository.DeleteItem(0));
		}

		private static int CountRecords(IEnumerable<EmployeeModel> l)
		{
			int recordsCountInit = 0;
			foreach (var i in l) recordsCountInit++;
			return recordsCountInit;
		}
	}
}
