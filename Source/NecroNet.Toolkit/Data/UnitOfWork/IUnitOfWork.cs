using System;
using System.Data;

namespace NecroNet.Toolkit.Data
{
	public interface IUnitOfWork : IDisposable
	{
		/// <summary>
		/// Persists all updates to the data source.
		/// </summary>
		void Flush();

		/// <summary>
		/// Begins a transaction by creating a transaction scope.
		/// </summary>
		ITransaction BeginTransaction(IsolationLevel? isolationLevel = null);

        /// <summary>
        /// Gets underlying object context.
        /// Under normal circumstances you should not use this. If you do, make sure to abstract it away if you want your code to be unit testable.
        /// </summary>
        IObjectContext Context { get; }
	}
}