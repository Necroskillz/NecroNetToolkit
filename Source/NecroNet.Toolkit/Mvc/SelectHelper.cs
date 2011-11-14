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
		                                                        Expression<Func<T, string>> textSelector,
		                                                        Expression<Func<T, object>> valueSelector,
		                                                        bool nullLine = false, string nullLineText = "--")
		{
			var textFunc = textSelector.Compile();
			var valueFunc = valueSelector.Compile();

			var result = new List<SelectListItem>();

			if(nullLine)
			{
				result.Add(new SelectListItem {Text = nullLineText, Value = string.Empty});
			}

			result.AddRange(data.Select(item => new SelectListItem {Text = textFunc(item), Value = valueFunc(item).ToString()}));

			return result;
		}

		public static IList<SelectListItem> ToSelectItemList<T>(this IEnumerable<T> data,
		                                                        Expression<Func<T, string>> textSelector,
		                                                        Expression<Func<T, object>> valueSelector,
																object selectedValue, bool nullLine = false, string nullLineText = "--")
		{
			var textFunc = textSelector.Compile();
			var valueFunc = valueSelector.Compile();
			var result = new List<SelectListItem>();

			if(nullLine)
			{
				result.Add(new SelectListItem {Text = nullLineText, Value = string.Empty});
			}

			result.AddRange(from item in data
			                let value = valueFunc(item)
			                select
			                	new SelectListItem
			                		{Text = textFunc(item), Value = value.ToString(), Selected = value == selectedValue});

			return result;
		}
	}
}