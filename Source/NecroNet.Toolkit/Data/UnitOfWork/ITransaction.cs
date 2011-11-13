using System;

namespace NecroNet.Toolkit.Data
{
	public interface ITransaction : IDisposable
	{
		void Commit();
	}
}