using System;
using System.Data;
using NecroNet.Toolkit.Internals;

namespace NecroNet.Toolkit.Data
{
	public class UnitOfWorkImplementor : IUnitOfWorkImplementor
	{
		private readonly IObjectContext _context;
		private readonly IUnitOfWorkFactory _factory;
	    private bool _isDisposed;

		public UnitOfWorkImplementor(IUnitOfWorkFactory factory, IObjectContext context)
		{
			_factory = factory;
			_context = context;
		}

		public void Dispose()
		{
			_factory.DisposeUnitOfWork();
			_context.Dispose();
		    _isDisposed = true;
		}

		public void Flush()
		{
			_context.SaveChanges();
		}

		public ITransaction BeginTransaction(IsolationLevel? isolationLevel)
		{
			return new DatabaseTransaction(_context.Connection, isolationLevel);
		}

	    public IObjectContext Context
	    {
	        get
	        {
                if (_isDisposed)
                {
                    ExceptionHelper.ThrowInvalidOp("This unit of work and it's object context has already been disposed.");
                }

	            return _context;
	        }
	    }
	}
}