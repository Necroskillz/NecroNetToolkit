using System;
using System.Configuration;
using System.Transactions;
using NecroNet.Toolkit.Configuration;

namespace NecroNet.Toolkit.Data
{
	public class GenericTransaction : ITransaction
	{
		protected TransactionScope _transactionScope;

		public GenericTransaction(TransactionScopeOption? scopeOption = null, IsolationLevel? isolationLevel = null, int? timeout = null)
		{
			TransactionScopeOption scopeOptionActual;
			IsolationLevel isolationLevelActual;
			TimeSpan timeoutActual;

			if (NecroNetToolkitConfigurationManager.Configuration != null)
			{
				var transactionConfig = NecroNetToolkitConfigurationManager.Configuration.UnitOfWork.Transaction;
				scopeOptionActual = scopeOption ?? transactionConfig.ScopeOption;
				isolationLevelActual = isolationLevel ?? transactionConfig.IsolationLevel;
				timeoutActual = timeout.HasValue ? TimeSpan.FromMilliseconds(timeout.Value) : TimeSpan.FromMilliseconds(transactionConfig.Timeout);
			}
			else
			{
				scopeOptionActual = scopeOption ?? default(TransactionScopeOption);
				isolationLevelActual = isolationLevel ?? default(IsolationLevel);
				timeoutActual = timeout.HasValue ? TimeSpan.FromMilliseconds(timeout.Value) : default(TimeSpan);
			}

			var options = new TransactionOptions {IsolationLevel = isolationLevelActual, Timeout = timeoutActual};
			_transactionScope = new TransactionScope(scopeOptionActual, options);
		}

		public void Dispose()
		{
			_transactionScope.Dispose();
		}

		public void Commit()
		{
			_transactionScope.Complete();
		}
	}
}