﻿using System;
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

namespace NecroNet.Toolkit.EntityFramework
{
	public abstract class UltimateEntityRepositoryBase<TEntity> : IRepository<TEntity>
		where TEntity : class
	{
		private readonly object QueryConfigKey = new object();

		private QueryConfig QueryConfig
		{
			get
			{
				var config = Local.Data[QueryConfigKey];
				if (config == null)
				{
					Local.Data[QueryConfigKey] = config = new QueryConfig();
				}

				return config as QueryConfig;
			}
		}

		internal abstract EntityOperatorBase<TEntity> Operator { get; } 

		public IQueryable<TEntity> GetQueryable()
		{
			return Operator.GetConfiguredQuery(QueryConfig);
		}

		public virtual IList<TEntity> GetList()
		{
			return Operator.GetConfiguredQuery(QueryConfig).ToList();
		}

		public virtual IList<TEntity> GetList(Expression<Func<TEntity, bool>> predicate)
		{
			return Operator.GetConfiguredQuery(QueryConfig).Where(predicate).ToList();
		}

		public IPagedList<TEntity> GetPage<TKey>(int index, int pageSize, Expression<Func<TEntity, TKey>> orderBySelector, bool ascending)
		{
			return ascending
					? Operator.GetConfiguredQuery(QueryConfig).OrderBy(orderBySelector).ToPagedList(index, pageSize)
					: Operator.GetConfiguredQuery(QueryConfig).OrderByDescending(orderBySelector).ToPagedList(index, pageSize);
		}

		public IPagedList<TEntity> GetPage<TKey>(Expression<Func<TEntity, bool>> predicate, int index, int pageSize, Expression<Func<TEntity, TKey>> orderBySelector, bool ascending)
		{
			return ascending
					? Operator.GetConfiguredQuery(QueryConfig).Where(predicate).OrderBy(orderBySelector).ToPagedList(index, pageSize)
					: Operator.GetConfiguredQuery(QueryConfig).Where(predicate).OrderByDescending(orderBySelector).ToPagedList(index, pageSize);
		}

		[Obsolete("This method has a bug when skipping records with entity framework and was replaced by GetPage<TKey>")]
		public virtual IPagedList<TEntity> GetPagedList(Expression<Func<TEntity, bool>> predicate, int index, int pageSize)
		{
			return Operator.GetConfiguredQuery(QueryConfig).Where(predicate).ToPagedList(index, pageSize);
		}

		[Obsolete("This method has a bug when skipping records with entity framework and was replaced by GetPage<TKey>")]
		public virtual IPagedList<TEntity> GetPagedList(int index, int pageSize)
		{
			return Operator.GetConfiguredQuery(QueryConfig).ToPagedList(index, pageSize);
		}

		public virtual ISortedPagedList<TEntity> GetSortedPagedList(int index, int pageSize, string sortKey,
																	string sortDirection)
		{
			return Operator.GetConfiguredQuery(QueryConfig).ToSortedPagedList(index, pageSize, sortKey, sortDirection);
		}

		public virtual ISortedPagedList<TEntity> GetSortedPagedList(Expression<Func<TEntity, bool>> predicate,
																	int index, int pageSize, string sortKey,
																	string sortDirection)
		{
			return Operator.GetConfiguredQuery(QueryConfig).Where(predicate).ToSortedPagedList(index, pageSize, sortKey, sortDirection);
		}

		public virtual IEnumerable<TEntity> GetEnumerable()
		{
			return Operator.GetConfiguredQuery(QueryConfig).AsEnumerable();
		}

		public virtual IEnumerable<TEntity> GetEnumerable(Expression<Func<TEntity, bool>> predicate)
		{
			return Operator.GetConfiguredQuery(QueryConfig).Where(predicate).AsEnumerable();
		}

		public virtual IEnumerable<TEntity> GetEnumerable<TKey>(Expression<Func<TEntity, bool>> predicate,
																Expression<Func<TEntity, TKey>> keySelector,
																bool ascending = true)
		{
			return ascending
					? Operator.GetConfiguredQuery(QueryConfig).Where(predicate).OrderBy(keySelector).AsEnumerable()
					: Operator.GetConfiguredQuery(QueryConfig).Where(predicate).OrderByDescending(keySelector).AsEnumerable();
		}

		public virtual IEnumerable<TEntity> GetEnumerable<TKey>(Expression<Func<TEntity, TKey>> keySelector,
																bool ascending = true)
		{
			return ascending
					? Operator.GetConfiguredQuery(QueryConfig).OrderBy(keySelector).AsEnumerable()
					: Operator.GetConfiguredQuery(QueryConfig).OrderByDescending(keySelector).AsEnumerable();
		}

		public virtual TEntity Get(Expression<Func<TEntity, bool>> predicate)
		{
			return Operator.GetConfiguredQuery(QueryConfig).FirstOrDefault(predicate);
		}

		public virtual void Add(TEntity entity)
		{
			Operator.AddEntity(entity);
		}

		public virtual void Remove(TEntity entity)
		{
			Operator.RemoveEntity(entity);
		}

		public virtual void Remove(Expression<Func<TEntity, bool>> predicate)
		{
			var item = Operator.GetQuery().Where(predicate).FirstOrDefault();
			Operator.RemoveEntity(item);
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
			return Operator.GetQuery().Count(predicate);
		}

		public virtual int Count()
		{
			return Operator.GetQuery().Count();
		}

		public virtual IRepository<TEntity> WithInclude<TProperty>(Expression<Func<TEntity, TProperty>> includeSelector)
		{
			var include = ExpressionHelper.GetExpressionText(includeSelector);
			QueryConfig.Includes.Add(include);
			return this;
		}

		public virtual IRepository<TEntity> WithInclude(string include)
		{
			QueryConfig.Includes.Add(include);
			return this;
		}

		public abstract void Clear();
	}
}