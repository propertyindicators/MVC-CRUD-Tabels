using System;
using System.ComponentModel.DataAnnotations;
using Crud.Bll;

namespace ProbMVC.Models
{
	public class EmployeeModel : IHasId, IConvertable<Crud.Bll.EmployeeModel>
	{
		public int Id { get; set; }

		[Required]
		public string Name { get; set; }

		[Required]
		public string Position { get; set; }

		[Range(15, 80)]
		public int Age { get; set; }

		[DataType(DataType.Date)]
		[Display(Name = "Start Date")]
		public DateTime StartDate { get; set; }

		[Range(10, 100000)]
		public decimal Salary { get; set; }

		public EmployeeModel() { }

		public EmployeeModel(Crud.Bll.EmployeeModel e)
		{
			InitWith(e);
		}

		public Crud.Bll.EmployeeModel ConvertToBll()
		{
			return new Crud.Bll.EmployeeModel()
			{
				Id = Id,
				Name = Name,
				Position = Position,
				Age = Age,
				StartDate = StartDate,
				Salary = Salary
			};
		}

		public virtual void InitWith(Crud.Bll.EmployeeModel e)
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