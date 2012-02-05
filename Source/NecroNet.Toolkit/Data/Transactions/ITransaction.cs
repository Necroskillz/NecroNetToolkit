using System;

namespace NecroNet.Toolkit.Data
{
	public interface ITransaction : IDisposable
	{
		/// <summary>
		/// Indicates that all operations within the scope are completed successfully.
		/// </summary>
		void Complete();
	}
}