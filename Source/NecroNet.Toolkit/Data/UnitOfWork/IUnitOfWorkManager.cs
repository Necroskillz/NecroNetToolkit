namespace NecroNet.Toolkit.Data
{
	public interface IUnitOfWorkManager
	{
		IUnitOfWork Start();
		IUnitOfWork Start<TObjectContext>();

		bool IsStarted();
		bool IsStarted<TObjectContext>();

		IUnitOfWork GetCurrent();
		IUnitOfWork GetCurrent<TObjectContext>();
	}
}