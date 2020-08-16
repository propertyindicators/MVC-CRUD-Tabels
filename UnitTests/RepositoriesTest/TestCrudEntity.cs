using System;
using Crud.Bll;

namespace UnitTests.RepositoriesTest
{
	public static class TestCrudEntity
	{
		public static EmployeeModel Create() => new EmployeeModel
			{
				Id = 0,
				Name = "Test name",
				Age = 30,
				Position = "Test position",
				Salary = 1000,
				StartDate = DateTime.Today
			};
	}
}
