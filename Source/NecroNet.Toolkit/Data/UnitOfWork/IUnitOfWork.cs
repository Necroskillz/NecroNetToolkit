using System;
using System.Transactions;

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
		ITransaction BeginTransaction(TransactionScopeOption? scopeOption = null, IsolationLevel? isolationLevel = null, int? timeout = null);
	}
}