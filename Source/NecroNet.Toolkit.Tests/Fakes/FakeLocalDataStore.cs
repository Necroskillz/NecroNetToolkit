using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Caching;
using NecroNet.Toolkit.Http;

namespace NecroNet.Toolkit.Tests.Fakes
{
	public class FakeLocalDataStore : IHttpGeneralDataStore
	{
		public void Clear()
		{
			throw new Exception();
		}

		public bool Contains(object key)
		{
			throw new Exception();
		}

		public T Get<T>(object key)
		{
			throw new Exception();
		}

		public object Get(object key)
		{
			throw new Exception();
		}

		public void Set(object key, object value)
		{
			throw new Exception();
		}
	}

	public class FakeCookieStore : IHttpCookiesDataStore
	{
		public void Clear()
		{
			throw new Exception();
		}

		public bool Contains(string key)
		{
			throw new Exception();
		}

		public string Get(string key)
		{
			throw new Exception();
		}

		public void Set(string key, string value, DateTime? expires)
		{
			throw new Exception();
		}

		public HttpCookie GetCookie(string key)
		{
			throw new Exception();
		}

		public void SetCookie(HttpCookie cookie)
		{
			throw new Exception();
		}
	}

	public class FakeCacheStore : IHttpCacheDataStore
	{
		public void Clear()
		{
			throw new Exception();
		}

		public bool Contains(string key)
		{
			throw new Exception();
		}

		public T Get<T>(string key)
		{
			throw new Exception();
		}

		public object Get(string key)
		{
			throw new Exception();
		}

		public void Set(string key, object value, DateTime? absoluteExpiration = new DateTime?(), TimeSpan? slidingExpiration = new TimeSpan?(), CacheItemPriority cacheItemPriority = CacheItemPriority.Normal, CacheItemRemovedCallback removedCallback = null)
		{
			throw new Exception();
		}
	}
}
