using System;
using System.Data;

namespace NecroNet.Toolkit.Data
{
	public interface IUnitOfWork : IDisposable
	{
		/// <summary>
		/// Persists all updates to the data source.
		/// </summary>
		void Flush();

        /// <summary>
        /// Gets underlying object context.
        /// Under normal circumstances you should not use this. If you do, make sure to abstract it away if you want your code to be unit testable.
        /// </summary>
        IObjectContext Context { get; }
	}
}