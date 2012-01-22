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
			var query = config.Configure(GetStore());

			return query;
		}

		public override IQueryable<TEntity> GetQuery()
		{
			return GetStore();
		}
	}
}
