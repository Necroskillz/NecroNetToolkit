using System.Linq;
using System.Collections.Generic;
using System;
using System.Web;

namespace NecroNet.Toolkit.Web.Http
{
	internal class CookiesDataStore : IHttpCookiesDataStore
	{
		private HttpCookieCollection OutgoingCookies
		{
			get
			{
				return HttpContext.Current.Response.Cookies;
			}
		}

		private HttpCookieCollection IncommingCookies
		{
			get
			{
				return HttpContext.Current.Request.Cookies;
			}
		}

		public bool Contains(string key)
		{
			return IncommingCookies.AllKeys.Contains(key);
		}

		public void Clear()
		{
			OutgoingCookies.Clear();
		}

		public string Get(string key)
		{
			var cookie = IncommingCookies[key];
			return cookie == null ? null : cookie.Value;
		}

		public void Set(string key, string value, DateTime expires)
		{
			var cookie = new HttpCookie(key)
			             	{
			             		Expires = expires,
			             		Value = value
			             	};
			
			OutgoingCookies.Set(cookie);
		}

		public HttpCookie GetCookie(string key)
		{
			return IncommingCookies[key];
		}

		public void SetCookie(HttpCookie cookie)
		{
			OutgoingCookies.Set(cookie);
		}
	}
}