using System.Collections.Generic;
using System.Web.Mvc;
using MvcTurbine.Web.Filters;
using Filter = MvcTurbine.Web.Filters.Filter;

namespace NecroNetToolkit.Web.Test.Filters
{
	public class FilterRegistry : IFilterRegistry
	{
		public IEnumerable<Filter> GetFilterRegistrations()
		{
			yield return new GlobalFilter {FilterType = typeof(HandleErrorAttribute)};
		}
	}
}