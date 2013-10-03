using System;
using System.Collections.Generic;
using NecroNet.Toolkit.Mvc.Resources;

namespace NecroNet.Toolkit.Mvc
{
	internal class SelectListManagerRegistry : ISelectListManagerRegistry
	{
		private readonly Dictionary<string, dynamic> _selectListSettings = new Dictionary<string, dynamic>();
		
		private bool CheckKey(string key)
		{
			return _selectListSettings.ContainsKey(key);
		}

		public void Add<T, TOrderByProperty>(string key, Func<T, string> textSelector,
			Func<T, object> valueSelector,
			Func<T, TOrderByProperty> orderBySelector,
			Func<T, bool> predicate = null,
			bool nullLine = false, string nullLineText = null,
			SelectListOptimization optimization = SelectListOptimization.None,
			bool noCache = false)
		{
			var selectParams = new SelectListParameters<T, TOrderByProperty>(textSelector, valueSelector, orderBySelector, predicate, nullLine, nullLineText, optimization, noCache);
			var fullKey = GetKey<T>(key);

			if (CheckKey(fullKey))
			{
				throw new InvalidOperationException(string.Format(Res.ExceptionMessage_SelectListManager_AlreadyRegistered, fullKey));
			}

			_selectListSettings.Add(fullKey, selectParams);
		}

		public void Add<T, TOrderByProperty>(Func<T, string> textSelector,
			Func<T, object> valueSelector,
			Func<T, TOrderByProperty> orderBySelector,
			Func<T, bool> predicate = null,
			bool nullLine = false, string nullLineText = null,
			SelectListOptimization optimization = SelectListOptimization.None,
			bool noCache = false)
		{
			Add(null, textSelector, valueSelector, orderBySelector, predicate, nullLine, nullLineText, optimization, noCache);
		}

		public void Add<T>(string key, Func<T, string> textSelector,
			Func<T, object> valueSelector,
			Func<T, bool> predicate = null,
			bool nullLine = false, string nullLineText = null,
			SelectListOptimization optimization = SelectListOptimization.None,
			bool noCache = false)
		{
			var selectParams = new SelectListParameters<T, object>(textSelector, valueSelector, predicate, nullLine, nullLineText, optimization, noCache);
			var fullKey = GetKey<T>(key);

			if (CheckKey(fullKey))
			{
				throw new InvalidOperationException(string.Format(Res.ExceptionMessage_SelectListManager_AlreadyRegistered, fullKey));
			}

			_selectListSettings.Add(fullKey, selectParams);
		}

		public void Add<T>(Func<T, string> textSelector,
			Func<T, object> valueSelector,
			Func<T, bool> predicate = null,
			bool nullLine = false, string nullLineText = null,
			SelectListOptimization optimization = SelectListOptimization.None,
			bool noCache = false)
		{
			Add(null, textSelector, valueSelector, predicate, nullLine, nullLineText, optimization, noCache);
		}

		public void Remove<T>(string key = null)
		{
			var fullKey = GetKey<T>(key);

			_selectListSettings.Remove(fullKey);
		}

		public string GetKey<T>(string key = null)
		{
			return typeof(T).FullName + key;
		}

		public dynamic GetSettings(string fullKey)
		{
			if (!CheckKey(fullKey))
			{
				throw new InvalidOperationException(string.Format(Res.ExceptionMessage_SelectListManager_NotRegistered, fullKey));
			}

			return _selectListSettings[fullKey];
		}
	}
}