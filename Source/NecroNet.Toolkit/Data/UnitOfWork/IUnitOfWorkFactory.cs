namespace NecroNet.Toolkit.Data
{
	public interface IUnitOfWorkFactory
	{
		IUnitOfWork Create();
		void DisposeUnitOfWork();
	}
}