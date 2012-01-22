using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Web.Mvc;

namespace NecroNet.Toolkit.Mvc
{
	public interface ISelectListManager
	{
		IList<SelectListItem> GetItems<T>(string key, object selectedValue = null, Expression<Func<T, bool>> predicate = null);
		IList<SelectListItem> GetItems<T>(object selectedValue = null, Expression<Func<T, bool>> predicate = null);
	}
}