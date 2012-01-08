using System;

namespace NecroNet.Toolkit.Data
{
	public class UnitOfWorkFactory : IUnitOfWorkFactory
	{
		private static readonly object ContextKey = new object();

		internal UnitOfWorkFactory(Type objectContextFactoryType)
		{
			ContextFactory = (IObjectContextFactory) objectContextFactoryType.GetConstructor(new Type[] {}).Invoke(null);
		}

		public IUnitOfWork Create()
		{
			var context = CreateContext();
			Local.Data[ContextKey] = context;
			return new UnitOfWorkImplementor(this, context);
		}

		public IObjectContextFactory ContextFactory { get; private set; }

		public IObjectContext CurrentContext
		{
			get
			{
				var context = Local.Data[ContextKey];
				if(context == null)
				{
					throw new InvalidOperationException("You are not in a unit of work.");
				}

				return context as IObjectContext;
			}
			set
			{
				Local.Data[ContextKey] = value;
			}
		}

		public void DisposeUnitOfWork(UnitOfWorkImplementor adapter)
		{
			CurrentContext = null;
			UnitOfWork.DisposeUnitOfWork(adapter);
		}

		private IObjectContext CreateContext()
		{
			return ContextFactory.CreateObjectContext();
		}
	}
}