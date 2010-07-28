using System.Web.Mvc;
using System.Web.Routing;

namespace NecroNet.Toolkit.Mvc
{
	public static class RouteCollectionExtensions
	{
		public static LowercaseRoute MapLowercaseRoute(this RouteCollection routes, string name, string url, object defaults = null, object constraints = null, string[] namespaces = null)
		{
			Guard.IsNotNull(routes, "routes");
			Guard.IsNotNull(url, "url");

			var route = new LowercaseRoute(url, new MvcRouteHandler())
			            	{
			            		Defaults = new RouteValueDictionary(defaults),
			            		Constraints = new RouteValueDictionary(constraints),
			            		DataTokens = new RouteValueDictionary()
			            	};

			if(namespaces != null && namespaces.Length > 0)
			{
				route.DataTokens["Namespaces"] = namespaces;
			}

			routes.Add(name, route);

			return route;
		}
	}
}