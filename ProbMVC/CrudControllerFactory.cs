using System;
using System.Web.Mvc;
using System.Web.Routing;
using Crud.Bll;
using ProbMVC.Controllers;
using Unity;

namespace ProbMVC
{
	public class CrudControllerFactory : DefaultControllerFactory
	{
		private readonly IUnityContainer DIContainer;

		public CrudControllerFactory(IUnityContainer diContainer)
		{
			DIContainer = diContainer;
		}

		protected override IController GetControllerInstance(RequestContext requestContext, Type controllerType)
		{
			if (controllerType == typeof(EmployeeController))
			{
				var employeeService = DIContainer.Resolve<ICrudRepository<EmployeeModel>>("EmployeeSimpleService");
				return new EmployeeController(employeeService);
			}
			else if (controllerType == typeof(EmployeeSupportRemovedController))
			{
				var employeeSupportRemovedService = DIContainer.Resolve<ICrudRepository<EmployeeModel>>(
					"EmployeeSupportRemovedAndCacheService");
				return new EmployeeSupportRemovedController(employeeSupportRemovedService);
			}
			else if (controllerType == typeof(EmployeeRemovedController))
			{
				var employeeRemovedService = DIContainer.Resolve<ICrudRemovedEntitiesService<EmployeeRemovedModel>>(
					"EmployeeRemovedSupportCacheService");
				return new EmployeeRemovedController(employeeRemovedService);
			}
			return base.GetControllerInstance(requestContext, controllerType);
		}
	}
}