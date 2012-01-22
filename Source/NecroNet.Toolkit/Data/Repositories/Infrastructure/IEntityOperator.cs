using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NecroNet.Toolkit.Data
{
	public interface IEntityOperator<TEntity>
	{
		void AddEntity(TEntity entity);
		void RemoveEntity(TEntity entity);
		IQueryable<TEntity> GetConfiguredQuery(QueryConfig config);
		IQueryable<TEntity> GetQuery();
	}
}
