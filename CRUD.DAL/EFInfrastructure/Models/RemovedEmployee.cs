using System;
using System.ComponentModel.DataAnnotations.Schema;
using Crud.Bll;

namespace Crud.Dal
{
	/// <summary>
	/// Entity of removed Employee recordset
	/// </summary>
	[Table("RemovedEmployees")]
	public class EmployeeRemoved : IHasId, IConvertable<EmployeeRemovedModel>
	{
		public int Id { get; set; }

		public string Name { get; set; }

		public string Position { get; set; }

		public int Age { get; set; }

		[Column(TypeName = "DateTime2")]
		public DateTime StartDate { get; set; }

		[Column(TypeName = "Money")]
		public decimal Salary { get; set; }

		public DateTime RemovedDate { get; set; } = DateTime.Now;


		public EmployeeRemovedModel ConvertToBll()
		{
			return new EmployeeRemovedModel
			{
				Id = Id,
				Name = Name,
				Position = Position,
				Age = Age,
				StartDate = StartDate,
				Salary = Salary,
				RemovedDate = RemovedDate
			};
		}

		public void InitWith(EmployeeRemovedModel e)
		{
			Id = e.Id;
			Name = e.Name;
			Position = e.Position;
			Age = e.Age;
			StartDate = e.StartDate;
			Salary = e.Salary;
			RemovedDate = e.RemovedDate;
		}
	}
}