using MvcTurbine;
using MvcTurbine.Blades;
using NecroNet.Toolkit.Data;
using NecroNetToolkit.Web.Model;

namespace NecroNetToolkit.Web.Test.Blades
{
	public class ApplicationBlade : Blade
	{
		public override void Spin(IRotorContext context)
		{
			UnitOfWork.Register<AllDealsEntities, AllDealsEntitiesFactory>();
			UnitOfWork.Register<DoplatkyEntities, DoplatkyEntitiesFactory>();
		}
	}
}