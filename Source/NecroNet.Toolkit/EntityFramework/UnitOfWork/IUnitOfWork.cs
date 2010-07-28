using System;

namespace NecroNet.Toolkit.EntityFramework
{
	public interface IUnitOfWork : IDisposable
	{
		/// <summary>
		/// Persists all updates to the data source.
		/// </summary>
		void Flush();
	}
}