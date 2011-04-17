using System;
using System.Collections.Generic;
using System.Data.Objects;
using System.Linq;
using System.Reflection;
using System.Text;

namespace NecroNet.Toolkit.EntityFramework
{
	public abstract class EdmRepositoryBase<TObjectContext, TEntity> : UltimateEntityRepositoryBase<TEntity>
		where TObjectContext : IObjectContext
		where TEntity : class
	{
		private static PropertyInfo _entitySetPropertyInfo;
		protected abstract TObjectContext ObjectContext { get; }

		protected IObjectSet<TEntity> EntitySet
		{
			get
			{
				if (_entitySetPropertyInfo == null)
				{
					Initialize();
				}

				return _entitySetPropertyInfo.GetValue(ObjectContext, null) as IObjectSet<TEntity>;
			}
		}

		private string _entitySetName;
		protected virtual string EntitySetName
		{
			get
			{
				if (_entitySetName == null)
				{
					Initialize();
				}
				return _entitySetName;
			}
			set { _entitySetName = value; }
		}

		private void Initialize()
		{
			var attributes = GetType().GetCustomAttributes(typeof(EntitySetNameAttribute), false);
			if (attributes == null || attributes.Length > 1 || attributes.Length == 0)
			{
				throw new InvalidOperationException("Invalid EntitySetName attribute setup.");
			}

			EntitySetName = ((EntitySetNameAttribute)attributes.First()).EntitySetName;
			_entitySetPropertyInfo = typeof(TObjectContext).GetProperty(EntitySetName);

			if (_entitySetPropertyInfo == null)
			{
				throw new InvalidOperationException("Invalid entity set name.");
			}
		}

		private EntityOperatorBase<TEntity> _operator;
		internal override EntityOperatorBase<TEntity> Operator
		{
			get
			{
				return _operator ?? (_operator = new EdmEntityOperator<TEntity>(() => EntitySet));
			}
		}

		public override void Clear()
		{
			var context = ObjectContext as ObjectContext;
			context.ExecuteStoreCommand(string.Format("DELETE FROM {0}", EntitySetName));
			context.Refresh(RefreshMode.StoreWins, EntitySet);
		}
	}
}
