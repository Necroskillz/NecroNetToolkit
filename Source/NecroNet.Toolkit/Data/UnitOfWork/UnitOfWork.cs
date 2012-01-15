using System;
using System.Collections.Generic;
using NecroNet.Toolkit.Internals;

namespace NecroNet.Toolkit.Data
{
	public static class UnitOfWork
	{
		private static string DefaultKey { get; set; }

		private static string GetKey<TObjectContext>()
		{
			return typeof(TObjectContext).FullName;
		}

		private const string UnitOfWorkStoreKey = "UnitOfWorkStore.Key";
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

		private static void StoreUnitOfWork(string key, IUnitOfWork unitOfWork)
		{
			UnitOfWorkStore.Add(key, unitOfWork);
		}

		private static IUnitOfWork RetrieveUnitOfWork(string key)
		{
			var store = UnitOfWorkStore;

			return store.ContainsKey(key) ? store[key] : null;
		}

		private static readonly Dictionary<string, IUnitOfWorkFactory> UnitOfWorkFactories = new Dictionary<string, IUnitOfWorkFactory>();

		private static IUnitOfWorkFactory GetUnitOfWorkFactory(string key)
		{
			if (!UnitOfWorkFactories.ContainsKey(key))
			{
				ExceptionHelper.ThrowInvalidOp("UnitOfWork factory for type '{0}' was not initialized. Make sure you called UnitOfWork.Register() method before using UnitOfWork.", key);
			}

			return UnitOfWorkFactories[key];
		}

		/// <summary>
		/// Registers unit of work for specified type of object context with factory type that can create object context (<see cref="IObjectContextFactory"/>).
		/// </summary>
		/// <typeparam name="TObjectContext">Type of object context created by factory and used by unit of work</typeparam>
		/// <param name="objectContextFactoryType">Type of <see cref="IObjectContextFactory"/> used for creating object contexts.</param>
		public static void Register<TObjectContext>(Type objectContextFactoryType)
		{
			var key = GetKey<TObjectContext>();
			if (UnitOfWorkFactories.ContainsKey(key))
			{
				ExceptionHelper.ThrowInvalidOp("UnitOfWork for type '{0}' was already registered.", key);
			}

			UnitOfWorkFactories.Add(key, new UnitOfWorkFactory<TObjectContext>(objectContextFactoryType));
		}

		/// <summary>
		/// Registers unit of work for specified type of object context with factory type that can create object context (<see cref="IObjectContextFactory"/>).
		/// </summary>
		/// <typeparam name="TObjectContext">Type of object context created by factory and used by unit of work</typeparam>
		/// <typeparam name="TObjectContextFactory">Type of <see cref="IObjectContextFactory"/> used for creating object contexts.</typeparam>
		public static void Register<TObjectContext, TObjectContextFactory>()
		{
			Register<TObjectContext>(typeof(TObjectContextFactory));
		}

		/// <summary>
		/// Registers default unit of work for specified type of object context with factory type that can create object context (<see cref="IObjectContextFactory"/>).
		/// </summary>
		/// <typeparam name="TObjectContext">Type of object context created by factory and used by unit of work</typeparam>
		/// <param name="objectContextFactoryType">Type of <see cref="IObjectContextFactory"/> used for creating object contexts.</param>
		public static void RegisterDefault<TObjectContext>(Type objectContextFactoryType)
		{
			if (!string.IsNullOrEmpty(DefaultKey))
			{
				ExceptionHelper.ThrowInvalidOp("You cannot specify more then one default unit of work.");
			}

			var key = GetKey<TObjectContext>();
			if (UnitOfWorkFactories.ContainsKey(key))
			{
				ExceptionHelper.ThrowInvalidOp("UnitOfWork for type '{0}' was already registered.", key);
			}

			DefaultKey = key;
			UnitOfWorkFactories.Add(key, new UnitOfWorkFactory<TObjectContext>(objectContextFactoryType));
		}

		/// <summary>
		/// Registers default unit of work for specified type of object context with factory type that can create object context (<see cref="IObjectContextFactory"/>).
		/// </summary>
		/// <typeparam name="TObjectContext">Type of object context created by factory and used by unit of work</typeparam>
		/// <typeparam name="TObjectContextFactory">Type of <see cref="IObjectContextFactory"/> used for creating object contexts.</typeparam>
		public static void RegisterDefault<TObjectContext, TObjectContextFactory>()
		{
			RegisterDefault<TObjectContext>(typeof(TObjectContextFactory));
		}

		private static IUnitOfWork GetCurrent(string key)
		{
			var unitOfWork = RetrieveUnitOfWork(key);
			if (unitOfWork == null)
			{
				ExceptionHelper.ThrowInvalidOp("You are not in a unit of work of type '{0}'.", key);
			}

			return unitOfWork;
		}

		/// <summary>
		/// Gets currently active unit of work of specified type.
		/// </summary>
		/// <typeparam name="TObjectContext">Type of object context</typeparam>
		public static IUnitOfWork GetCurrent<TObjectContext>()
		{
			return GetCurrent(GetKey<TObjectContext>());
		}

		/// <summary>
		/// Gets currently active default unit of work.
		/// </summary>
		public static IUnitOfWork GetCurrent()
		{
			return GetCurrent(DefaultKey);
		}

		private static bool IsStarted(string key)
		{
			return UnitOfWorkStore.ContainsKey(key);
		}

		/// <summary>
		/// Gets whether a unit of work of specified type has been started, and have not yet been disposed.
		/// </summary>
		/// <typeparam name="TObjectContext">Type of object context</typeparam>
		public static bool IsStarted<TObjectContext>()
		{
			return IsStarted(GetKey<TObjectContext>());
		}

		/// <summary>
		/// Gets whether default unit of work has been started, and have not yet been disposed.
		/// </summary>
		public static bool IsStarted()
		{
			return IsStarted(DefaultKey);
		}

		private static IUnitOfWork Start(string key)
		{
			if (IsStarted(key))
			{
				ExceptionHelper.ThrowInvalidOp("You cannot start more than one unit of work of the same type at the same time.");
			}

			var factory = GetUnitOfWorkFactory(key);
			var unitOfWork = factory.Create();
			StoreUnitOfWork(key, unitOfWork);

			return unitOfWork;
		}

		/// <summary>
		/// Starts a unit of work of specified type.
		/// </summary>
		/// <typeparam name="TObjectContext">Type of object context</typeparam>
		public static IUnitOfWork Start<TObjectContext>()
		{
			return Start(GetKey<TObjectContext>());
		}

		/// <summary>
		/// Starts default unit of work.
		/// </summary>
		public static IUnitOfWork Start()
		{
			if(string.IsNullOrEmpty(DefaultKey))
			{
				ExceptionHelper.ThrowInvalidOp("Default UnitOfWork factory was not initialized. Make sure you called UnitOfWork.RegisterDefault() method before using default UnitOfWork.");
			}

			return Start(DefaultKey);
		}

		internal static void DisposeUnitOfWork<TObjectContext>()
		{
			UnitOfWorkStore.Remove(GetKey<TObjectContext>());
		}
	}
}