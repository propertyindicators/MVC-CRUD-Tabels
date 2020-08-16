namespace Crud.Bll
{
	public interface IConvertable<T> where T : IHasId
	{
		void InitWith(T modelBll);
		T ConvertToBll();
	}
}
