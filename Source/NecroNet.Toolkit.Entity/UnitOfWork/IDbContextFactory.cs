using System;
using System.Data.Entity;

namespace NecroNet.Toolkit.Entity
{
	public interface IDbContextFactory
	{
		DbContext Create(Type type);
	}
}