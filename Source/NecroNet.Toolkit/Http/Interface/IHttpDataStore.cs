using System.Linq;
using System.Collections.Generic;
using System;

namespace NecroNet.Toolkit.Http
{
	public interface IHttpDataStore
	{
		/// <summary>
		/// Removes all entries from the data store.
		/// </summary>
		void Clear();
	}
}