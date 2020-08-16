using System;
using System.ComponentModel.DataAnnotations.Schema;
using Crud.Bll;

namespace Crud.Dal
{
	/// <summary>
	/// Employee EntityFramework recordset 
	/// </summary>
	[Table("Employees")]
	public class Employee : IHasId, IConvertable<EmployeeModel>
	{
		public int Id { get; set; }

		public string Name { get; set; }

		public string Position { get; set; }

		public int Age { get; set; }

		[Column(TypeName = "DateTime2")]
		public DateTime StartDate { get; set; }

		[Column(TypeName = "Money")]
		public decimal Salary { get; set; }

		public EmployeeModel ConvertToBll()
		{
			return new EmployeeModel
			{
				Id = Id,
				Name = Name,
				Position = Position,
				Age = Age,
				StartDate = StartDate,
				Salary = Salary
			};
		}

		public void InitWith(EmployeeModel e)
		{
			Id = e.Id;
			Name = e.Name;
			Position = e.Position;
			Age = e.Age;
			StartDate = e.StartDate;
			Salary = e.Salary;
		}
	}
}