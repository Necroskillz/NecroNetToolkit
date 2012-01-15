using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Objects;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace NecroNet.Toolkit.Data
{
	public abstract class CodeFirstRepositoryBase<TDbContext, TEntity> : UltimateRepositoryBase<TEntity>
		where TDbContext: IObjectContext
		where TEntity : class
	{
		protected abstract TDbContext DbContext { get; }

		private IEntityOperator<TEntity> _operator;
		protected override IEntityOperator<TEntity> Operator
		{
			get
			{
				return _operator ?? (_operator = new CodeFirstEntityOperator<TEntity>(() => DbContext.AsActual<DbContext>().Set<TEntity>()));
			}
		}

		public override void Clear()
		{
			var context = DbContext.AsActual<DbContext>();
			
			var sql = ((ObjectQuery<TEntity>)context.Set<TEntity>().AsQueryable()).ToTraceString();
			var match = Regex.Match(sql, @".*FROM (\[.*?\].\[.*?\]) AS \[.*?\]$", RegexOptions.Singleline);
			var tableName = match.Groups[1].Value;

			context.Database.ExecuteSqlCommand(string.Format("DELETE FROM {0}", tableName));
		}

		public override void Update(TEntity entity)
		{
			DbContext.AsActual<DbContext>().Entry(entity).State = EntityState.Modified;
		}
	}
}
