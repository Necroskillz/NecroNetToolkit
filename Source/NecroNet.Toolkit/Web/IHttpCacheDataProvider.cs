using System;
using System.Web.Caching;

namespace NecroNet.Toolkit
{
	public interface IHttpCacheDataProvider : IHttpDataProvider
	{
		bool Contains(string key);

		T Get<T>(string key);
		object Get(string key);

		void Set(string key, object value, DateTime? absoluteExpiration = null, TimeSpan? slidingExpiration = null,
		         CacheItemPriority cacheItemPriority = CacheItemPriority.Default, CacheItemRemovedCallback removedCallback = null);
	}
}