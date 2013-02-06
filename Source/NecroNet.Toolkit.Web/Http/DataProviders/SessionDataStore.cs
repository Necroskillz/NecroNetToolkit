using System.Collections.Specialized;
using System.Linq;
using System.Collections.Generic;
using System;
using System.Web;

namespace NecroNet.Toolkit.Web.Http
{
	internal class SessionDataStore : IHttpGeneralDataStore
	{
		private const string StoreKey = "SessionStore.Key";

		private static HybridDictionary Store
		{
			get
			{
				var hashtable = HttpContext.Current.Session[StoreKey] as HybridDictionary;
				if(hashtable == null)
				{
					hashtable = new HybridDictionary();
					HttpContext.Current.Session[StoreKey] = hashtable;
				}
				return hashtable;
			}
		}

		public bool Contains(object key)
		{
			return Store.Contains(key);
		}

		public void Clear()
		{
			Store.Clear();
		}

		public T Get<T>(object key)
		{
			var value = Store[key];

			return value == null ? default(T) : (T)value;
		}

		public object Get(object key)
		{
			return Store[key];
		}

		public void Set(object key, object value)
		{
			Store[key] = value;
		}
	}
}