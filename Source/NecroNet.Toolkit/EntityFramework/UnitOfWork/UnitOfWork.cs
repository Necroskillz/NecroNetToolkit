using System;

namespace NecroNet.Toolkit.EntityFramework
{
	public static class UnitOfWork
	{
		private const string CurrentUnitOfWorkKey = "CurrentUnitOfWork.Key";
		private static IUnitOfWorkFactory _unitOfWorkFactory;

		private static IUnitOfWork CurrentUnitOfWork
		{
			get
			{
				return Local.Data[CurrentUnitOfWorkKey] as IUnitOfWork;
			}
			set
			{
				Local.Data[CurrentUnitOfWorkKey] = value;
			}
		}

		/// <summary>
		/// Gets currently active unit of work.
		/// </summary>
		public static IUnitOfWork Current
		{
			get
			{
				var unitOfWork = CurrentUnitOfWork;
				if(unitOfWork == null)
				{
					throw new InvalidOperationException("You are not in a unit of work");
				}

				return unitOfWork;
			}
		}

		/// <summary>
		/// Gets whether a unit of work has been started, and have not yet been disposed.
		/// </summary>
		public static bool IsStarted
		{
			get
			{
				return CurrentUnitOfWork != null;
			}
		}

		/// <summary>
		/// Gets current underlying object context.
		/// </summary>
		public static IObjectContext CurrentContext
		{
			get
			{
				return UnitOfWorkFactory.CurrentContext;
			}
		}

		private static IUnitOfWorkFactory UnitOfWorkFactory
		{
			get
			{
				if(_unitOfWorkFactory == null)
				{
					throw new InvalidOperationException("UnitOfWork factory was not initialized. Make sure you called UnitOfWork.Setup() method before using UnitOfWork.");
				}

				return _unitOfWorkFactory;
			}
		}

		/// <summary>
		/// Sets up unit of work with factory type that can create object context (<see cref="IObjectContextFactory"/>).
		/// </summary>
		/// <param name="objectContextFactoryType"><see cref="IObjectContextFactory"/> you want to use for creating object contexts.</param>
		public static void Setup(Type objectContextFactoryType)
		{
			if(_unitOfWorkFactory != null)
			{
				throw new InvalidOperationException("UnitOfWork is already set up.");
			}

			_unitOfWorkFactory = new UnitOfWorkFactory(objectContextFactoryType);
		}

		/// <summary>
		/// Starts a unit of work.
		/// </summary>
		/// <returns>Unit of work scope object.</returns>
		public static IUnitOfWork Start()
		{
			if(CurrentUnitOfWork != null)
			{
				throw new InvalidOperationException("You cannot start more than one unit of work at the same time.");
			}

			var unitOfWork = UnitOfWorkFactory.Create();
			CurrentUnitOfWork = unitOfWork;
			return unitOfWork;
		}

		internal static void DisposeUnitOfWork(IUnitOfWorkImplementor unitOfWork)
		{
			CurrentUnitOfWork = null;
		}
	}
}