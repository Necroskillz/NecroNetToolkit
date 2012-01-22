using System.Linq;
using System.Collections.Generic;
using System;

namespace NecroNet.Toolkit.Http
{
	/// <summary>
	/// Helper class for storing and retrieving data from http items (<c>HttpContext.Current.Items</c>).
	/// </summary>
	public static class Local
	{
		private static IHttpGeneralDataStore _localDataStore = new LocalDataStore();

		/// <summary>
		/// Accesses data through current data provider (default is <see cref="LocalDataStore"/>).
		/// </summary>
		public static IHttpGeneralDataStore Data
		{
			get
			{
				return _localDataStore;
			}
		}

		/// <summary>
		/// Changes current data provider. Can be used for test to provide stub implementation.
		/// </summary>
		/// <param name="dataStore">The data provider implementation to use.</param>
		public static void ChangeDataStore(IHttpGeneralDataStore dataStore)
		{
			_localDataStore = dataStore;
		}

		/// <summary>
		/// Resets the current data provider back to <see cref="LocalDataStore"/>.
		/// </summary>
		public static void ResetDataStore()
		{
			_localDataStore = new LocalDataStore();
		}
	}
}