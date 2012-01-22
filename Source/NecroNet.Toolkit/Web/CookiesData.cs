using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace NecroNet.Toolkit
{
	public static class Cookies
	{
		private static IHttpCookiesDataProvider _cookiesDataProvider = new CookiesDataProvider();

		public static IHttpCookiesDataProvider Data
		{
			get
			{
				return _cookiesDataProvider;
			}
		}

		public static void ChangeContext(IHttpCookiesDataProvider dataProvider)
		{
			_cookiesDataProvider = dataProvider;
		}

		public class CookiesDataProvider : IHttpCookiesDataProvider
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
					Expires = DateTime.Now.Add(TimeSpan.FromDays(365)),
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
				IncommingCookies.Set(cookie);
			}
		}
	}
}
