using System;

namespace NecroNet.Toolkit.Data
{
	public interface IObjectContext : IDisposable
	{
		int SaveChanges();
	}
}