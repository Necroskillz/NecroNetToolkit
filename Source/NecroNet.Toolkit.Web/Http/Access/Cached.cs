using System;
using System.Collections.Generic;
using System.Linq;

namespace NecroNet.Toolkit.Web.Http
{
	/// <summary>
	/// Helper class for storing and retrieving data from asp.net cache (<c>HttpContext.Current.Cache</c>).
	/// </summary>
	public class Cached
	{
		private static IHttpCacheDataStore _cacheDataStore = new CacheDataStore();

		/// <summary>
		/// Accesses data through current data provider (default is <see cref="CacheDataStore"/>).
		/// </summary>
		public static IHttpCacheDataStore Data
		{
			get
			{
				return _cacheDataStore;
			}
		}

		/// <summary>
		/// Changes current data provider. Can be used for test to provide stub implementation.
		/// </summary>
		/// <param name="dataStore">The data provider implementation to use.</param>
		public static void ChangeDataStore(IHttpCacheDataStore dataStore)
		{
			_cacheDataStore = dataStore;
		}

		/// <summary>
		/// Resets the current data provider back to <see cref="CacheDataStore"/>.
		/// </summary>
		public static void ResetDataStore()
		{
			_cacheDataStore = new CacheDataStore();
		}
	}
}