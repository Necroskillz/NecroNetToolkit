using System;
using System.Collections.Generic;
using System.Linq;

namespace NecroNet.Toolkit.Mvc
{
	public interface ISelectListManagerRegistry
	{
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
		/// <param name="optimization">A stategy by which the select list is optimized.</param>
		/// <param name="noCache">True means the collection won't be cached.</param>
		void Add<T, TOrderByProperty>(string key, Func<T, string> textSelector,
			Func<T, object> valueSelector,
			Func<T, TOrderByProperty> orderBySelector,
			Func<T, bool> predicate = null,
			bool nullLine = false, string nullLineText = null,
			SelectListOptimization optimization = SelectListOptimization.None,
			bool noCache = false);

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
		/// <param name="optimization">A stategy by which the select list is optimized.</param>
		/// <param name="noCache">True means the collection won't be cached.</param>
		void Add<T, TOrderByProperty>(Func<T, string> textSelector,
			Func<T, object> valueSelector,
			Func<T, TOrderByProperty> orderBySelector,
			Func<T, bool> predicate = null,
			bool nullLine = false, string nullLineText = null,
			SelectListOptimization optimization = SelectListOptimization.None,
			bool noCache = false);

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
		/// <param name="optimization">A stategy by which the select list is optimized.</param>
		/// <param name="noCache">True means the collection won't be cached.</param>
		void Add<T>(string key, Func<T, string> textSelector,
			Func<T, object> valueSelector,
			Func<T, bool> predicate = null,
			bool nullLine = false, string nullLineText = null,
			SelectListOptimization optimization = SelectListOptimization.None,
			bool noCache = false);

		/// <summary>
		/// Registers a select list configuration with the <see cref="SelectListManager"/>.
		/// </summary>
		/// <typeparam name="T">The type of items to generate select list from.</typeparam>
		/// <param name="textSelector">A delegate that selects the text displayed on the drop down list.</param>
		/// <param name="valueSelector">A delegate that selects underlying value for the drop down list.</param>
		/// <param name="predicate">A predicate used to filter the collection.</param>
		/// <param name="nullLine">Whether to insert an empty line (meaning no value selected).</param>
		/// <param name="nullLineText">The text of the empty line.</param>
		/// <param name="optimization">A stategy by which the select list is optimized.</param>
		/// <param name="noCache">True means the collection won't be cached.</param>
		void Add<T>(Func<T, string> textSelector,
			Func<T, object> valueSelector,
			Func<T, bool> predicate = null,
			bool nullLine = false, string nullLineText = null,
			SelectListOptimization optimization = SelectListOptimization.None,
			bool noCache = false);

		void Remove<T>(string key = null);
	}
}