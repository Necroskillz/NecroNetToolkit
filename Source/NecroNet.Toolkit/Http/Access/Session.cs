using System.Linq;
using System.Collections.Generic;
using System;

namespace NecroNet.Toolkit.Http
{
	/// <summary>
	/// Helper class for storing and retrieving data from http session (<c>HttpContext.Current.Session</c>).
	/// </summary>
	public static class Session
	{
		private static IHttpGeneralDataStore _sessionDataStore = new SessionDataStore();

		/// <summary>
		/// Accesses data through current data provider (default is <see cref="SessionDataStore"/>).
		/// </summary>
		public static IHttpGeneralDataStore Data
		{
			get
			{
				return _sessionDataStore;
			}
		}

		/// <summary>
		/// Changes current data provider. Can be used for test to provide stub implementation.
		/// </summary>
		/// <param name="dataStore">The data provider implementation to use.</param>
		public static void ChangeDataStore(IHttpGeneralDataStore dataStore)
		{
			_sessionDataStore = dataStore;
		}

		/// <summary>
		/// Resets the current data provider back to <see cref="SessionDataStore"/>.
		/// </summary>
		public static void ResetDataStore()
		{
			_sessionDataStore = new SessionDataStore();
		}
	}
}
