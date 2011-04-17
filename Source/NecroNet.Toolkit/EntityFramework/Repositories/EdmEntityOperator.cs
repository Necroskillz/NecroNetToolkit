using System;
using System.Data.Entity;
using System.Data.Objects;
using System.Linq;

namespace NecroNet.Toolkit.EntityFramework
{
	internal class EdmEntityOperator<TEntity> : EntityOperatorBase<TEntity> where TEntity:class
	{
		private readonly Func<IObjectSet<TEntity>> _getObjectSet;

		public EdmEntityOperator(Func<IObjectSet<TEntity>> getObjectSet)
		{
			_getObjectSet = getObjectSet;
		}

		public override void AddEntity(TEntity entity)
		{
			_getObjectSet().AddObject(entity);
		}

		public override void RemoveEntity(TEntity entity)
		{
			_getObjectSet().DeleteObject(entity);
		}

		public override IQueryable<TEntity> GetConfiguredQuery(QueryConfig config)
		{
			var query = _getObjectSet().AsQueryable();
			query = config.Includes.Aggregate(query, (current, include) => current.Include(include));

			config.Reset();

			return query;
		}

		public override IQueryable<TEntity> GetQuery()
		{
			return _getObjectSet();
		}
	}
}
