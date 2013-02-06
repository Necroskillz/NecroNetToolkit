using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web.Caching;
using System.Web.Mvc;
using NecroNet.Toolkit.Core.Resources;
using NecroNet.Toolkit.Web.Http;

namespace NecroNet.Toolkit.Web.Mvc
{
	public abstract class SelectListManager : ISelectListManager
	{
		private static readonly Dictionary<string, dynamic> SelectListSettings = new Dictionary<string, dynamic>();

		private static bool CheckKey(string key)
		{
			return SelectListSettings.ContainsKey(key);
		}

		/// <summary>
		/// Registers a select list configuration with the <see cref="SelectListManager"/>.
		/// </summary>
		/// <typeparam name="T">The type of items to generate select list from.</typeparam>
		/// <typeparam name="TOrderByProperty">The type of property to order by.</typeparam>
		/// <param name="key">The key to associate with this specific registration. Use to differentiate between configurations for the same item type.</param>
		/// <param name="textSelector">A delegate that selects the text displayed on the drop down list.</param>
		/// <param name="valueSelector">A delegate that selects underlying value for the drop down list.</param>
		/// <param name="orderBySelector">A selector of property used for ordering.</param>
		/// <param name="predicate">A predicate used to filter the collection.</param>
		/// <param name="nullLine">Whether to insert an empty line (meaning no value selected).</param>
		/// <param name="nullLineText">The text of the empty line.</param>
		/// <param name="optimalization">A stategy by which the select list is optimized.</param>
		/// <param name="noCache">True means the collection won't be cached.</param>
		public static void Register<T, TOrderByProperty>(string key, Func<T, string> textSelector,
														 Func<T, object> valueSelector,
														 Expression<Func<T, TOrderByProperty>> orderBySelector,
														 Expression<Func<T, bool>> predicate = null,
														 bool nullLine = false, string nullLineText = null, SelectListOptimalization optimalization = SelectListOptimalization.None, bool noCache = false)
		{
			var selectParams = new SelectListParameters<T, TOrderByProperty>(textSelector, valueSelector, orderBySelector, predicate, nullLine, nullLineText, optimalization, noCache);
			var fullKey = GetKey<T>(key);

			if(CheckKey(fullKey))
			{
				Throw.New<InvalidOperationException>(Res.ExceptionMessage_SelectListManager_AlreadyRegistered, fullKey);
			}

			SelectListSettings.Add(fullKey, selectParams);
		}

		/// <summary>
		/// Registers a select list configuration with the <see cref="SelectListManager"/>.
		/// </summary>
		/// <typeparam name="T">The type of items to generate select list from.</typeparam>
		/// <typeparam name="TOrderByProperty">The type of property to order by.</typeparam>
		/// <param name="textSelector">A delegate that selects the text displayed on the drop down list.</param>
		/// <param name="valueSelector">A delegate that selects underlying value for the drop down list.</param>
		/// <param name="orderBySelector">A selector of property used for ordering.</param>
		/// <param name="predicate">A predicate used to filter the collection.</param>
		/// <param name="nullLine">Whether to insert an empty line (meaning no value selected).</param>
		/// <param name="nullLineText">The text of the empty line.</param>
		/// <param name="optimalization">A stategy by which the select list is optimized.</param>
		/// <param name="noCache">True means the collection won't be cached.</param>
		public static void Register<T, TOrderByProperty>(Func<T, string> textSelector,
														 Func<T, object> valueSelector,
														 Expression<Func<T, TOrderByProperty>> orderBySelector,
														 Expression<Func<T, bool>> predicate = null,
														 bool nullLine = false, string nullLineText = null, SelectListOptimalization optimalization = SelectListOptimalization.None, bool noCache = false)
		{
			Register(null, textSelector, valueSelector, orderBySelector, predicate, nullLine, nullLineText, optimalization, noCache);
		}

		/// <summary>
		/// Registers a select list configuration with the <see cref="SelectListManager"/>.
		/// </summary>
		/// <typeparam name="T">The type of items to generate select list from.</typeparam>
		/// <param name="key">The key to associate with this specific registration. Use to differentiate between configurations for the same item type.</param>
		/// <param name="textSelector">A delegate that selects the text displayed on the drop down list.</param>
		/// <param name="valueSelector">A delegate that selects underlying value for the drop down list.</param>
		/// <param name="predicate">A predicate used to filter the collection.</param>
		/// <param name="nullLine">Whether to insert an empty line (meaning no value selected).</param>
		/// <param name="nullLineText">The text of the empty line.</param>
		/// <param name="optimalization">A stategy by which the select list is optimized.</param>
		/// <param name="noCache">True means the collection won't be cached.</param>
		public static void Register<T>(string key, Func<T, string> textSelector,
									   Func<T, object> valueSelector,
									   Expression<Func<T, bool>> predicate = null,
									   bool nullLine = false, string nullLineText = null, SelectListOptimalization optimalization = SelectListOptimalization.None, bool noCache = false)
		{
			var selectParams = new SelectListParameters<T, object>(textSelector, valueSelector, predicate, nullLine, nullLineText, optimalization, noCache);
			var fullKey = GetKey<T>(key);

			if(CheckKey(key))
			{
				Throw.New<InvalidOperationException>(Res.ExceptionMessage_SelectListManager_AlreadyRegistered, fullKey);
			}

			SelectListSettings.Add(fullKey, selectParams);
		}

		/// <summary>
		/// Registers a select list configuration with the <see cref="SelectListManager"/>.
		/// </summary>
		/// <typeparam name="T">The type of items to generate select list from.</typeparam>
		/// <param name="textSelector">A delegate that selects the text displayed on the drop down list.</param>
		/// <param name="valueSelector">A delegate that selects underlying value for the drop down list.</param>
		/// <param name="predicate">A predicate used to filter the collection.</param>
		/// <param name="nullLine">Whether to insert an empty line (meaning no value selected).</param>
		/// <param name="nullLineText">The text of the empty line.</param>
		/// <param name="optimalization">A stategy by which the select list is optimized.</param>
		/// <param name="noCache">True means the collection won't be cached.</param>
		public static void Register<T>(Func<T, string> textSelector,
									   Func<T, object> valueSelector,
									   Expression<Func<T, bool>> predicate = null,
									   bool nullLine = false, string nullLineText = null, SelectListOptimalization optimalization = SelectListOptimalization.None, bool noCache = false)
		{
			Register(null, textSelector, valueSelector, predicate, nullLine, nullLineText, optimalization, noCache);
		}

		private static string GetKey<T>(string key = null)
		{
			return typeof(T).FullName + key;
		}

		protected abstract IQueryable<T> GetQuery<T>(); 

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
					query = GetQuery<T>();

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
						query = GetQuery<T>();

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

			var optimalization = selectParams.Optimalization;

			Func<bool> optimalizationCondition = () => false;
			Action optimalizationAction = () => { };

			switch (optimalization)
			{
				case SelectListOptimalization.None:
					break;
				case SelectListOptimalization.DeleteNullLineIfSelectListHasOneItemAndNullLine:
					optimalizationCondition = () => selectParams.NullLine && selectList.Count == 2;
					optimalizationAction = () => selectList.RemoveAt(0);
					break;
				case SelectListOptimalization.PreselectIfSelectListHasOneItemAndNullLine:
					optimalizationCondition = () => selectParams.NullLine && selectList.Count == 2;
					optimalizationAction = () => selectList[1].Selected = true;
					break;
				case SelectListOptimalization.AlwaysPreselectFirstItem:
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

		private IList<SelectListItem> CompileSelectList<T>(string key, IQueryable<T> query, object selectedValue, Expression<Func<T, bool>> predicate)
		{
			var fullKey = GetKey<T>(key);

			if(!CheckKey(fullKey))
			{
				Throw.New<InvalidOperationException>(Res.ExceptionMessage_SelectListManager_NotRegistered, fullKey);
			}

			var selectParams = SelectListSettings[fullKey];

			return CompileSelectList(fullKey, query, selectParams, selectedValue, predicate);
		}

		public IList<SelectListItem> GetItems<T>(string key, object selectedValue, Expression<Func<T, bool>> predicate, IQueryable<T> customQuery)
		{
			return CompileSelectList(key, customQuery, selectedValue, predicate);
		}

		public IList<SelectListItem> GetItems<T>(object selectedValue, Expression<Func<T, bool>> predicate, IQueryable<T> customQuery)
		{
			return GetItems(null, selectedValue, predicate, customQuery);
		}
	}
}
