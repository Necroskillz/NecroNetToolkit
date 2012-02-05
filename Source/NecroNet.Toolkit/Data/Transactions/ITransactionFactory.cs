using System.Transactions;

namespace NecroNet.Toolkit.Data
{
	public interface ITransactionFactory
	{
		/// <summary>
		/// Makes a code block transactional.
		/// </summary>
		/// <param name="scopeOption">An instance of the <see cref="T:System.Transactions.TransactionScopeOption"/> enumeration that describes the transaction requirements associated with this transaction scope.</param>
		/// <param name="isolationLevel">the isolation level of the transaction.</param>
		/// <param name="timeout">The timeout period for the transaction.</param>
		ITransaction BeginTransaction(TransactionScopeOption? scopeOption = null, IsolationLevel? isolationLevel = null, int? timeout = null);
	}
}