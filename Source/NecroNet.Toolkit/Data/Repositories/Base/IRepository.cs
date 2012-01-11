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
		/// <summary>
		/// Gets collection of entities as emumerable.
		/// </summary>
		IEnumerable<TEntity> GetEnumerable();

		/// <summary>
		/// Gets collection of entities filtered by specified pageNumber as emumerable.
		/// </summary>
		/// <param name="predicate">Predicate used to filter the collection</param>
		IEnumerable<TEntity> GetEnumerable(Expression<Func<TEntity, bool>> predicate);

		/// <summary>
		/// Gets collection of entities ordered by specified key in specified direction as emumerable.
		/// </summary>
		/// <typeparam name="TKey">Type of property used for ordering</typeparam>
		/// <param name="keySelector">Selector of property used for ordering</param>
		/// <param name="ascending">Sort order, true for ascending, false for descending</param>
		IEnumerable<TEntity> GetEnumerable<TKey>(Expression<Func<TEntity, TKey>> keySelector, bool ascending = true);

		/// <summary>
		/// Gets collection of entities filtered by specified pageNumber, ordered by specified key in specified direction as emumerable.
		/// </summary>
		/// <typeparam name="TKey">Type of property used for ordering</typeparam>
		/// <param name="predicate">Predicate used to filter the collection</param>
		/// <param name="keySelector">Selector of property used for ordering</param>
		/// <param name="ascending">Sort order, true for ascending, false for descending</param>
		IEnumerable<TEntity> GetEnumerable<TKey>(Expression<Func<TEntity, bool>> predicate,
		                                         Expression<Func<TEntity, TKey>> keySelector, bool ascending = true);

		/// <summary>
		/// Gets query of entities.
		/// </summary>
		IQueryable<TEntity> GetQueryable();

		/// <summary>
		/// Gets a list of entities.
		/// </summary>
		IList<TEntity> GetList();

		/// <summary>
		/// Gets a list of entities filtered by specified pageNumber.
		/// </summary>
		/// <param name="predicate">Predicate used to filter the collection</param>
		IList<TEntity> GetList(Expression<Func<TEntity, bool>> predicate);

		/// <summary>
		/// Gets a paged list of entities ordered by specified key in specified direction.
		/// </summary>
		/// <typeparam name="TKey">Type of property used for ordering</typeparam>
		/// <param name="pageNumber">Number of current page</param>
		/// <param name="pageSize">The amount of items on a page</param>
		/// <param name="orderBySelector">Selector of property used for ordering</param>
		/// <param name="ascending">Sort order, true for ascending, false for descending</param>
		IPagedList<TEntity> GetPage<TKey>(int pageNumber, int pageSize, Expression<Func<TEntity, TKey>> orderBySelector, bool ascending = true);

		/// <summary>
		/// Gets a paged list of entities filtered by specified pageNumber, ordered by specified key in specified direction.
		/// </summary>
		/// <typeparam name="TKey">Type of property used for ordering</typeparam>
		/// <param name="predicate">Predicate used to filter the collection</param>
		/// <param name="pageNumber">Number of current page</param>
		/// <param name="pageSize">The amount of items on a page</param>
		/// <param name="orderBySelector">Selector of property used for ordering</param>
		/// <param name="ascending">Sort order, true for ascending, false for descending</param>
		IPagedList<TEntity> GetPage<TKey>(Expression<Func<TEntity, bool>> predicate, int pageNumber, int pageSize, Expression<Func<TEntity, TKey>> orderBySelector, bool ascending = true);

		/// <summary>
		/// Gets sorted, paged list of entities.
		/// </summary>
		/// <param name="pageNumber">Number of current page</param>
		/// <param name="pageSize">The amount of items on a page</param>
		/// <param name="sortKey">Name of the property to sort by</param>
		/// <param name="sortDirection">Sort direction (use constants in <see cref="SortDirection"/>)</param>
		ISortedPagedList<TEntity> GetSortedPagedList(int pageNumber, int pageSize, string sortKey, string sortDirection);

		/// <summary>
		/// Gets sorted, paged list of entities filtered by specified pageNumber.
		/// </summary>
		/// <param name="pageNumber">Predicate used to filter the collection</param>
		/// <param name="index">Number of current page</param>
		/// <param name="pageSize">The amount of items on a page</param>
		/// <param name="sortKey">Name of the property to sort by</param>
		/// <param name="sortDirection">Sort direction (use constants in <see cref="SortDirection"/>)</param>
		ISortedPagedList<TEntity> GetSortedPagedList(Expression<Func<TEntity, bool>> pageNumber, int index, int pageSize,
		                                             string sortKey, string sortDirection);

		/// <summary>
		/// Gets a single entity that matches specified pageNumber.
		/// </summary>
		/// <param name="predicate">Predicate indentifying the entity</param>
		TEntity Get(Expression<Func<TEntity, bool>> predicate);

		/// <summary>
		/// Adds specified entity to the repository.
		/// </summary>
		/// <param name="entity">The entity to be added</param>
		void Add(TEntity entity);

		/// <summary>
		/// Removes single entity that matches specified pageNumber from the repository.
		/// </summary>
		/// <param name="predicate">Predicate indentifying the entity</param>
		void Remove(Expression<Func<TEntity, bool>> predicate);

		/// <summary>
		/// Removes specified entity from the repository.
		/// </summary>
		/// <param name="entity">The entity to be removed</param>
		void Remove(TEntity entity);

		/// <summary>
		/// Removes all entities that match specified pageNumber from the repository.
		/// </summary>
		/// <param name="predicate">Predicate indentifying the entities</param>
		void RemoveRange(Expression<Func<TEntity, bool>> predicate);

		/// <summary>
		/// Gets the count of all entitites that match specified pageNumber in the repository.
		/// </summary>
		/// <param name="predicate">Predicate indentifying the entities</param>
		int Count(Expression<Func<TEntity, bool>> predicate);

		/// <summary>
		/// Gets the count of all entitites in the repository.
		/// </summary>
		int Count();

		/// <summary>
		/// Registers an include (eager load) of specified property for the next query.
		/// </summary>
		/// <typeparam name="TProperty">Type of property to include</typeparam>
		/// <param name="includeSelector">Selector of property to include</param>
		IRepository<TEntity> WithInclude<TProperty>(Expression<Func<TEntity, TProperty>> includeSelector);

		/// <summary>
		/// Registers an include (eager load) of specified property for the next query.
		/// </summary>
		/// <param name="include">Name of property to include</param>
		IRepository<TEntity> WithInclude(string include);

		/// <summary>
		/// Removes all entities from the repository.
		/// </summary>
		void Clear();

		/// <summary>
		/// Marks the specified property as updated. (this method may not be supported by all types of repository, e.g. if entities are self tracking)
		/// </summary>
		/// <param name="entity">The updated entity</param>
		void Update(TEntity entity);
	}
}