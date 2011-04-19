using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using PagedList;

namespace NecroNet.Toolkit.Data
{
	public interface IRepository<TEntity>
		where TEntity : class
	{
		IEnumerable<TEntity> GetEnumerable();
		IEnumerable<TEntity> GetEnumerable(Expression<Func<TEntity, bool>> predicate);
		IEnumerable<TEntity> GetEnumerable<TKey>(Expression<Func<TEntity, TKey>> keySelector, bool ascending = true);

		IEnumerable<TEntity> GetEnumerable<TKey>(Expression<Func<TEntity, bool>> predicate,
		                                         Expression<Func<TEntity, TKey>> keySelector, bool ascending = true);

		IQueryable<TEntity> GetQueryable();

		IList<TEntity> GetList();
		IList<TEntity> GetList(Expression<Func<TEntity, bool>> predicate);

		IPagedList<TEntity> GetPage<TKey>(int index, int pageSize, Expression<Func<TEntity, TKey>> orderBySelector, bool ascending = true);
		IPagedList<TEntity> GetPage<TKey>(Expression<Func<TEntity, bool>> predicate, int index, int pageSize, Expression<Func<TEntity, TKey>> orderBySelector, bool ascending = true);

		[Obsolete("This method has a bug when skipping records with entity framework and was replaced by GetPage<TKey>")]
		IPagedList<TEntity> GetPagedList(int index, int pageSize);

		[Obsolete("This method has a bug when skipping records with entity framework and was replaced by GetPage<TKey>")]
		IPagedList<TEntity> GetPagedList(Expression<Func<TEntity, bool>> predicate, int index, int pageSize);

		ISortedPagedList<TEntity> GetSortedPagedList(int index, int pageSize, string sortKey, string sortDirection);
		ISortedPagedList<TEntity> GetSortedPagedList(Expression<Func<TEntity, bool>> predicate, int index, int pageSize,
		                                             string sortKey, string sortDirection);

		TEntity Get(Expression<Func<TEntity, bool>> predicate);

		void Add(TEntity entity);

		void Remove(Expression<Func<TEntity, bool>> predicate);
		void Remove(TEntity entity);
		void RemoveRange(Expression<Func<TEntity, bool>> predicate);

		int Count(Expression<Func<TEntity, bool>> predicate);
		int Count();

		IRepository<TEntity> WithInclude<TProperty>(Expression<Func<TEntity, TProperty>> includeSelector);
		IRepository<TEntity> WithInclude(string include);

		void Clear();
	}
}