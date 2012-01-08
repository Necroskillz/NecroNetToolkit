using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web.Mvc;

namespace NecroNet.Toolkit.Mvc
{
	public static class SelectHelper
	{
		public static IList<SelectListItem> ToSelectItemList<T>(this IEnumerable<T> data,
		                                                        Func<T, string> textSelector,
		                                                        Func<T, object> valueSelector,
		                                                        bool nullLine = false, string nullLineText = "--")
		{
			var result = new List<SelectListItem>();

			if(nullLine)
			{
				result.Add(new SelectListItem {Text = nullLineText, Value = string.Empty});
			}

			result.AddRange(data.Select(item => new SelectListItem { Text = textSelector(item), Value = valueSelector(item).ToString() }));

			return result;
		}

		public static IList<SelectListItem> ToSelectItemList<T, TValue>(this IEnumerable<T> data,
																		Func<T, string> textSelector,
																		Func<T, TValue> valueSelector,
																		TValue selectedValue, bool nullLine = false, string nullLineText = "--")
		{
			var result = new List<SelectListItem>();

			if(nullLine)
			{
				result.Add(new SelectListItem {Text = nullLineText, Value = string.Empty});
			}

			result.AddRange(from item in data
							let value = valueSelector(item)
			                select
			                	new SelectListItem { Text = textSelector(item), Value = value.ToString(), Selected = value.Equals(selectedValue) });

			return result;
		}
	}
}