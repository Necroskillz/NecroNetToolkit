using System;
using System.Collections.Generic;
using NecroNet.Toolkit.Internals;

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
			var store = ObjectContextStore;
			var key = GetKey();

			if(store.ContainsKey(key))
			{
				store[key] = objectContext;
			}
			else
			{
				store.Add(GetKey(), objectContext);
			}
		}

		private static IObjectContext RetrieveObjectContext()
		{
			var store = ObjectContextStore;
			var key = GetKey();

			return store.ContainsKey(key) ? store[key] : null;
		}

		private IObjectContext CreateContext()
		{
			return ContextFactory.CreateObjectContext();
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
					ExceptionHelper.ThrowInvalidOp("You are not in a unit of work of type '{0}'.", GetKey());
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
			StoreObjectContext(null);

			UnitOfWork.DisposeUnitOfWork<TObjectContext>();
		}
	}
}