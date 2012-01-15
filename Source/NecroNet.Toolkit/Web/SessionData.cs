using System;
using System.Collections;
using System.Web;

namespace NecroNet.Toolkit
{
	public static class Session
	{
		private static ILocalDataProvider _sessionDataProvider = new SessionDataProvider();

		public static ILocalDataProvider Data
		{
			get
			{
				return _sessionDataProvider;
			}
		}

		public static void ChangeContext(ILocalDataProvider dataProvider)
		{
			_sessionDataProvider = dataProvider;
		}

		public class SessionDataProvider : ILocalDataProvider
		{
			private const string LocalDataHashtableKey = "SessionHashtable.Key";

			private static Hashtable LocalHashtable
			{
				get
				{
					var hashtable = HttpContext.Current.Session[LocalDataHashtableKey] as Hashtable;
					if(hashtable == null)
					{
						hashtable = new Hashtable();
						HttpContext.Current.Session[LocalDataHashtableKey] = hashtable;
					}
					return hashtable;
				}
			}

			public object this[object key]
			{
				get
				{
					return LocalHashtable[key];
				}
				set
				{
					LocalHashtable[key] = value;
				}
			}

			public int Count
			{
				get
				{
					return LocalHashtable.Count;
				}
			}

			public void Clear()
			{
				LocalHashtable.Clear();
			}

			public bool Contains(object key)
			{
				return LocalHashtable.ContainsKey(key);
			}
		}
	}
}
