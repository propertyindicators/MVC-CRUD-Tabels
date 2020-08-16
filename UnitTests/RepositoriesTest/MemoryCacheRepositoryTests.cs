using System.Threading;
using Crud.Bll;
using Crud.Dal;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTests.RepositoriesTest
{
	[TestClass]
	public class MemoryCacheRepositoryTests
	{
		[TestMethod]
		public void MemoryCacheRepositoryTest()
		{
			const int testId = 1212;
			var repositoryMemoryCache = new MemoryCacheRepository<EmployeeModel>(testing : true);
			EmployeeModel testItem = TestCrudEntity.Create();
			int initAge = testItem.Age;
			testItem.Id = testId;
			repositoryMemoryCache.SaveItem(testItem);
			EmployeeModel savedItem = repositoryMemoryCache.GetItem(testId);
			Assert.IsNotNull(savedItem);
			savedItem.Age++;
			repositoryMemoryCache.SaveItem(savedItem);
			EmployeeModel updatedItem = repositoryMemoryCache.GetItem(testId);
			Assert.IsNotNull(updatedItem);
			Assert.AreEqual(initAge + 1, updatedItem.Age);
			repositoryMemoryCache.DeleteItem(testId);
			Assert.IsNull(repositoryMemoryCache.GetItem(testId));
			repositoryMemoryCache.SaveItem(testItem);
			Thread.Sleep((MemoryCacheRepository<EmployeeModel>.TestExpirationPeriodSeconds * 1000) + 1);
			Assert.IsNull(repositoryMemoryCache.GetItem(testId));
		}
	}
}
