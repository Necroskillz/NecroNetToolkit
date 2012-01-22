using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Web.Mvc;

namespace NecroNet.Toolkit.Mvc
{
	public interface ISelectListManager
	{
		/// <summary>
		/// Gets a select list for items of the specified type with the specified selected value, filtered by the specified predicate, using the configuration associated with the specified key.
		/// </summary>
		/// <typeparam name="T">The type of items to generate the select list from.</typeparam>
		/// <param name="key">The key that was used to register <see cref="SelectListManager"/> cofiguration.</param>
		/// <param name="selectedValue">The value that is currently selected in the list.</param>
		/// <param name="predicate">The predicate to filter the collection.</param>
		IList<SelectListItem> GetItems<T>(string key, object selectedValue = null, Expression<Func<T, bool>> predicate = null);

		/// <summary>
		/// Gets a select list for items of the specified type with the specified selected value, filtered by the specified predicate.
		/// </summary>
		/// <typeparam name="T">The type of items to generate the select list from.</typeparam>
		/// <param name="selectedValue">The value that is currently selected in the list.</param>
		/// <param name="predicate">The predicate to filter the collection.</param>
		IList<SelectListItem> GetItems<T>(object selectedValue = null, Expression<Func<T, bool>> predicate = null);
	}
}