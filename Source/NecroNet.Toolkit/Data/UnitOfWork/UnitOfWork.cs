using System;
using System.Collections.Generic;

namespace NecroNet.Toolkit.Data
{
	public static class UnitOfWork
	{
		private const string UnitOfWorkStoreKey = "UnitOfWorkStore.Key";
		private static readonly Dictionary<string, IUnitOfWorkFactory> UnitOfWorkFactories = new Dictionary<string, IUnitOfWorkFactory>();

		/// <summary>
		/// Gets currently active unit of work of specified type.
		/// </summary>
		public static IUnitOfWork GetCurrent<TObjectContext>()
		{
			var unitOfWork = RetrieveUnitOfWork<TObjectContext>();
			if (unitOfWork == null)
			{
				throw new InvalidOperationException("You are not in a unit of work");
			}

			return unitOfWork;
		}

		private static void StoreUnitOfWork<TObjectContext>(IUnitOfWork unitOfWork)
		{
			UnitOfWorkStore.Add(GetKey<TObjectContext>(), unitOfWork);
		}

		private static IUnitOfWork RetrieveUnitOfWork<TObjectContext>()
		{
			var store = UnitOfWorkStore;
			var key = GetKey<TObjectContext>();

			return store.ContainsKey(key) ? store[key] : null;
		}

		private static Dictionary<string, IUnitOfWork> UnitOfWorkStore
		{
			get
			{
				var store = Local.Data[UnitOfWorkStoreKey] as Dictionary<string, IUnitOfWork>;
				if (store == null)
				{
					Local.Data[UnitOfWorkStoreKey] = store = new Dictionary<string, IUnitOfWork>();
				}

				return store;
			}
		}

		private static string GetKey<TObjectContext>()
		{
			return typeof(TObjectContext).FullName;
		}

		/// <summary>
		/// Gets whether a unit of work of specified type has been started, and have not yet been disposed.
		/// </summary>
		public static bool IsStarted<TObjectContext>()
		{
			return UnitOfWorkStore.ContainsKey(GetKey<TObjectContext>());
		}

		/// <summary>
		/// Gets current underlying object context.
		/// </summary>
		public static IObjectContext GetCurrentContext<TObjectContext>()
		{
			// TODO: add some checks
			return UnitOfWorkFactories[GetKey<TObjectContext>()].CurrentContext;
		}

		private static IUnitOfWorkFactory GetUnitOfWorkFactory<TObjectContext>()
		{
			var key = GetKey<TObjectContext>();
			if (UnitOfWorkFactories.ContainsKey(key))
			{
				throw new InvalidOperationException("UnitOfWork factory was not initialized. Make sure you called UnitOfWork.Setup() method before using UnitOfWork.");
			}

			return UnitOfWorkFactories[key];
		}

		/// <summary>
		/// Sets up unit of work with factory type that can create object context (<see cref="IObjectContextFactory"/>).
		/// </summary>
		/// <param name="objectContextFactoryType"><see cref="IObjectContextFactory"/> you want to use for creating object contexts.</param>
		public static void Register<TObjectContext>(Type objectContextFactoryType)
		{
			var key = GetKey<TObjectContext>();
			if (UnitOfWorkFactories.ContainsKey(key))
			{
				throw new InvalidOperationException("This type of UnitOfWork is already set up.");
			}

			UnitOfWorkFactories.Add(key, new UnitOfWorkFactory<TObjectContext>(objectContextFactoryType));
		}

		public static void Register<TObjectContext, TObjectContextFactory>()
		{
			Register<TObjectContext>(typeof(TObjectContextFactory));
		}

		/// <summary>
		/// Starts a unit of work of specified type.
		/// </summary>
		/// <returns>Unit of work scope object.</returns>
		public static IUnitOfWork Start<TObjectContext>()
		{
			if (IsStarted<TObjectContext>())
			{
				throw new InvalidOperationException("You cannot start more than one unit of work of the same type at the same time.");
			}

			var factory = GetUnitOfWorkFactory<TObjectContext>();
			var unitOfWork = factory.Create();
			StoreUnitOfWork<TObjectContext>(unitOfWork);

			return unitOfWork;
		}

		// TODO: whats that parameter for?
		internal static void DisposeUnitOfWork<TObjectContext>(IUnitOfWorkImplementor unitOfWork)
		{
			StoreUnitOfWork<TObjectContext>(null);
		}
	}
}