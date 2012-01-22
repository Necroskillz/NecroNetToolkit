using System;
using System.Data.Entity;
using System.Data.Objects;
using System.Linq;

namespace NecroNet.Toolkit.Data
{
	internal class EdmEntityOperator<TEntity> : EntityOperatorBase<TEntity, IObjectSet<TEntity>> where TEntity:class
	{
		public EdmEntityOperator(Func<IObjectSet<TEntity>> getStore) : base(getStore)
		{
		}

		public override void AddEntity(TEntity entity)
		{
			GetStore().AddObject(entity);
		}

		public override void RemoveEntity(TEntity entity)
		{
			GetStore().DeleteObject(entity);
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
