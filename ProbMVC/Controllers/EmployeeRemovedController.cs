using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Crud.Bll;

namespace ProbMVC.Controllers
{
	public class EmployeeRemovedController : Controller
	{
		private readonly ICrudRemovedEntitiesService<EmployeeRemovedModel> EmployeeRemovedService;

		public EmployeeRemovedController(ICrudRemovedEntitiesService<EmployeeRemovedModel> r)
		{
			EmployeeRemovedService = r 
				?? throw new ArgumentNullException("Injected EmployeeRemovedService is null");
		}

		public ActionResult Index()
		{
			IEnumerable<EmployeeRemovedModel> employeesBll = EmployeeRemovedService.GetItems();
			var employeesUi = employeesBll.Select(i => new Models.EmployeeRemovedModel(i));
			return View(employeesUi);
		}

		private const string IdIsNotDefined = "Error: Removed employee ID is not defined in request";

		[HttpGet]
		public ActionResult Details(int? id)
		{
			if (id == null)
			{
				ViewBag.Error = IdIsNotDefined;
				return View();
			}
			EmployeeRemovedModel item = EmployeeRemovedService.GetItem(id.Value);
			if (item == null)
			{
				ViewBag.Error = $"Removed employee with ID={id.Value} does not exist";
				return View();
			}
			return View(new Models.EmployeeRemovedModel(item));
		}

		[HttpGet]
		public ActionResult Delete(int? id)
		{
			if (id == null)
			{
				ViewBag.Error = IdIsNotDefined;
				return View("Error");
			}
			EmployeeRemovedService.DeleteItem(id.Value);
			return RedirectToRoute(new {Action = "Index"});
		}

		[HttpGet]
		public ActionResult Revert(int? id)
		{
			if (id == null)
			{
				ViewBag.Error = IdIsNotDefined;
				return View("Error");
			}
			int newId = EmployeeRemovedService.RevertItem(id.Value);
			if (newId == 0)
			{
				ViewBag.Error = "Oops! Reverting of employee record was not successfully finished. Please request the support.";
				return View("Error");
			}
			return RedirectToRoute(new {Controller = "EmployeeSupportRemoved", Action = "Details", id = newId});
		}
	}
}