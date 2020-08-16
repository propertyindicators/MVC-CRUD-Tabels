using System;
using System.ComponentModel.DataAnnotations;
using Crud.Bll;

namespace ProbMVC.Models
{
	public class EmployeeRemovedModel : EmployeeModel, IConvertable<Crud.Bll.EmployeeRemovedModel>
	{
		[DataType(DataType.Date)]
		public DateTime RemovedDate { get; set; }

		public EmployeeRemovedModel() { }

		public EmployeeRemovedModel(Crud.Bll.EmployeeRemovedModel e)
		{
			InitWith(e);
		}

		new public Crud.Bll.EmployeeRemovedModel ConvertToBll()
		{
			var r = new Crud.Bll.EmployeeRemovedModel();
			r.InitWithBase(base.ConvertToBll());
			r.RemovedDate = RemovedDate;
			return r;
		}

		public void InitWith(Crud.Bll.EmployeeRemovedModel e)
		{
			base.InitWith(e);
			RemovedDate = e.RemovedDate;
		}
	}
}