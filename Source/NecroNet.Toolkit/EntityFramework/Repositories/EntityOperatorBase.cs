using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NecroNet.Toolkit.EntityFramework
{
	internal abstract class EntityOperatorBase<TEntity> where TEntity : class
	{
		public abstract void AddEntity(TEntity entity);
		public abstract void RemoveEntity(TEntity entity);
		public abstract IQueryable<TEntity> GetConfiguredQuery(QueryConfig config);
		public abstract IQueryable<TEntity> GetQuery();
	}
}
