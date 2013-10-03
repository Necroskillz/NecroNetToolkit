using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web.Mvc;
using NecroNet.Toolkit.Mvc.Resources;

namespace NecroNet.Toolkit.Mvc
{
	public class SelectListManager : ISelectListManager
	{
		private static SelectListManagerRegistry _registry;

		public static ISelectListManagerRegistry Registry
		{
			get
			{
				return _registry;
			}
		}

		private ISelectListManagerCacheProvider _cacheProvider;
		private ISelectListManagerDataProvider _dataProvider;

		internal static SelectListManagerConfiguration Configuration = SelectListManagerConfiguration.Default;

		private ISelectListManagerCacheProvider CacheProvider
		{
			get { return _cacheProvider ?? (_cacheProvider = (ISelectListManagerCacheProvider)Activator.CreateInstance(Configuration.CacheProviderType)); }
		}

		private ISelectListManagerDataProvider DataProvider
		{
			get { return _dataProvider ?? (_dataProvider = Configuration.DataProviderFactory()); }
		}

		private static bool _initialized;

		public static void Initialize(Action<ISelectListManagerInitializator> action)
		{
			var initializator = new SelectListManagerInitializator();
			action(initializator);

			Configuration = initializator.GetConfiguration();
			_registry = initializator.GetRegistry();

			_initialized = true;
		}
		
		private IEnumerable<T> GetData<T>() where T : class
		{
			return DataProvider.GetData<T>();
		}

		private static IEnumerable<T> ApplyBaseFilterAndOrder<T, TOrderByProperty>(IEnumerable<T> data, SelectListParameters<T, TOrderByProperty> selectParams)
				where T : class
		{
			if (selectParams.Predicate != null)
			{
				data = data.Where(selectParams.Predicate);
			}

			if (selectParams.OrderBySelector != null)
			{
				data = data.OrderBy(selectParams.OrderBySelector);
			}

			return data;
		}

		private IEnumerable<SelectListItem> CompileSelectList<T, TOrderByProperty>(string key, IEnumerable<T> data, SelectListParameters<T, TOrderByProperty> selectParams, object selectedValue, Func<T, bool> predicate) where T : class
		{
			if (data == null)
			{
				if (selectParams.ForceNoCache)
				{
					data = GetData<T>();

					data = ApplyBaseFilterAndOrder(data, selectParams);
				}
				else
				{
					var cached = CacheProvider.Get<T>(key);
					if (cached != null)
					{
						data = cached;
					}
					else
					{
						data = GetData<T>();

						data = ApplyBaseFilterAndOrder(data, selectParams);

						_cacheProvider.Set(key, data.ToList(), Configuration.CacheDuration);
					}
				}
			}
			else
			{
				data = ApplyBaseFilterAndOrder(data, selectParams);
			}

			if (predicate != null)
			{
				data = data.Where(predicate);
			}

			var selectList = selectedValue != null ?
				data.ToSelectItemList(selectParams.TextSelector, selectParams.ValueSelector, selectedValue, selectParams.NullLine, selectParams.NullLineText) :
				data.ToSelectItemList(selectParams.TextSelector, selectParams.ValueSelector, selectParams.NullLine, selectParams.NullLineText);

			var optimalization = selectParams.Optimization;

			Func<bool> optimalizationCondition = () => false;
			Action optimalizationAction = () => { };

			switch (optimalization)
			{
				case SelectListOptimization.None:
					break;
				case SelectListOptimization.DeleteNullLineIfSelectListHasOneItemAndNullLine:
					optimalizationCondition = () => selectParams.NullLine && selectList.Count == 2;
					optimalizationAction = () => selectList.RemoveAt(0);
					break;
				case SelectListOptimization.PreselectIfSelectListHasOneItemAndNullLine:
					optimalizationCondition = () => selectParams.NullLine && selectList.Count == 2;
					optimalizationAction = () => selectList[1].Selected = true;
					break;
				case SelectListOptimization.AlwaysPreselectFirstItem:
					optimalizationCondition = () => selectParams.NullLine && selectList.Count >= 2;
					optimalizationAction = () => selectList[1].Selected = true;
					break;
			}

			if (optimalizationCondition())
			{
				optimalizationAction();
			}

			return selectList;
		}

		private IList<SelectListItem> CompileSelectList<T>(string key, IEnumerable<T> data, object selectedValue, Func<T, bool> predicate)
		{
			var fullKey = _registry.GetKey<T>(key);

			var selectParams = _registry.GetSettings(fullKey);

			return CompileSelectList(fullKey, data, selectParams, selectedValue, predicate);
		}

		public IList<SelectListItem> GetItems<T>(string key, object selectedValue, Func<T, bool> predicate, IEnumerable<T> customData)
		{
			if (!_initialized)
			{
				throw new InvalidOperationException(Res.ExceptionMessage_SelectListManager_NotInitialized);
			}

			return CompileSelectList(key, customData, selectedValue, predicate);
		}

		public IList<SelectListItem> GetItems<T>(object selectedValue, Func<T, bool> predicate, IEnumerable<T> customData)
		{
			return GetItems(null, selectedValue, predicate, customData);
		}
	}
}
