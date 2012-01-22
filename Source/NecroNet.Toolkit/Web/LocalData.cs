using System;
using System.Collections;
using System.Collections.Specialized;
using System.Web;

namespace NecroNet.Toolkit
{
	public static class Local
	{
		private static IHttpGeneralDataProvider _data = new LocalDataProvider();

		public static IHttpGeneralDataProvider Data
		{
			get
			{
				return _data;
			}
		}

		public static void ChangeContext(IHttpGeneralDataProvider dataProvider)
		{
			_data = dataProvider;
		}

		public class LocalDataProvider : IHttpGeneralDataProvider
		{
			[ThreadStatic]
			private static HybridDictionary _localStore;

			private static readonly object StoreKey = new object();

			private static HybridDictionary Store
			{
				get
				{
					if(!RunningInWeb)
					{
						return _localStore ?? (_localStore = new HybridDictionary());
					}

					var webHashtable = HttpContext.Current.Items[StoreKey] as HybridDictionary;
					if(webHashtable == null)
					{
						webHashtable = new HybridDictionary();
						HttpContext.Current.Items[StoreKey] = webHashtable;
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