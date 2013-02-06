using System;
using System.Linq;
using System.Collections.Generic;
using NecroNet.Toolkit.Core.Resources;

namespace NecroNet.Toolkit.Entity
{
	public class DbContextBuilder
	{
		private static DbContextBuilder _current;
		public static DbContextBuilder Current
		{
			get
			{
				return _current ?? (_current = new DbContextBuilder());
			}
		}

		private IDbContextFactory _dbContextFactory;

		public IDbContextFactory ContextFactory
		{
			get
			{
				return _dbContextFactory ?? new ActivatorDbContextFactory();
			}
		}

		public void SetContextFactory(IDbContextFactory factory)
		{
			if(factory == null)
			{
				Throw.NewArgumentNullException(() => factory);
			}

			_dbContextFactory = factory;
		}

		
		public void SetContextFactory(Type factoryType)
		{
			if (factoryType == null)
			{
				Throw.NewArgumentNullException(() => factoryType);
			}

			if(!typeof(IDbContextFactory).IsAssignableFrom(factoryType))
			{
				Throw.NewArgumentException(() => factoryType, Res.DbContextBuilder_TypeIsNotIDbContextFactory);
			}

			_dbContextFactory = (IDbContextFactory)Activator.CreateInstance(factoryType);
		}
	}
}