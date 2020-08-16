using Crud.Bll;
using Crud.Dal;
using Unity;
using Unity.Injection;

namespace CrudDIContainers
{
	public static class DIContainersBootstraper
	{
		public static IUnityContainer CrudEmployee(string connectionString)
		{
			var c = new UnityContainer();
			c.RegisterType<ICrudRepository<EmployeeModel>, MemoryCacheRepository<EmployeeModel>>(
				"EmployeeMainCacheRepository");
			c.RegisterType<ICrudRepository<EmployeeRemovedModel>, MemoryCacheRepository<EmployeeRemovedModel>>(
				"EmployeeRemovedCacheRepository");
			c.RegisterType<ICrudRepository<EmployeeModel>, EntityFrameworkRepository<EmployeeModel, Employee>>(
				"EmployeeMainRepositoryEF",
				new InjectionConstructor(connectionString));
			c.RegisterType<ICrudRepository<EmployeeModel>, DapperRepository<EmployeeModel>>(
				"EmployeeMainRepositoryDapper",
				new InjectionConstructor(connectionString, "Employees"));
			c.RegisterType<ICrudRepository<EmployeeRemovedModel>, DapperRepository<EmployeeRemovedModel>>(
				"EmployeeRemovedRepository",
				new InjectionConstructor(connectionString, "RemovedEmployees"));
			c.RegisterType<ICrudRepository<EmployeeModel>, CrudServiceBase<EmployeeModel>>(
				"EmployeeSimpleService",
				new InjectionConstructor(
					new ResolvedParameter<ICrudRepository<EmployeeModel>>("EmployeeMainRepositoryEF")));
			c.RegisterType<ICrudRepository<EmployeeModel>, CrudServiceSupportCache<EmployeeModel>>(
				"EmployeeMainSupportCacheService",
				new InjectionConstructor(
					new ResolvedParameter<ICrudRepository<EmployeeModel>>("EmployeeMainRepositoryDapper"),
					new ResolvedParameter<ICrudRepository<EmployeeModel>>("EmployeeMainCacheRepository")));
			c.RegisterType<ICrudRepository<EmployeeRemovedModel>, CrudServiceSupportCache<EmployeeRemovedModel>>(
				"EmployeeRemovedSupportCacheService",
				new InjectionConstructor(
					new ResolvedParameter<ICrudRepository<EmployeeRemovedModel>>("EmployeeRemovedRepository"),
					new ResolvedParameter<ICrudRepository<EmployeeRemovedModel>>("EmployeeRemovedCacheRepository")));
			c.RegisterType<ICrudRepository<EmployeeModel>, CrudServiceSupportRemoved<EmployeeModel, EmployeeRemovedModel>>(
				"EmployeeSupportRemovedAndCacheService",
				new InjectionConstructor(
					new ResolvedParameter<ICrudRepository<EmployeeModel>>("EmployeeMainSupportCacheService"),
					new ResolvedParameter<ICrudRepository<EmployeeRemovedModel>>("EmployeeRemovedSupportCacheService")));
			c.RegisterType<ICrudRemovedEntitiesService<EmployeeRemovedModel>,
				CrudServiceRemovedEntities<EmployeeRemovedModel, EmployeeModel>>(
				"EmployeeRemovedSupportCacheService",
				new InjectionConstructor(
					new ResolvedParameter<ICrudRepository<EmployeeRemovedModel>>("EmployeeRemovedSupportCacheService"),
					new ResolvedParameter<ICrudRepository<EmployeeModel>>("EmployeeMainSupportCacheService")));
			return c;
		}
	}
}
