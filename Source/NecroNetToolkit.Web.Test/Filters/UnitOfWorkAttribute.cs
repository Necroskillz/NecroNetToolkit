using System.Web.Mvc;
using NecroNet.Toolkit.Data;
using NecroNetToolkit.Web.Model;

namespace NecroNetToolkit.Web.Test.Filters
{
	public class UnitOfWorkAttribute : ActionFilterAttribute
	{
		private readonly IUnitOfWorkManager _unitOfWorkManager;

		public UnitOfWorkAttribute(IUnitOfWorkManager unitOfWorkManager)
		{
			_unitOfWorkManager = unitOfWorkManager;
		}

		public override void OnActionExecuting(ActionExecutingContext filterContext)
		{
			if(!filterContext.IsChildAction)
			{
				_unitOfWorkManager.Start<AllDealsEntities>();
				_unitOfWorkManager.Start<DoplatkyEntities>();
			}
		}

		public override void OnResultExecuted(ResultExecutedContext filterContext)
		{
			_unitOfWorkManager.GetCurrent<AllDealsEntities>().Dispose();
			_unitOfWorkManager.GetCurrent<DoplatkyEntities>().Dispose();
		}
	}
}