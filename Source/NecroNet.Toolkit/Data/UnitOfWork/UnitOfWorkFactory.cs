using System;
using System.Collections.Generic;
using NecroNet.Toolkit.Internals;

namespace NecroNet.Toolkit.Data
{
	internal class UnitOfWorkFactory<TObjectContext> : IUnitOfWorkFactory
	{
		internal UnitOfWorkFactory(Type objectContextFactoryType)
		{
			ContextFactory = (IObjectContextFactory) Activator.CreateInstance(objectContextFactoryType);
		}

		private IObjectContext CreateContext()
		{
			return ContextFactory.CreateObjectContext();
		}

		public IUnitOfWork Create()
		{
			var context = CreateContext();
			return new UnitOfWorkImplementor(this, context);
		}

	    private IObjectContextFactory ContextFactory { get; set; }

		public void DisposeUnitOfWork()
		{
			UnitOfWork.DisposeUnitOfWork<TObjectContext>();
		}
	}
}