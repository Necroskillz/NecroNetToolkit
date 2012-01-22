using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Web;

namespace NecroNet.Toolkit
{
	public interface IHttpCookiesDataProvider : IHttpDataProvider
	{
		bool Contains(string key);

		string Get(string key);
		void Set(string key, string value, DateTime expires);

		HttpCookie GetCookie(string key);
		void SetCookie(HttpCookie cookie);
	}
}