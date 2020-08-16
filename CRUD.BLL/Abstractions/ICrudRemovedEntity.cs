using System;

namespace Crud.Bll
{
	public interface ICrudRemovedEntity : IHasId
	{
		DateTime RemovedDate { get; set; }

		void InitWithBase(IHasId init);
	}
}
