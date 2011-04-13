using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.Objects;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Web.Mvc;
using PagedList;
using NecroNet.Toolkit.EntityFramework.Repositories;

namespace NecroNet.Toolkit.EntityFramework
{
	public abstract class UltimateEntityRepositoryBase<TObjectContext, TEntity> : IRepository<TEntity>
		where TEntity : class
		where TObjectContext : IObjectContext
	{
		private static PropertyInfo _entitySetPropertyInfo;
		protected abstract TObjectContext ObjectContext { get; }

		private string _entitySetName;
		protected virtual string EntitySetName
		{
			get
			{
				if(_entitySetName == null)
				{
					Initialize();
				}
				return _entitySetName;
			}
			set { _entitySetName = value; }
		}

		protected IObjectSet<TEntity> EntitySet
		{
			get
			{
				if(_entitySetPropertyInfo == null)
				{
					Initialize();
				}

				return _entitySetPropertyInfo.GetValue(ObjectContext, null) as IObjectSet<TEntity>;
			}
		}

		private const string QueryConfigKey = "QueryConfig.Key";

		private static QueryConfig QueryConfig
		{
			get
			{
				var config = Local.Data[QueryConfigKey];
				if(config == null)
				{
					Local.Data[QueryConfigKey] = config = new QueryConfig();
				}

				return config as QueryConfig;
			} 
		}

		private void Initialize()
		{
			var attributes = GetType().GetCustomAttributes(typeof(EntitySetNameAttribute), false);
			if(attributes == null || attributes.Length > 1 || attributes.Length == 0)
			{
				throw new InvalidOperationException("Invalid EntitySetName attribute setup.");
			}

			EntitySetName = ((EntitySetNameAttribute)attributes.First()).EntitySetName;
			_entitySetPropertyInfo = typeof(TObjectContext).GetProperty(EntitySetName);

			if(_entitySetPropertyInfo == null)
			{
				throw new InvalidOperationException("Invalid entity set name.");
			}
		}

		private ObjectQuery<TEntity> GetConfiguredQuery()
		{
			var config = QueryConfig;

			var query = config.Includes.Aggregate((ObjectQuery<TEntity>)EntitySet, (current, include) => current.Include(include));
			QueryConfig.Reset();

			return query;
		}

		public IQueryable<TEntity> GetQueryable()
		{
			return GetConfiguredQuery();
		}

		public virtual IList<TEntity> GetList()
		{
			return GetConfiguredQuery().ToList();
		}

		public virtual IList<TEntity> GetList(Expression<Func<TEntity, bool>> predicate)
		{
			return GetConfiguredQuery().Where(predicate).ToList();
		}

		public IPagedList<TEntity> GetPage<TKey>(int index, int pageSize, Expression<Func<TEntity, TKey>> orderBySelector, bool ascending)
		{
			return ascending
					? GetConfiguredQuery().OrderBy(orderBySelector).ToPagedList(index, pageSize)
					: GetConfiguredQuery().OrderByDescending(orderBySelector).ToPagedList(index, pageSize);
		}

		public IPagedList<TEntity> GetPage<TKey>(Expression<Func<TEntity, bool>> predicate, int index, int pageSize, Expression<Func<TEntity, TKey>> orderBySelector, bool ascending)
		{
			return ascending
					? GetConfiguredQuery().Where(predicate).OrderBy(orderBySelector).ToPagedList(index, pageSize)
					: GetConfiguredQuery().Where(predicate).OrderByDescending(orderBySelector).ToPagedList(index, pageSize);
		}

		[Obsolete("This method has a bug when skipping records with entity framework and was replaced by GetPage<TKey>")]
		public virtual IPagedList<TEntity> GetPagedList(Expression<Func<TEntity, bool>> predicate, int index, int pageSize)
		{
			return GetConfiguredQuery().Where(predicate).ToPagedList(index, pageSize);
		}

		[Obsolete("This method has a bug when skipping records with entity framework and was replaced by GetPage<TKey>")]
		public virtual IPagedList<TEntity> GetPagedList(int index, int pageSize)
		{
			return GetConfiguredQuery().ToPagedList(index, pageSize);
		}

		public virtual ISortedPagedList<TEntity> GetSortedPagedList(int index, int pageSize, string sortKey,
																	string sortDirection)
		{
			return GetConfiguredQuery().ToSortedPagedList(index, pageSize, sortKey, sortDirection);
		}

		public virtual ISortedPagedList<TEntity> GetSortedPagedList(Expression<Func<TEntity, bool>> predicate,
																	int index, int pageSize, string sortKey,
																	string sortDirection)
		{
			return GetConfiguredQuery().Where(predicate).ToSortedPagedList(index, pageSize, sortKey, sortDirection);
		}

		public virtual IEnumerable<TEntity> GetEnumerable()
		{
			return GetConfiguredQuery().AsEnumerable();
		}

		public virtual IEnumerable<TEntity> GetEnumerable(Expression<Func<TEntity, bool>> predicate)
		{
			return GetConfiguredQuery().Where(predicate).AsEnumerable();
		}

		public virtual IEnumerable<TEntity> GetEnumerable<TKey>(Expression<Func<TEntity, bool>> predicate,
																Expression<Func<TEntity, TKey>> keySelector,
																bool ascending = true)
		{
			return ascending
					? GetConfiguredQuery().Where(predicate).OrderBy(keySelector).AsEnumerable()
					: GetConfiguredQuery().Where(predicate).OrderByDescending(keySelector).AsEnumerable();
		}

		public virtual IEnumerable<TEntity> GetEnumerable<TKey>(Expression<Func<TEntity, TKey>> keySelector,
																bool ascending = true)
		{
			return ascending
					? GetConfiguredQuery().OrderBy(keySelector).AsEnumerable()
					: GetConfiguredQuery().OrderByDescending(keySelector).AsEnumerable();
		}

		public virtual TEntity Get(Expression<Func<TEntity, bool>> predicate)
		{
			return GetConfiguredQuery().FirstOrDefault(predicate);
		}

		public virtual void Add(TEntity entity)
		{
			EntitySet.AddObject(entity);
		}

		public virtual void Remove(TEntity entity)
		{
			EntitySet.DeleteObject(entity);
		}

		public virtual void Remove(Expression<Func<TEntity, bool>> predicate)
		{
			var item = EntitySet.Where(predicate).FirstOrDefault();
			EntitySet.DeleteObject(item);
		}

		public virtual void RemoveRange(Expression<Func<TEntity, bool>> predicate)
		{
			var items = GetList(predicate);
			while (items.Count != 0)
			{
				var item = items.First();
				Remove(item);
				items.Remove(item);
			}
		}

		public virtual int Count(Expression<Func<TEntity, bool>> predicate)
		{
			return EntitySet.Count(predicate);
		}

		public virtual int Count()
		{
			return EntitySet.Count();
		}

		public IRepository<TEntity> WithInclude<TProperty>(Expression<Func<TEntity, TProperty>> includeSelector)
		{
			var include = ExpressionHelper.GetExpressionText(includeSelector);
			QueryConfig.Includes.Add(include);
			return this;
		}

		public IRepository<TEntity> WithInclude(string include)
		{
			QueryConfig.Includes.Add(include);
			return this;
		}

		public virtual void Clear()
		{
			var context = ObjectContext as ObjectContext;
			context.ExecuteStoreCommand(string.Format("DELETE FROM {0}", EntitySetName));
			context.Refresh(RefreshMode.StoreWins, EntitySet);
		}
	}
}