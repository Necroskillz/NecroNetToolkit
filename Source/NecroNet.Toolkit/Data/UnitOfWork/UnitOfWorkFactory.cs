using System;
using System.Collections.Generic;

namespace NecroNet.Toolkit.Data
{
	public class UnitOfWorkFactory<TObjectContext> : IUnitOfWorkFactory
	{
		internal UnitOfWorkFactory(Type objectContextFactoryType)
		{
			ContextFactory = (IObjectContextFactory) Activator.CreateInstance(objectContextFactoryType);
		}

		private const string ObjectContextStoreKey = "ObjectContextStore.Key";

		private static Dictionary<string, IObjectContext> ObjectContextStore
		{
			get
			{
				var store = Local.Data[ObjectContextStoreKey] as Dictionary<string, IObjectContext>;
				if (store == null)
				{
					Local.Data[ObjectContextStoreKey] = store = new Dictionary<string, IObjectContext>();
				}

				return store;
			}
		}

		private static string GetKey()
		{
			return typeof(TObjectContext).FullName;
		}

		private static void StoreObjectContext(IObjectContext objectContext)
		{
			ObjectContextStore.Add(GetKey(), objectContext);
		}

		private static IObjectContext RetrieveObjectContext()
		{
			var store = ObjectContextStore;
			var key = GetKey();

			return store.ContainsKey(key) ? store[key] : null;
		}

		public IUnitOfWork Create()
		{
			var context = CreateContext();
			StoreObjectContext(context);
			return new UnitOfWorkImplementor(this, context);
		}

		public IObjectContextFactory ContextFactory { get; private set; }

		public IObjectContext CurrentContext
		{
			get
			{
				var context = RetrieveObjectContext();
				if(context == null)
				{
					throw new InvalidOperationException(string.Format("You are not in a unit of work of type {0}.", GetKey()));
				}

				return context;
			}
			set
			{
				StoreObjectContext(value);
			}
		}

		public void DisposeUnitOfWork(UnitOfWorkImplementor adapter)
		{
			CurrentContext = null;
			UnitOfWork.DisposeUnitOfWork<TObjectContext>(adapter);
		}

		private IObjectContext CreateContext()
		{
			return ContextFactory.CreateObjectContext();
		}
	}
}