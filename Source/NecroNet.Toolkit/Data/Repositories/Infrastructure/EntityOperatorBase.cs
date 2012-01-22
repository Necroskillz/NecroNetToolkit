using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NecroNet.Toolkit.Data
{
	public abstract class EntityOperatorBase<TEntity, TDataStore> : IEntityOperator<TEntity>
	{
		protected readonly Func<TDataStore> GetStore;

		protected EntityOperatorBase(Func<TDataStore> getStore)
		{
			GetStore = getStore;
		}

		public abstract void AddEntity(TEntity entity);
		public abstract void RemoveEntity(TEntity entity);
		public abstract IQueryable<TEntity> GetConfiguredQuery(QueryConfig config);
		public abstract IQueryable<TEntity> GetQuery();
	}
}
