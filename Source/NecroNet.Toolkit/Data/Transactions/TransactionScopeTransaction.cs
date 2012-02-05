using System;
using System.Transactions;
using NecroNet.Toolkit.Configuration;

namespace NecroNet.Toolkit.Data
{
	public class TransactionScopeTransaction : ITransaction
	{
		private readonly TransactionScope _transactionScope;

		public TransactionScopeTransaction(TransactionScopeOption? scopeOption = null, IsolationLevel? isolationLevel = null, int? timeout = null)
		{
			var options = new TransactionOptions
			              	{
			              		IsolationLevel = isolationLevel ?? NecroNetToolkitConfigurationManager.GetOption(c => c.Transaction.IsolationLevel, IsolationLevel.ReadCommitted),
								Timeout = TimeSpan.FromMilliseconds(timeout ?? NecroNetToolkitConfigurationManager.GetOption(c => c.Transaction.Timeout))
			              	};

			_transactionScope = new TransactionScope(scopeOption ?? NecroNetToolkitConfigurationManager.GetOption(c => c.Transaction.ScopeOption), options);
		}

		public void Dispose()
		{
			_transactionScope.Dispose();
		}

		public void Complete()
		{
			_transactionScope.Complete();
		}
	}
}