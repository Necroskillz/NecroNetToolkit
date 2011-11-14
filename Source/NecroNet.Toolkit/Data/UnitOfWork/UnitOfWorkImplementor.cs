using System;
using System.Transactions;

namespace NecroNet.Toolkit.Data
{
	public class UnitOfWorkImplementor : IUnitOfWorkImplementor
	{
		private readonly IObjectContext _context;
		private readonly IUnitOfWorkFactory _factory;

		public UnitOfWorkImplementor(IUnitOfWorkFactory factory, IObjectContext context)
		{
			_factory = factory;
			_context = context;
		}

		public void Dispose()
		{
			_factory.DisposeUnitOfWork(this);
			_context.Dispose();
		}

		public void Flush()
		{
			_context.SaveChanges();
		}

		public ITransaction BeginTransaction(TransactionScopeOption? scopeOption, IsolationLevel? isolationLevel, int? timeout)
		{
			return new GenericTransaction(scopeOption, isolationLevel, timeout);
		}
	}
}