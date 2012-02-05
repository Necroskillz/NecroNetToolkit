using System;
using System.Linq;
using MvcTurbine.Ninject;
using NecroNet.Toolkit.Data;
using NecroNetToolkit.Web.Model;
using Ninject;
using Ninject.Extensions.Conventions;

namespace NecroNetToolkit.Web.Test.Registration
{
	public class NinjectRegistration : NinjectRegistrator
	{
		public override void Register(IKernel kernel)
		{
			kernel.Bind<IUnitOfWorkManager>().To<UnitOfWorkManager>().InSingletonScope();
			kernel.Bind<ITransactionFactory>().To<TransactionFactory>().InSingletonScope();

			kernel.Load<TestNinjectModule>();

			//kernel.Scan(s =>
			//                {
			//                    s.FromAssembliesMatching("*Web.Model*");
			//                    s.WhereTypeInheritsFromGeneric(typeof(IRepository<>));
			//                    s.BindWith(new GenericBindingGenerator(typeof(IRepository<>)));
			//                    s.BindWithDefaultConventions();
			//                    s.InSingletonScope();
			//                });
		}
	}

	public static class AssemblyScannerExtensions
	{
		public static void WhereTypeInheritsFromGeneric(this AssemblyScanner scanner, Type type)
		{
			scanner.Where(t => t.GetInterfaces().Any(i => i.IsGenericType && i.GetGenericTypeDefinition() == type));
		}
	}
}