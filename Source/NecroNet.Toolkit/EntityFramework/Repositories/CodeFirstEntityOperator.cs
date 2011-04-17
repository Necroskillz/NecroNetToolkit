using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Reflection;
using System.Text;

namespace NecroNet.Toolkit.EntityFramework
{
	internal class CodeFirstEntityOperator<TEntity> : EntityOperatorBase<TEntity> where TEntity:class
	{
		private readonly Func<IDbSet<TEntity>> _getDbSet;

		public CodeFirstEntityOperator(Func<IDbSet<TEntity>> getDbSet)
		{
			_getDbSet = getDbSet;
		}

		public override void AddEntity(TEntity entity)
		{
			_getDbSet().Add(entity);
		}

		public override void RemoveEntity(TEntity entity)
		{
			_getDbSet().Remove(entity);
		}

		public override IQueryable<TEntity> GetConfiguredQuery(QueryConfig config)
		{
			var query = _getDbSet().AsQueryable();
			query = config.Includes.Aggregate(query, (current, include) => current.Include(include));

			config.Reset();

			return query;
		}

		public override IQueryable<TEntity> GetQuery()
		{
			return _getDbSet();
		}
	}
}
