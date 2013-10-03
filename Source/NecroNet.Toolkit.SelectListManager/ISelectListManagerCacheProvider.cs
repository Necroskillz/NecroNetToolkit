using System;
using System.Linq;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;

namespace NecroNet.Toolkit.Mvc
{
	public interface ISelectListManagerCacheProvider
	{
		List<T> Get<T>(string key);
		void Set<T>(string key, List<T> data, int cacheMinutes);
	}
}