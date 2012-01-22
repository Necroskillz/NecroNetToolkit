using System;

namespace NecroNet.Toolkit.Data
{
	public interface ITransaction : IDisposable
	{
		/// <summary>
		/// Commits the database transaction
		/// </summary>
		void Commit();
	}
}