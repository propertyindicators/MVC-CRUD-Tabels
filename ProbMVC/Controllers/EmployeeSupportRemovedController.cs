using Crud.Bll;

namespace ProbMVC.Controllers
{
	public class EmployeeSupportRemovedController : EmployeeController
	{
		public EmployeeSupportRemovedController(ICrudRepository<EmployeeModel> r) : base(r)
		{
			ViewBag.HasLinkToRemoved = true;
		}
	}
}
