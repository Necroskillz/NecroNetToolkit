using System;
using System.Collections.Generic;

namespace NecroNet.Toolkit.Data
{
	public static class UnitOfWork
	{
		private const string UnitOfWorkStoreKey = "UnitOfWorkStore.Key";
		private static readonly Dictionary<string, IUnitOfWorkFactory> UnitOfWorkFactories = new Dictionary<string, IUnitOfWorkFactory>();

		private static string DefaultKey { get; set; }

		/// <summary>
		/// Gets currently active unit of work of specified type.
		/// </summary>
		public static IUnitOfWork GetCurrent<TObjectContext>()
		{
			return GetCurrent(GetKey<TObjectContext>());
		}

		public static IUnitOfWork GetCurrent()
		{
			return GetCurrent(DefaultKey);
		}

		private static IUnitOfWork GetCurrent(string key)
		{
			var unitOfWork = RetrieveUnitOfWork(key);
			if (unitOfWork == null)
			{
				throw new InvalidOperationException("You are not in a unit of work");
			}

			return unitOfWork;
		}

		private static void StoreUnitOfWork(string key, IUnitOfWork unitOfWork)
		{
			UnitOfWorkStore.Add(key, unitOfWork);
		}

		private static IUnitOfWork RetrieveUnitOfWork(string key)
		{
			var store = UnitOfWorkStore;

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

		private static bool IsStarted(string key)
		{
			return UnitOfWorkStore.ContainsKey(key);
		}

		/// <summary>
		/// Gets whether a unit of work of specified type has been started, and have not yet been disposed.
		/// </summary>
		public static bool IsStarted<TObjectContext>()
		{
			return IsStarted(GetKey<TObjectContext>());
		}

		public static bool IsStarted()
		{
			return IsStarted(DefaultKey);
		}

		/// <summary>
		/// Gets current underlying object context.
		/// </summary>
		public static IObjectContext GetCurrentContext<TObjectContext>()
		{
			var key = GetKey<TObjectContext>();

			return GetCurrentContext(key);
		}

		public static IObjectContext GetCurrentContext()
		{
			return GetCurrentContext(DefaultKey);
		}

		private static IObjectContext GetCurrentContext(string key)
		{
			var factory = GetUnitOfWorkFactory(key);

			if (!IsStarted(key))
			{
				throw new InvalidOperationException("You are not in a unit of work");
			}

			return factory.CurrentContext;
		}

		private static IUnitOfWorkFactory GetUnitOfWorkFactory(string key)
		{
			if (!UnitOfWorkFactories.ContainsKey(key))
			{
				throw new InvalidOperationException("UnitOfWork factory was not initialized. Make sure you called UnitOfWork.Register() method before using UnitOfWork.");
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

		public static void RegisterDefault<TObjectContext>(Type objectContextFactoryType)
		{
			if(!string.IsNullOrEmpty(DefaultKey))
			{
				throw new InvalidOperationException("You cannot specify more then one default unit of work.");
			}

			var key = GetKey<TObjectContext>();
			if (UnitOfWorkFactories.ContainsKey(key))
			{
				throw new InvalidOperationException("This type of UnitOfWork is already set up.");
			}

			DefaultKey = key;
			UnitOfWorkFactories.Add(key, new UnitOfWorkFactory<TObjectContext>(objectContextFactoryType));
		}

		public static void RegisterDefault<TObjectContext, TObjectContextFactory>()
		{
			RegisterDefault<TObjectContext>(typeof(TObjectContextFactory));
		}

		/// <summary>
		/// Starts a unit of work of specified type.
		/// </summary>
		/// <returns>Unit of work scope object.</returns>
		public static IUnitOfWork Start<TObjectContext>()
		{
			return Start(GetKey<TObjectContext>());
		}

		public static IUnitOfWork Start()
		{
			if(string.IsNullOrEmpty(DefaultKey))
			{
				throw new InvalidOperationException("Default UnitOfWork factory was not initialized. Make sure you called UnitOfWork.RegisterDefault() method before using default UnitOfWork.");
			}

			return Start(DefaultKey);
		}

		private static IUnitOfWork Start(string key)
		{
			if (IsStarted(key))
			{
				throw new InvalidOperationException("You cannot start more than one unit of work of the same type at the same time.");
			}

			var factory = GetUnitOfWorkFactory(key);
			var unitOfWork = factory.Create();
			StoreUnitOfWork(key, unitOfWork);

			return unitOfWork;
		}

		// TODO: whats that parameter for?
		internal static void DisposeUnitOfWork<TObjectContext>()
		{
			UnitOfWorkStore.Remove(GetKey<TObjectContext>());
		}
	}
}