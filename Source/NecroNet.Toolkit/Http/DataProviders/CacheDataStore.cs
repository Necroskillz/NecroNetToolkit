using System.Linq;
using System.Collections.Generic;
using System;
using System.Web;
using System.Web.Caching;

namespace NecroNet.Toolkit.Http
{
	internal class CacheDataStore : IHttpCacheDataStore
	{
		private Cache Store
		{
			get
			{
				return HttpContext.Current.Cache;
			}
		}

		public bool Contains(string key)
		{
			return Store.Get(key) != null;
		}

		public void Clear()
		{
			var keys = new List<string>();
			var enumerator = Store.GetEnumerator();

			while (enumerator.MoveNext())
			{
				keys.Add(enumerator.Key.ToString());
			}

			foreach (var key in keys)
			{
				Store.Remove(key);
			}
		}

		public T Get<T>(string key)
		{
			var value = Store[key];

			return value == null ? default(T) : (T)value;
		}

		public object Get(string key)
		{
			return Store.Get(key);
		}

		public void Set(string key, object value, DateTime? absoluteExpiration = new DateTime?(), TimeSpan? slidingExpiration = new TimeSpan?(), CacheItemPriority cacheItemPriority = CacheItemPriority.Normal, CacheItemRemovedCallback removedCallback = null)
		{
			Store.Insert(key, value, null, absoluteExpiration ?? Cache.NoAbsoluteExpiration, slidingExpiration ?? Cache.NoSlidingExpiration, cacheItemPriority, removedCallback);
		}
	}
}