using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Objects;
using System.Linq;
using System.Text;

namespace NecroNet.Toolkit.Data
{
	public abstract class CodeFirstRepositoryBase<TDbContext, TEntity> : UltimateRepositoryBase<TEntity>
		where TDbContext: DbContext, IObjectContext
		where TEntity : class
	{
		protected abstract TDbContext DbContext { get; }

		private IEntityOperator<TEntity> _operator;
		protected override IEntityOperator<TEntity> Operator
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

		public override void Update(TEntity entity)
		{
			DbContext.Entry(entity).State = EntityState.Modified;
		}
	}
}
