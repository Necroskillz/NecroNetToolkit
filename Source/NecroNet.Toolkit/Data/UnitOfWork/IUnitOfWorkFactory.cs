namespace NecroNet.Toolkit.Data
{
	internal interface IUnitOfWorkFactory
	{
		IUnitOfWork Create();
		void DisposeUnitOfWork();
	}
}