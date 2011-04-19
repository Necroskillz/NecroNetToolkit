namespace NecroNet.Toolkit.Data
{
	public interface IUnitOfWorkFactory
	{
		IObjectContextFactory ContextFactory { get; }
		IObjectContext CurrentContext { get; set; }

		IUnitOfWork Create();
		void DisposeUnitOfWork(UnitOfWorkImplementor adapter);
	}
}