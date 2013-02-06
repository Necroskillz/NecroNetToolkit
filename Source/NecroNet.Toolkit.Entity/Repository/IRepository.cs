using System;
using System.Linq;
using System.Linq.Expressions;

namespace NecroNet.Toolkit.Entity
{
	public interface IRepository<TEntity> where TEntity : class
	{
		IQueryable<TEntity> GetAll();
		TEntity Get(params object[] primaryKeys);

		void Add(TEntity entity);

		void Delete(TEntity entity);
		void Delete(params object[] primaryKeys);
		void Delete(Expression<Func<TEntity, bool>> predicate);

		void Update(TEntity entity);
	}
}