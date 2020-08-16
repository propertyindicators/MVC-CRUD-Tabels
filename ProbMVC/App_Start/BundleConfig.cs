using System.Web.Optimization;

namespace ProbMVC
{
	public class BundleConfig
	{
		//Дополнительные сведения об объединении см. по адресу: http://go.microsoft.com/fwlink/?LinkId=301862
		public static void RegisterBundles(BundleCollection bundles)
		{
			bundles.Add(new ScriptBundle("~/bundles/jquery").Include("~/Scripts/jquery-{version}.js"));

			bundles.Add(new ScriptBundle("~/bundles/dataTables").Include("~/Scripts/jquery.dataTables.min.js"));

			bundles.Add(new ScriptBundle("~/bundles/jqueryValidate").Include("~/Scripts/jquery.validate*"));

			bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include("~/Scripts/bootstrap.js"));

			bundles.Add(new ScriptBundle("~/bundles/respond").Include("~/Scripts/respond.js"));

			bundles.Add(new ScriptBundle("~/bundles/plaintabinit").Include("~/Scripts/plaintabinit.js"));

			bundles.Add(new ScriptBundle("~/bundles/bootstrap-datepicker").Include("~/Scripts/bootstrap-datepicker.js"));

			bundles.Add(new ScriptBundle("~/bundles/formwithdateinit").Include("~/Scripts/formwithdateinit.js"));

			// Используйте версию Modernizr для разработчиков, чтобы учиться работать. Когда вы будете готовы перейти к работе,
			// используйте средство сборки на сайте http://modernizr.com, чтобы выбрать только нужные тесты.
			bundles.Add(new ScriptBundle("~/bundles/modernizr").Include("~/Scripts/modernizr-*"));

			// Styles
			bundles.Add(new StyleBundle("~/Content/site").Include("~/Content/site.css"));

			bundles.Add(new StyleBundle("~/Content/bootstrap").Include(
				"~/Content/bootstrap.css",
				"~/Content/bootstrap-theme.css"));

			bundles.Add(new StyleBundle("~/Content/bootstrap-datepicker").Include(
				"~/Content/bootstrap-datepicker3.css"));

			bundles.Add(new StyleBundle("~/Content/dataTables").Include("~/Content/jquery.dataTables.min.css"));
		}
	}
}
