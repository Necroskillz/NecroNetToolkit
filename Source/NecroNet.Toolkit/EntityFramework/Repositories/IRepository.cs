using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using PagedList;

namespace NecroNet.Toolkit.EntityFramework
{
	public interface IRepository<TEntity>
		where TEntity : class
	{
		IEnumerable<TEntity> GetEnumerable();
		IEnumerable<TEntity> GetEnumerable(Expression<Func<TEntity, bool>> predicate);
		IEnumerable<TEntity> GetEnumerable<TKey>(Expression<Func<TEntity, TKey>> keySelector, bool ascending = true);

		IEnumerable<TEntity> GetEnumerable<TKey>(Expression<Func<TEntity, bool>> predicate,
		                                         Expression<Func<TEntity, TKey>> keySelector, bool ascending = true);

		IList<TEntity> GetList();
		IPagedList<TEntity> GetPagedList(int index, int pageSize);
		ISortedPagedList<TEntity> GetSortedPagedList(int index, int pageSize, string sortKey, string sortDirection);

		IList<TEntity> GetList(Expression<Func<TEntity, bool>> predicate);
		IPagedList<TEntity> GetPagedList(Expression<Func<TEntity, bool>> predicate, int index, int pageSize);

		ISortedPagedList<TEntity> GetSortedPagedList(Expression<Func<TEntity, bool>> predicate, int index, int pageSize,
		                                             string sortKey, string sortDirection);

		TEntity Get(Expression<Func<TEntity, bool>> predicate);

		void Add(TEntity entity);

		void Remove(Expression<Func<TEntity, bool>> predicate);
		void Remove(TEntity entity);
		void RemoveRange(Expression<Func<TEntity, bool>> predicate);

		int Count(Expression<Func<TEntity, bool>> predicate);
		int Count();
	}
}