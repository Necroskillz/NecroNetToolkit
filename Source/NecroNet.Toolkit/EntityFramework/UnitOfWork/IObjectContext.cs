using System;

namespace NecroNet.Toolkit.EntityFramework
{
	public interface IObjectContext : IDisposable
	{
		int SaveChanges();
	}
}