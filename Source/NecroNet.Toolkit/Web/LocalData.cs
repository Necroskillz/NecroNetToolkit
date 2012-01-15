using System;
using System.Collections;
using System.Web;

namespace NecroNet.Toolkit
{
	public static class Local
	{
		private static ILocalDataProvider _data = new LocalDataProvider();

		public static ILocalDataProvider Data
		{
			get
			{
				return _data;
			}
		}

		public static void ChangeContext(ILocalDataProvider dataProvider)
		{
			_data = dataProvider;
		}

		public class LocalDataProvider : ILocalDataProvider
		{
			[ThreadStatic]
			private static Hashtable _localData;

			private static readonly object LocalDataHashtableKey = new object();

			private static Hashtable LocalHashtable
			{
				get
				{
					if(!RunningInWeb)
					{
						return _localData ?? (_localData = new Hashtable());
					}

					var webHashtable = HttpContext.Current.Items[LocalDataHashtableKey] as Hashtable;
					if(webHashtable == null)
					{
						webHashtable = new Hashtable();
						HttpContext.Current.Items[LocalDataHashtableKey] = webHashtable;
					}
					return webHashtable;
				}
			}

			private static bool RunningInWeb
			{
				get
				{
					return HttpContext.Current != null;
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