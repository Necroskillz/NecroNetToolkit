using System;
using System.Collections.Concurrent;
using System.Data.Entity;
using System.Linq;
using System.Collections.Generic;
using System.Reflection;
using NecroNet.Toolkit.Core.Resources;

namespace NecroNet.Toolkit.Entity
{
	public class UnitOfWork : IUnitOfWork
	{
		~UnitOfWork()
		{
			Dispose(false);
		}

		private ConcurrentDictionary<Type, object> _repositoryCache = new ConcurrentDictionary<Type, object>();
		private ConcurrentDictionary<Type, DbContext> _contextCache = new ConcurrentDictionary<Type, DbContext>();
		private bool _disposed;
		
		public IRepository<TEntity> GetRepository<TEntity>() where TEntity : class
		{
			return (IRepository<TEntity>) _repositoryCache.GetOrAdd(typeof(TEntity), t =>
				{
					var contextType = EntityToContextNavigator.Current.Map.ContextFor(t);
					if (contextType == null)
					{
						Throw.New<InvalidOperationException>(Res.UnitOfWork_NoDbContextForEntity);
					}

					var dbContext = EnsureContext(contextType);

					return new GenericRepository<TEntity>(dbContext);
				});
		}

		public TDbContext GetContext<TDbContext>() where TDbContext : DbContext
		{
			return (TDbContext) EnsureContext(typeof (TDbContext));
		}

		private DbContext EnsureContext(Type type)
		{
			return _contextCache.GetOrAdd(type, t => DbContextBuilder.Current.ContextFactory.Create(t));
		}

		public void Save<TDbContext>() where TDbContext : DbContext
		{
			DbContext context;
			var type = typeof(TDbContext);
			
			if (_contextCache.TryGetValue(type, out context))
			{
				context.SaveChanges();
			}
		}

		public void Save()
		{
			foreach (var dbContext in _contextCache.Values)
			{
				dbContext.SaveChanges();
			}
		}

		public void Dispose()
		{
			Dispose(true);
			GC.SuppressFinalize(this);
		}

		protected virtual void Dispose(bool disposing)
		{
			if (!_disposed)
			{
				if (disposing)
				{
					foreach (var dbContext in _contextCache.Values)
					{
						dbContext.Dispose();
					}

					_contextCache = null;
					_repositoryCache = null;
				}

				_disposed = true;
			}
		}
	}
}