using System.Web.Routing;

namespace NecroNet.Toolkit.Mvc
{
	public class LowercaseRoute : Route
	{
		public LowercaseRoute(string url, IRouteHandler routeHandler)
			: base(url, routeHandler) { }
		public LowercaseRoute(string url, RouteValueDictionary defaults, IRouteHandler routeHandler)
			: base(url, defaults, routeHandler) { }
		public LowercaseRoute(string url, RouteValueDictionary defaults, RouteValueDictionary constraints, IRouteHandler routeHandler)
			: base(url, defaults, constraints, routeHandler) { }
		public LowercaseRoute(string url, RouteValueDictionary defaults, RouteValueDictionary constraints, RouteValueDictionary dataTokens, IRouteHandler routeHandler)
			: base(url, defaults, constraints, dataTokens, routeHandler) { }

		public override VirtualPathData GetVirtualPath(RequestContext requestContext, RouteValueDictionary values)
		{
			if(values.ContainsKey("Area"))
			{
				values["Area"] = ((string)values["Area"]).ToLowerInvariant();
			}

			if(values.ContainsKey("Controller"))
			{
				values["Controller"] = ((string)values["Controller"]).ToLowerInvariant();
			}

			if(values.ContainsKey("Action"))
			{
				values["Action"] = ((string)values["Action"]).ToLowerInvariant();
			}

			return base.GetVirtualPath(requestContext, values);
		}
	}
}