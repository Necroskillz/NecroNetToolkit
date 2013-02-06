using System;
using System.Data.Entity;

namespace NecroNet.Toolkit.Entity
{
	public interface IUnitOfWork : IDisposable
	{
		void Save<TDbContext>() where TDbContext : DbContext;
		void Save();
		IRepository<TEntity> GetRepository<TEntity>() where TEntity : class;
		TDbContext GetContext<TDbContext>() where TDbContext : DbContext;
	}
}