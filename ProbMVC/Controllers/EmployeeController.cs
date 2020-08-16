using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Crud.Bll;

namespace ProbMVC.Controllers
{
	public class EmployeeController : Controller
	{
		// Services injection
		private readonly ICrudRepository<EmployeeModel> EmployeeService;
		private const string modelsPath = "~/Views/Employee/";

		public EmployeeController(ICrudRepository<EmployeeModel> r)
		{
			EmployeeService = r ?? throw new ArgumentNullException("Injected EmployeeService is null");
			ViewBag.ControllerName = ControllerName();
			ViewBag.HasLinkToRemoved = false;
		}

		public ActionResult Index()
		{
			IEnumerable<EmployeeModel> employeesBll = EmployeeService.GetItems();
			var employeesUi = employeesBll.Select(i => new Models.EmployeeModel(i));
			return View(ViewPath("Index"), employeesUi);
		}

		[HttpGet]
		public ActionResult Edit(int? id)
		{
			Models.EmployeeModel employee;
			if (id == null)
			{
				employee = new Models.EmployeeModel() { StartDate = DateTime.Today };
				ViewBag.Action = "Create new employee";
			}
			else
			{
				EmployeeModel editItem = EmployeeService.GetItem(id.Value);
				if (editItem == null)
				{
					ViewBag.Error = DoesNotExistError(id.Value);
					return View();
				}
				employee = new Models.EmployeeModel(editItem);
				ViewBag.Action = "Edit employee data";
			}
			return View(ViewPath("Edit"), employee);
		}

		[HttpPost]
		public ActionResult Edit(Models.EmployeeModel e)
		{
			string view = ViewPath("Edit");
			if (!ModelState.IsValidField("StartDate") || e.StartDate == default(DateTime))
			{
				ModelState.AddModelError("StartDate", @"Input correct start date using ""yyyy-MM-dd"" format");
			}
			if (!ModelState.IsValid) return View(view, e);
			int res = EmployeeService.SaveItem(e.ConvertToBll());
			if (res == 0) // saving error
			{
				ViewBag.Error = "Oops! Employee data was not saved - please try again or request the support.";
				return View(view, e);
			}
			else
			{
				return RedirectToRoute(new { Controller = ControllerName(), Action = "Details", Id = res });
			}
		}

		private const string IdIsNotDefined = "Error: Employee ID is not defined in request";

		[HttpGet]
		public ActionResult Details(int? id)
		{
			const string viewName = "Details";
			if (id == null)
			{
				ViewBag.Error = IdIsNotDefined;
				return View(ViewPath(viewName));
			}
			EmployeeModel item = EmployeeService.GetItem(id.Value);
			if (item == null)
			{
				ViewBag.Error = DoesNotExistError(id.Value);
				return View(ViewPath(viewName));
			}
			return View(ViewPath(viewName), new Models.EmployeeModel(item));
		}

		[HttpGet]
		public ActionResult Delete(int? id)
		{
			if (id == null)
			{
				ViewBag.Error = IdIsNotDefined;
				return View(ViewPath("Error"));
			}
			EmployeeService.DeleteItem(id.Value);
			return RedirectToRoute(new {Controller = ControllerName(), Action = "Index"});
		}

		private string ControllerName() => GetType().Name.Replace("Controller", "");

		private string ViewPath(string actionName) => modelsPath + actionName + ".cshtml";

		private string DoesNotExistError(int id) => $"Employee with ID={id} does not exist";
	}
}
