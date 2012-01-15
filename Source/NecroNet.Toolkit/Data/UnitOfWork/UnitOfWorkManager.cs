namespace NecroNet.Toolkit.Data
{
	public class UnitOfWorkManager : IUnitOfWorkManager
	{
		public IUnitOfWork Start()
		{
			return UnitOfWork.Start();
		}

		public IUnitOfWork Start<TObjectContext>()
		{
			return UnitOfWork.Start<TObjectContext>();
		}

		public bool IsStarted()
		{
			return UnitOfWork.IsStarted();
		}

		public bool IsStarted<TObjectContext>()
		{
			return UnitOfWork.IsStarted<TObjectContext>();
		}

		public IUnitOfWork GetCurrent()
		{
			return UnitOfWork.GetCurrent();
		}

		public IUnitOfWork GetCurrent<TObjectContext>()
		{
			return UnitOfWork.GetCurrent<TObjectContext>();
		}
	}
}