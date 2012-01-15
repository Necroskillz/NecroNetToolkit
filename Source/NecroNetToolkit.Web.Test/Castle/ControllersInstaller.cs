using System.Web.Mvc;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using NecroNet.Toolkit.Data;

namespace NecroNetToolkit.Web.Test
{
	public class ControllersInstaller : IWindsorInstaller
	{
		public void Install(IWindsorContainer container, IConfigurationStore store)
		{
			container.Register(Classes.FromThisAssembly()
								.BasedOn<IController>()
								.LifestyleTransient());

			container.Register(Component.For<IUnitOfWorkManager>().ImplementedBy<UnitOfWorkManager>().LifestyleSingleton());
		}
	}
}