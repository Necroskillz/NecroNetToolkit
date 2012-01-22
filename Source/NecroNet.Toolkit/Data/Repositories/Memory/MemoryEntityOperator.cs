using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NecroNet.Toolkit.Data
{
	internal class MemoryEntityOperator<TEntity> : EntityOperatorBase<TEntity, IList<TEntity>>
	{
		public MemoryEntityOperator(Func<IList<TEntity>> getStore) : base(getStore)
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
			return GetQuery();
		}

		public override IQueryable<TEntity> GetQuery()
		{
			return GetStore().AsQueryable();
		}
	}
}
