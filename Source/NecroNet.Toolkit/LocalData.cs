using System;
using System.Collections;
using System.Web;

namespace NecroNet.Toolkit
{
	public static class Local
	{
		private static ILocalData _data = new LocalData();

		public static ILocalData Data
		{
			get
			{
				return _data;
			}
		}

		public static void ChangeContext(ILocalData localData)
		{
			_data = localData;
		}

		internal class LocalData : ILocalData
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