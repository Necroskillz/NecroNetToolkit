using System.Data.Entity;
using System.Linq;
using System.Collections.Generic;
using System;

namespace NecroNet.Toolkit.Entity
{
	public class ActivatorDbContextFactory : IDbContextFactory
	{
		public DbContext Create(Type type)
		{
			return (DbContext) Activator.CreateInstance(type);
		}
	}
}