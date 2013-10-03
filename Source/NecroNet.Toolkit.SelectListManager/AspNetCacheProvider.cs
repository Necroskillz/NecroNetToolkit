using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Caching;

namespace NecroNet.Toolkit.Mvc
{
	internal class AspNetCacheProvider : ISelectListManagerCacheProvider
	{
		public List<T> Get<T>(string key)
		{
			var context = HttpContext.Current;

			if(context == null) return null;

			return context.Cache.Get(key) as List<T>;
		}

		public void Set<T>(string key, List<T> data, int cacheMinutes)
		{
			var context = HttpContext.Current;

			if (context == null) return;

			context.Cache.Insert(key, data, null, DateTime.Now.AddMinutes(cacheMinutes), Cache.NoSlidingExpiration);
		}
	}
}