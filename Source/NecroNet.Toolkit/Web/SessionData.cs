using System;
using System.Collections;
using System.Collections.Specialized;
using System.Web;

namespace NecroNet.Toolkit
{
	public static class Session
	{
		private static IHttpGeneralDataProvider _sessionDataProvider = new SessionDataProvider();

		public static IHttpGeneralDataProvider Data
		{
			get
			{
				return _sessionDataProvider;
			}
		}

		public static void ChangeContext(IHttpGeneralDataProvider dataProvider)
		{
			_sessionDataProvider = dataProvider;
		}

		public class SessionDataProvider : IHttpGeneralDataProvider
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
}
