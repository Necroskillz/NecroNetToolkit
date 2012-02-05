using System.Transactions;

namespace NecroNet.Toolkit.Data
{
	public class TransactionFactory : ITransactionFactory
	{
		public ITransaction BeginTransaction(TransactionScopeOption? scopeOption, IsolationLevel? isolationLevel, int? timeout)
		{
			return new TransactionScopeTransaction(scopeOption, isolationLevel, timeout);
		}
	}
}