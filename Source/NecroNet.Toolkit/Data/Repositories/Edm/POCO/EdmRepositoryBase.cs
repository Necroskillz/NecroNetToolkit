using System;
using System.Collections.Generic;
using System.Data.Objects;
using System.Linq;
using System.Reflection;
using System.Text;

namespace NecroNet.Toolkit.Data
{
	public abstract class EdmPOCORepositoryBase<TObjectContext, TEntity> : EdmRepositoryBase<TObjectContext, TEntity>
		where TObjectContext : IObjectContext
		where TEntity : class
	{
		public override void Update(TEntity entity)
		{
			var entitySet = (ObjectSet<TEntity>)EntitySet;
			entitySet.ApplyCurrentValues(entity);
		}
	}
}
