using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web.Caching;
using System.Web.Mvc;
using NecroNet.Toolkit.Configuration;
using NecroNet.Toolkit.Data;
using NecroNet.Toolkit.Internals;
using NecroNet.Toolkit.Resources;

namespace NecroNet.Toolkit.Mvc
{
	public class SelectListManager : ISelectListManager
	{
		private readonly IQueryFactory _queryFactory;

		private static readonly Dictionary<string, dynamic> SelectListSettings = new Dictionary<string, dynamic>();

		public SelectListManager(IQueryFactory queryFactory)
		{
			_queryFactory = queryFactory;
		}

		private static bool CheckKey(string key)
		{
			return SelectListSettings.ContainsKey(key);
		}

		public static void Register<T, TOrderByProperty>(string key, Func<T, string> textSelector,
														 Func<T, object> valueSelector,
														 Expression<Func<T, TOrderByProperty>> orderBySelector,
														 Expression<Func<T, bool>> predicate = null,
														 bool nullLine = false, string nullLineText = "--", bool noCache = false)
		{
			var selectParams = new SelectListParameters<T, TOrderByProperty>(textSelector, valueSelector, orderBySelector, predicate, nullLine, nullLineText, noCache);
			var fullKey = GetKey<T>(key);

			if(CheckKey(fullKey))
			{
				Throw.New<InvalidOperationException>(Res.ExceptionMessage_SelectListManager_AlreadyRegistered, fullKey);
			}

			SelectListSettings.Add(fullKey, selectParams);
		}

		public static void Register<T, TOrderByProperty>(Func<T, string> textSelector,
														 Func<T, object> valueSelector,
														 Expression<Func<T, TOrderByProperty>> orderBySelector,
														 Expression<Func<T, bool>> predicate = null,
														 bool nullLine = false, string nullLineText = "--", bool noCache = false)
		{
			Register(null, textSelector, valueSelector, orderBySelector, predicate, nullLine, nullLineText, noCache);
		}

		public static void Register<T>(string key, Func<T, string> textSelector,
									   Func<T, object> valueSelector,
									   Expression<Func<T, bool>> predicate = null,
									   bool nullLine = false, string nullLineText = "--", bool noCache = false)
		{
			var selectParams = new SelectListParameters<T, object>(textSelector, valueSelector, predicate, nullLine, nullLineText, noCache);
			var fullKey = GetKey<T>(key);

			if(CheckKey(key))
			{
				Throw.New<InvalidOperationException>(Res.ExceptionMessage_SelectListManager_AlreadyRegistered, fullKey);
			}

			SelectListSettings.Add(fullKey, selectParams);
		}

		public static void Register<T>(Func<T, string> textSelector,
									   Func<T, object> valueSelector,
									   Expression<Func<T, bool>> predicate = null,
									   bool nullLine = false, string nullLineText = "--", bool noCache = false)
		{
			Register(null, textSelector, valueSelector, predicate, nullLine, nullLineText, noCache);
		}

		private static string GetKey<T>(string key = null)
		{
			return typeof(T).FullName + key;
		}

		private static IQueryable<T> ProcessQuery<T, TOrderByProperty>(IQueryable<T> query, SelectListParameters<T, TOrderByProperty> selectParams)
			where T : class
		{
			if (selectParams.Predicate != null)
			{
				query = query.Where(selectParams.Predicate);
			}

			if (selectParams.OrderBySelector != null)
			{
				query = query.OrderBy(selectParams.OrderBySelector);
			}

			return query;
		}

		private IEnumerable<SelectListItem> CompileSelectList<T, TOrderByProperty>(string key, IQueryable<T> query, SelectListParameters<T, TOrderByProperty> selectParams, object selectedValue, Expression<Func<T, bool>> predicate) where T : class
		{
			if (query == null)
			{
				if(selectParams.ForceNoCache)
				{
					query = _queryFactory.GetQuery<T>();

					query = ProcessQuery(query, selectParams);
				}
				else
				{
					var cached = Cached.Data.Get<List<T>>(key);
					if (cached != null)
					{
						query = cached.AsQueryable();
					}
					else
					{
						query = _queryFactory.GetQuery<T>();

						query = ProcessQuery(query, selectParams);

						Cached.Data.Set(key, query.ToList(), DateTime.Now.AddMinutes(NecroNetToolkitConfigurationManager.GetOption(c => c.SelectListManager.CacheDuration, 1440)));
					}
				}
			}
			else
			{
				query = ProcessQuery(query, selectParams);
			}

			if (predicate != null)
			{
				query = query.Where(predicate);
			}

			var selectList = selectedValue != null ?
				query.ToSelectItemList(selectParams.TextSelector, selectParams.ValueSelector, selectedValue, selectParams.NullLine, selectParams.NullLineText) :
				query.ToSelectItemList(selectParams.TextSelector, selectParams.ValueSelector, selectParams.NullLine, selectParams.NullLineText);

			// dont display null line if select list contains only 1 item
			if (selectParams.NullLine && selectList.Count == 2)
			{
				selectList.RemoveAt(0);
			}

			return selectList;
		}

		protected IList<SelectListItem> CompileSelectList<T>(string key = null, IQueryable<T> query = null, object selectedValue = null, Expression<Func<T, bool>> predicate = null)
		{
			var fullKey = GetKey<T>(key);

			if(!CheckKey(fullKey))
			{
				Throw.New<InvalidOperationException>(Res.ExceptionMessage_SelectListManager_NotRegistered, fullKey);
			}

			var selectParams = SelectListSettings[fullKey];

			return CompileSelectList(fullKey, query, selectParams, selectedValue, predicate);
		}

		public IList<SelectListItem> GetItems<T>(string key, object selectedValue = null, Expression<Func<T, bool>> predicate = null)
		{
			return CompileSelectList(key, selectedValue: selectedValue, predicate: predicate);
		}

		public IList<SelectListItem> GetItems<T>(object selectedValue = null, Expression<Func<T, bool>> predicate = null)
		{
			return GetItems(null, selectedValue, predicate);
		}
	}
}
