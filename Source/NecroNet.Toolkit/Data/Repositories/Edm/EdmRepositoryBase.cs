using System;
using System.Collections.Generic;
using System.Data.Objects;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using NecroNet.Toolkit.Resources;

namespace NecroNet.Toolkit.Data
{
	public abstract class EdmRepositoryBase<TObjectContext, TEntity> : UltimateRepositoryBase<TEntity>
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

			if(attributes.Length != 1)
			{
				Throw.New<InvalidOperationException>(Res.ExceptionMessage_Repository_InvalidEntitySetNameAttributeSetup);
			}

			EntitySetName = ((EntitySetNameAttribute)attributes.First()).EntitySetName;
			_entitySetPropertyInfo = typeof(TObjectContext).GetProperty(EntitySetName);

			if(_entitySetPropertyInfo == null)
			{
				Throw.New<InvalidOperationException>(Res.ExceptionMessage_Repository_InvalidEntitySetName);
			}
		}

		private IEntityOperator<TEntity> _operator;
		protected override IEntityOperator<TEntity> Operator
		{
			get
			{
				return _operator ?? (_operator = new EdmEntityOperator<TEntity>(() => EntitySet));
			}
		}

		public override void Clear()
		{
			var sql = ((ObjectQuery<TEntity>)EntitySet).ToTraceString();
			var match = Regex.Match(sql, @".*FROM (\[.*?\].\[.*?\]) AS \[.*?\]$", RegexOptions.Singleline);
			var tableName = match.Groups[1].Value;

			var context = ObjectContext.AsActual<ObjectContext>();
			context.ExecuteStoreCommand(string.Format("DELETE FROM {0}", tableName));
		}
	}
}
