using System.Linq;
using System;
using System.Collections.Generic;
using System.Web;

namespace NecroNet.Toolkit.Http
{
	public interface IHttpCookiesDataStore : IHttpDataStore
	{
		/// <summary>
		/// Determines whether the data store contains a specific key.
		/// </summary>
		/// <param name="key">The key to locate.</param>
		bool Contains(string key);

		/// <summary>
		/// Retrieves a value associated with the specified key from the data store. If the value is not present, returns <c>null</c>.
		/// </summary>
		/// <param name="key">The key whose value to retrieve.</param>
		string Get(string key);

		/// <summary>
		/// Stores the specified value with the specified expiry date and associates it with the specified key.
		/// </summary>
		/// <param name="key">The key whose value to store.</param>
		/// <param name="value">The value to store.</param>
		/// <param name="expires">The date on which the value expires.</param>
		void Set(string key, string value, DateTime? expires = null);

		/// <summary>
		/// Retrieves a <see cref="HttpCookie"/> associated with the specified key from the data store. If the value is not present, returns <c>null</c>.
		/// </summary>
		/// <param name="key">The key whose <see cref="HttpCookie"/> to retrieve.</param>
		HttpCookie GetCookie(string key);

		/// <summary>
		/// Stores the specified <see cref="HttpCookie"/> and associates it with the specified key.
		/// </summary>
		/// <param name="cookie"></param>
		void SetCookie(HttpCookie cookie);
	}
}