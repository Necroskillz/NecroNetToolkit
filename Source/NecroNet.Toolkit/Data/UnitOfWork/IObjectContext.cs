using System;
using System.Data.Common;

namespace NecroNet.Toolkit.Data
{
	/// <summary>
	/// Interface that needs to be implemented by object context to work with the toolkit.
	/// </summary>
	public interface IObjectContext : IDisposable
	{
		int SaveChanges();
	    DbConnection Connection { get; }
	}
}