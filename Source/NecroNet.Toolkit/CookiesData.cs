using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace NecroNet.Toolkit
{
	public static class Cookies
	{
		private static ILocalDataProvider _cookiesDataProvider = new CookiesDataProvider();

		public static ILocalDataProvider Data
		{
			get
			{
				return _cookiesDataProvider;
			}
		}

		public static void ChangeContext(ILocalDataProvider dataProvider)
		{
			_cookiesDataProvider = dataProvider;
		}

		public class CookiesDataProvider : ILocalDataProvider
		{
			public HttpCookieCollection OutgoingCookies
			{
				get
				{
					return HttpContext.Current.Response.Cookies;
				}
			}

			public HttpCookieCollection IncommingCookies
			{
				get
				{
					return HttpContext.Current.Request.Cookies;
				}
			}

			public object this[object key]
			{
				get
				{
					var cookie = IncommingCookies[key.ToString()];
					return cookie == null ? null : cookie.Value;
				}
				set 
				{
					var cookie = new HttpCookie(key.ToString())
					             	{
					             		Expires = DateTime.Now.Add(TimeSpan.FromDays(365)),
					             		Value = value.ToString()
					             	};

					OutgoingCookies.Set(cookie);
					IncommingCookies.Set(cookie);
				}
			}

			public int Count
			{
				get
				{
					return IncommingCookies.Count;
				}
			}

			public void Clear()
			{
				OutgoingCookies.Clear();
			}

			public bool Contains(object key)
			{
				return IncommingCookies.AllKeys.Contains(key.ToString());
			}
		}
	}
}
