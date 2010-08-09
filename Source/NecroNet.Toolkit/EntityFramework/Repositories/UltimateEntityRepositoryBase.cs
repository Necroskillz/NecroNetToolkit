using System;
using System.Collections.Generic;
using System.Data.Objects;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using PagedList;

namespace NecroNet.Toolkit.EntityFramework
{
	public abstract class UltimateEntityRepositoryBase<TObjectContext, TEntity> : IRepository<TEntity>
		where TEntity : class
		where TObjectContext : IObjectContext
	{
		private static PropertyInfo _entitySetPropertyInfo;
		protected abstract TObjectContext ObjectContext { get; }

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

		private void Initialize()
		{
			var attributes = GetType().GetCustomAttributes(typeof(EntitySetNameAttribute), false);
			if(attributes == null || attributes.Length > 1 || attributes.Length == 0)
			{
				throw new InvalidOperationException("Invalid EntitySetName attribute setup.");
			}

			string entitySetName = ((EntitySetNameAttribute)attributes.First()).EntitySetName;
			_entitySetPropertyInfo = typeof(TObjectContext).GetProperty(entitySetName);

			if(_entitySetPropertyInfo == null)
			{
				throw new InvalidOperationException("Invalid entity set name.");
			}
		}

		public virtual IList<TEntity> GetList()
		{
			return EntitySet.ToList();
		}

		public virtual IList<TEntity> GetList(Expression<Func<TEntity, bool>> predicate)
		{
			return EntitySet.Where(predicate).ToList();
		}

		public IPagedList<TEntity> GetPage<TKey>(int index, int pageSize, Expression<Func<TEntity, TKey>> orderBySelector, bool ascending)
		{
			return ascending
					? EntitySet.OrderBy(orderBySelector).ToPagedList(index, pageSize)
					: EntitySet.OrderByDescending(orderBySelector).ToPagedList(index, pageSize);
		}

		public IPagedList<TEntity> GetPage<TKey>(Expression<Func<TEntity, bool>> predicate, int index, int pageSize, Expression<Func<TEntity, TKey>> orderBySelector, bool ascending)
		{
			return ascending
					? EntitySet.Where(predicate).OrderBy(orderBySelector).ToPagedList(index, pageSize)
					: EntitySet.Where(predicate).OrderByDescending(orderBySelector).ToPagedList(index, pageSize);
		}

		[Obsolete("This method has a bug when skipping records with entity framework and was replaced by GetPage<TKey>")]
		public virtual IPagedList<TEntity> GetPagedList(Expression<Func<TEntity, bool>> predicate, int index, int pageSize)
		{
			return EntitySet.Where(predicate).ToPagedList(index, pageSize);
		}

		[Obsolete("This method has a bug when skipping records with entity framework and was replaced by GetPage<TKey>")]		public virtual IPagedList<TEntity> GetPagedList(int index, int pageSize)
		{
			return EntitySet.ToPagedList(index, pageSize);
		}

		public virtual ISortedPagedList<TEntity> GetSortedPagedList(int index, int pageSize, string sortKey,
																	string sortDirection)
		{
			return EntitySet.ToSortedPagedList(index, pageSize, sortKey, sortDirection);
		}

		public virtual ISortedPagedList<TEntity> GetSortedPagedList(Expression<Func<TEntity, bool>> predicate,
																	int index, int pageSize, string sortKey,
																	string sortDirection)
		{
			return EntitySet.Where(predicate).ToSortedPagedList(index, pageSize, sortKey, sortDirection);
		}

		public virtual IEnumerable<TEntity> GetEnumerable()
		{
			return EntitySet.AsEnumerable();
		}

		public virtual IEnumerable<TEntity> GetEnumerable(Expression<Func<TEntity, bool>> predicate)
		{
			return EntitySet.Where(predicate).AsEnumerable();
		}

		public virtual IEnumerable<TEntity> GetEnumerable<TKey>(Expression<Func<TEntity, bool>> predicate,
																Expression<Func<TEntity, TKey>> keySelector,
																bool ascending = true)
		{
			return ascending
					? EntitySet.Where(predicate).OrderBy(keySelector).AsEnumerable()
					: EntitySet.Where(predicate).OrderByDescending(keySelector).AsEnumerable();
		}

		public virtual IEnumerable<TEntity> GetEnumerable<TKey>(Expression<Func<TEntity, TKey>> keySelector,
																bool ascending = true)
		{
			return ascending
					? EntitySet.OrderBy(keySelector).AsEnumerable()
					: EntitySet.OrderByDescending(keySelector).AsEnumerable();
		}

		public virtual TEntity Get(Expression<Func<TEntity, bool>> predicate)
		{
			return EntitySet.FirstOrDefault(predicate);
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
			while(items.Count != 0)
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
	}
}