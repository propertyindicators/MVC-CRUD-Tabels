using System;
namespace Crud.Bll
{
	public class EmployeeModel : IHasId
	{
		public int Id { get; set; }

		public string Name { get; set; }

		public string Position { get; set; }

		public int Age { get; set; }

		public DateTime StartDate { get; set; }

		public decimal Salary { get; set; }
	}

	public class EmployeeRemovedModel : EmployeeModel, ICrudRemovedEntity
	{
		public DateTime RemovedDate { get; set; }

		public void InitWithBase(IHasId eBase)
		{
			if (!GetType().IsSubclassOf(typeof(EmployeeModel)))
				throw new InvalidCastException("");
			Id = e().Id;
			Name = e().Name;
			Position = e().Position;
			Age = e().Age;
			StartDate = e().StartDate;
			Salary = e().Salary;

			RemovedDate = DateTime.Today;

			EmployeeModel e() => (EmployeeModel)eBase;
		}
	}
}
