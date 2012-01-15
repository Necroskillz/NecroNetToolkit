using System;
using System.Data.Common;

namespace NecroNet.Toolkit.Data
{
	public interface IObjectContext : IDisposable
	{
		int SaveChanges();
	    DbConnection Connection { get; }
	}
}