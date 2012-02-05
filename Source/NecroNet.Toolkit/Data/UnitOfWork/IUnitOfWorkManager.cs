namespace NecroNet.Toolkit.Data
{
	public interface IUnitOfWorkManager
	{
		/// <summary>
		/// Starts default unit of work.
		/// </summary>
		IUnitOfWork Start();

		/// <summary>
		/// Starts a unit of work of the specified type.
		/// </summary>
		/// <typeparam name="TObjectContext">The type of the object context.</typeparam>
		IUnitOfWork Start<TObjectContext>();

		/// <summary>
		/// Gets whether default unit of work has been started, and have not yet been disposed.
		/// </summary>
		bool IsStarted();

		/// <summary>
		/// Gets whether a unit of work of the specified type has been started, and have not yet been disposed.
		/// </summary>
		/// <typeparam name="TObjectContext">The type of the object context.</typeparam>
		bool IsStarted<TObjectContext>();

		/// <summary>
		/// Gets currently active default unit of work.
		/// </summary>
		IUnitOfWork GetCurrent();

		/// <summary>
		/// Gets currently active unit of work of the specified type.
		/// </summary>
		/// <typeparam name="TObjectContext">The type of the object context.</typeparam>
		IUnitOfWork GetCurrent<TObjectContext>();
	}
}