using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Reflection;
using System.Text;

namespace NecroNet.Toolkit.Data
{
	internal class CodeFirstEntityOperator<TEntity> : EntityOperatorBase<TEntity, IDbSet<TEntity>> where TEntity:class
	{
		public CodeFirstEntityOperator(Func<IDbSet<TEntity>> getStore) : base(getStore)
		{
		}

		public override void AddEntity(TEntity entity)
		{
			GetStore().Add(entity);
		}

		public override void RemoveEntity(TEntity entity)
		{
			GetStore().Remove(entity);
		}

		public override IQueryable<TEntity> GetConfiguredQuery(QueryConfig config)
		{
			var query = GetStore().AsQueryable();
			query = config.Includes.Aggregate(query, (current, include) => current.Include(include));

			config.Reset();

			return query;
		}

		public override IQueryable<TEntity> GetQuery()
		{
			return GetStore();
		}
	}
}
