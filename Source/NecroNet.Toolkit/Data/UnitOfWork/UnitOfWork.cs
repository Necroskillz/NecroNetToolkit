using System;
using System.Collections.Generic;
using NecroNet.Toolkit.Http;
using NecroNet.Toolkit.Resources;

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
				var store = Local.Data.Get<Dictionary<string, IUnitOfWork>>(UnitOfWorkStoreKey);
				if (store == null)
				{
					store = new Dictionary<string, IUnitOfWork>();
					Local.Data.Set(UnitOfWorkStoreKey, store);
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
			if(!UnitOfWorkFactories.ContainsKey(key))
			{
				Throw.New<InvalidOperationException>(Res.ExceptionMessage_UnitOfWork_NotRegistered, key);
			}

			return UnitOfWorkFactories[key];
		}

		/// <summary>
		/// Registers unit of work for the specified type of object context with factory type that can create object context (<see cref="IObjectContextFactory"/>).
		/// </summary>
		/// <typeparam name="TObjectContext">Type of object context created by factory and used by unit of work</typeparam>
		/// <param name="objectContextFactoryType">Type of <see cref="IObjectContextFactory"/> used for creating object contexts.</param>
		public static void Register<TObjectContext>(Type objectContextFactoryType)
		{
			var key = GetKey<TObjectContext>();

			if (UnitOfWorkFactories.ContainsKey(key))
			{
				Throw.New<InvalidOperationException>(Res.ExceptionMessage_UnitOfWork_AlreadyRegistered, key);
			}

			UnitOfWorkFactories.Add(key, new UnitOfWorkFactory<TObjectContext>(objectContextFactoryType));
		}

		/// <summary>
		/// Registers unit of work for the specified type of object context with factory type that can create object context (<see cref="IObjectContextFactory"/>).
		/// </summary>
		/// <typeparam name="TObjectContext">Type of object context created by factory and used by unit of work</typeparam>
		/// <typeparam name="TObjectContextFactory">Type of <see cref="IObjectContextFactory"/> used for creating object contexts.</typeparam>
		public static void Register<TObjectContext, TObjectContextFactory>()
		{
			Register<TObjectContext>(typeof(TObjectContextFactory));
		}

		/// <summary>
		/// Registers default unit of work for the specified type of object context with factory type that can create object context (<see cref="IObjectContextFactory"/>).
		/// </summary>
		/// <typeparam name="TObjectContext">Type of object context created by factory and used by unit of work</typeparam>
		/// <param name="objectContextFactoryType">Type of <see cref="IObjectContextFactory"/> used for creating object contexts.</param>
		public static void RegisterDefault<TObjectContext>(Type objectContextFactoryType)
		{
			if(!string.IsNullOrEmpty(DefaultKey))
			{
				Throw.New<InvalidOperationException>(Res.ExceptionMessage_UnitOfWork_RegisterMoreThanOneDefault);
			}

			var key = GetKey<TObjectContext>();

			if(UnitOfWorkFactories.ContainsKey(key))
			{
				Throw.New<InvalidOperationException>(Res.ExceptionMessage_UnitOfWork_AlreadyRegistered);
			}

			DefaultKey = key;
			UnitOfWorkFactories.Add(key, new UnitOfWorkFactory<TObjectContext>(objectContextFactoryType));
		}

		/// <summary>
		/// Registers default unit of work for the specified type of object context with factory type that can create object context (<see cref="IObjectContextFactory"/>).
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

			if(unitOfWork == null)
			{
				Throw.New<InvalidOperationException>(Res.ExceptionMessage_UnitOfWork_NotInUnitOfWork, key);
			}

			return unitOfWork;
		}

		/// <summary>
		/// Gets currently active unit of work of the specified type.
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
		/// Gets whether a unit of work of the specified type has been started, and have not yet been disposed.
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
			if(IsStarted(key))
			{
				Throw.New<InvalidOperationException>(Res.ExceptionMessage_UnitOfWork_StartSecondAtTheSameTime);
			}

			var factory = GetUnitOfWorkFactory(key);
			var unitOfWork = factory.Create();
			StoreUnitOfWork(key, unitOfWork);

			return unitOfWork;
		}

		/// <summary>
		/// Starts a unit of work of the specified type.
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
				Throw.New<InvalidOperationException>(Res.ExceptionMessage_UnitOfWork_DefaultNotRegistered);
			}

			return Start(DefaultKey);
		}

		internal static void DisposeUnitOfWork<TObjectContext>()
		{
			UnitOfWorkStore.Remove(GetKey<TObjectContext>());
		}
	}
}