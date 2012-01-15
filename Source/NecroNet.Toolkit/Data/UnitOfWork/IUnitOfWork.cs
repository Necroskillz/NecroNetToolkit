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
        /// Gets underlying object context
        /// </summary>
        IObjectContext Context { get; }
	}
}