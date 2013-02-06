using System;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace NecroNet.Toolkit.Entity
{
	public class GenericRepository<TEntity> : IRepository<TEntity> where TEntity : class
	{
		private readonly DbContext _context;
		private readonly DbSet<TEntity> _set;

		public GenericRepository(DbContext context)
		{
			_context = context;
			_set = context.Set<TEntity>();
		}

		public IQueryable<TEntity> GetAll()
		{
			return _set;
		}

		public TEntity Get(params object[] primaryKeys)
		{
			return _set.Find(primaryKeys);
		}

		public void Add(TEntity entity)
		{
			_set.Add(entity);
		}

		public void Delete(TEntity entity)
		{
			_set.Remove(entity);
		}

		public void Delete(params object[] primaryKeys)
		{
			var entity = _set.Find(primaryKeys);
			if(entity != null)
			{
				Delete(entity);
			}
		}

		public void Delete(Expression<Func<TEntity, bool>> predicate)
		{
			var entities = _set.Where(predicate);
			foreach (var entity in entities)
			{
				Delete(entity);
			}
		}

		public void Update(TEntity entity)
		{
			_context.Entry(entity).State = EntityState.Modified;
		}
	}
}