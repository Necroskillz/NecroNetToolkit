using System;

namespace NecroNet.Toolkit.Data
{
	public interface IUnitOfWork : IDisposable
	{
		/// <summary>
		/// Persists all updates to the data source.
		/// </summary>
		void Flush();
	}
}