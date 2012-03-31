using System.Linq;
using System.Collections.Generic;
using System;

namespace NecroNet.Toolkit.Data
{
	/// <summary>
	/// Interface for a factory to get repositories through IoC container.
	/// </summary>
	public interface IRepositoryFactory
	{
		/// <summary>
		/// Gets a repository of the specified type.
		/// </summary>
		/// <typeparam name="TEntity">The type of the repository.</typeparam>
		IRepository<TEntity> GetRepository<TEntity>() where TEntity : class;
	}
}