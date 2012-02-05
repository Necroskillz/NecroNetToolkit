using System;
using System.Data;
using NecroNet.Toolkit.Internals;
using NecroNet.Toolkit.Resources;

namespace NecroNet.Toolkit.Data
{
	internal class UnitOfWorkImplementor : IUnitOfWorkImplementor
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

	    public IObjectContext Context
	    {
	        get
	        {
				if(_isDisposed)
				{
					Throw.New<InvalidOperationException>(Res.ExceptionMessage_UnitOfWork_Disposed);
				}

	            return _context;
	        }
	    }
	}
}