using System;
using System.Configuration;
using System.Globalization;
using System.Threading;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using CrudDIContainers;
using Unity;

namespace ProbMVC
{
	public class MvcApplication : System.Web.HttpApplication
	{
		private static readonly IUnityContainer EmployeeServicesDIContainer =
			DIContainersBootstraper.CrudEmployee(
				ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);

		protected void Application_Start()
		{
			AreaRegistration.RegisterAllAreas();
			FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
			RouteConfig.RegisterRoutes(RouteTable.Routes);
			BundleConfig.RegisterBundles(BundleTable.Bundles);
			var controllerFactory = new CrudControllerFactory(EmployeeServicesDIContainer);
			ControllerBuilder.Current.SetControllerFactory(controllerFactory);
		}

		protected void Application_BeginRequest(object sender, EventArgs e)
		{
			CultureInfo  newCultureInfo = new CultureInfo("en-US");
			newCultureInfo.DateTimeFormat.ShortDatePattern = "yyyy-MM-dd";
			newCultureInfo.DateTimeFormat.DateSeparator = "-";
			Thread.CurrentThread.CurrentCulture = newCultureInfo;
		}

		protected void Application_End()
		{
			EmployeeServicesDIContainer.Dispose();
		}
	}
}
