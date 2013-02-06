using System;
using System.Collections.Generic;
using System.Linq;

namespace NecroNet.Toolkit.Web.Http
{
	/// <summary>
	/// Helper class for storing and retrieving data from http cookies (<c>HttpContext.Current.Request/Response.Cookies</c>).
	/// </summary>
	public static class Cookies
	{
		private static IHttpCookiesDataStore _cookiesDataStore = new CookiesDataStore();

		/// <summary>
		/// Accesses data through current data provider (default is <see cref="CookiesDataStore"/>).
		/// </summary>
		public static IHttpCookiesDataStore Data
		{
			get
			{
				return _cookiesDataStore;
			}
		}

		/// <summary>
		/// Changes current data provider. Can be used for test to provide stub implementation.
		/// </summary>
		/// <param name="dataStore">The data provider implementation to use.</param>
		public static void ChangeDataStore(IHttpCookiesDataStore dataStore)
		{
			_cookiesDataStore = dataStore;
		}

		/// <summary>
		/// Resets the current data provider back to <see cref="CookiesDataStore"/>.
		/// </summary>
		public static void ResetDataStore()
		{
			_cookiesDataStore = new CookiesDataStore();
		}
	}
}
