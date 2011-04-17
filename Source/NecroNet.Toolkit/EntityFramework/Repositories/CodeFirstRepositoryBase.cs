using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Objects;
using System.Linq;
using System.Text;

namespace NecroNet.Toolkit.EntityFramework
{
	public abstract class CodeFirstRepositoryBase<TDbContext, TEntity> : UltimateEntityRepositoryBase<TEntity>
		where TDbContext: DbContext, IObjectContext
		where TEntity : class
	{
		protected abstract TDbContext DbContext { get; }

		private EntityOperatorBase<TEntity> _operator;
		internal override EntityOperatorBase<TEntity> Operator
		{
			get
			{
				return _operator ?? (_operator = new CodeFirstEntityOperator<TEntity>(() => DbContext.Set<TEntity>()));
			}
		}

		public override void Clear()
		{
			var objectContext = ((IObjectContextAdapter) DbContext).ObjectContext;
			var entitySet = objectContext.CreateObjectSet<TEntity>().EntitySet;
			DbContext.Database.ExecuteSqlCommand(string.Format("DELETE FROM {0}", entitySet.Name));
			objectContext.Refresh(RefreshMode.StoreWins, entitySet);
		}
	}
}
