using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web.Mvc;
using NecroNet.Toolkit.Configuration;

namespace NecroNet.Toolkit.Mvc
{
	public static class SelectHelper
	{
		/// <summary>
		/// Converts an enumerable of items to list of <see cref="SelectListItem"/> that can be used for drop down lists.
		/// </summary>
		/// <typeparam name="T">The type of items</typeparam>
		/// <param name="data">The data source.</param>
		/// <param name="textSelector">A delegate that selects the text displayed on the drop down list.</param>
		/// <param name="valueSelector">A delegate that selects underlying value for the drop down list.</param>
		/// <param name="nullLine">Whether to insert an empty line (meaning no value selected).</param>
		/// <param name="nullLineText">The text of the empty line. If null, uses value from web.config. If web.config value is not set, uses "--".</param>
		public static IList<SelectListItem> ToSelectItemList<T>(this IEnumerable<T> data,
		                                                        Func<T, string> textSelector,
		                                                        Func<T, object> valueSelector,
		                                                        bool nullLine = false, string nullLineText = null)
		{
			var result = new List<SelectListItem>();

			if(nullLine)
			{
				result.Add(new SelectListItem {Text = nullLineText ?? NecroNetToolkitConfigurationManager.GetOption(c => c.SelectListManager.NullLineText), Value = string.Empty});
			}

			result.AddRange(data.Select(item => new SelectListItem { Text = textSelector(item), Value = valueSelector(item).ToString() }));

			return result;
		}

		/// <summary>
		/// Converts an enumerable of items to list of <see cref="SelectListItem"/> with the specified selected value that can be used for drop down lists.
		/// </summary>
		/// <typeparam name="T">The type of items</typeparam>
		/// <param name="data">The data source.</param>
		/// <param name="textSelector">A delegate that selects the text displayed on the drop down list.</param>
		/// <param name="valueSelector">A delegate that selects underlying value for the drop down list.</param>
		/// <param name="selectedValue">The value that is selected.</param>
		/// <param name="nullLine">Whether to insert an empty line (meaning no value selected).</param>
		/// <param name="nullLineText">The text of the empty line. If null, uses value from web.config. If web.config value is not set, uses "--".</param>
		public static IList<SelectListItem> ToSelectItemList<T>(this IEnumerable<T> data,
																Func<T, string> textSelector,
																Func<T, object> valueSelector,
																object selectedValue, bool nullLine = false, string nullLineText = null)
		{
			var result = new List<SelectListItem>();

			if(nullLine)
			{
				result.Add(new SelectListItem {Text = nullLineText ?? NecroNetToolkitConfigurationManager.GetOption(c => c.SelectListManager.NullLineText), Value = string.Empty});
			}

			// seems like the best and most convenient way is to compare values as strings, since boxed value types don't compare well
			// e.g. ((object)(int)1).Equals((object)(short)1) == false
			var selectedValueString = selectedValue.ToString();

			result.AddRange(from item in data
							let value = valueSelector(item).ToString()
			                select
			                	new SelectListItem { Text = textSelector(item), Value = value, Selected = string.Equals(value, selectedValueString) });

			return result;
		}

		public static IList<SelectListItem> SetSelectedSingle(this IList<SelectListItem> selectList, object selectedValue)
		{
			var selectedValueString = selectedValue.ToString();

			selectList.Each(i =>
			                	{
									if (i.Value == selectedValueString)
									{
										i.Selected = true;
										return LoopContinuation.Break;
									}

			                		return LoopContinuation.Continue;
			                	});

			return selectList;
		}

		public static IList<SelectListItem> SetSelectedMultiple(this IList<SelectListItem> selectList, Func<string, bool> isSelectedFunc)
		{
			selectList.Each(i => i.Selected = isSelectedFunc(i.Value));

			return selectList;
		}
	}
}