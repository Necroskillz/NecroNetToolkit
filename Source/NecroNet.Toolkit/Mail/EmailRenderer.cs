// This code is courtesy of https://github.com/andrewdavey/postal

using System;
using System.IO;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using NecroNet.Toolkit.Configuration;

namespace NecroNet.Toolkit.Mail
{
	internal class EmailRenderer : IEmailRenderer
	{
		public EmailRenderer(ViewEngineCollection viewEngines, Uri url)
		{
			_viewEngines = viewEngines;
			if (url != null)
			{
				_getUrl = () => url;
			}
			else
			{
				// Delay asking for Url from HttpContext since EmailViewRenderer may be
				// created before any HttpContext exists.
				_getUrl = () => GetUrlFromHttpContext() ?? DefaultUrlRatherThanNull();
			}

			_emailViewDirectoryName = NecroNetToolkitConfigurationManager.GetOption(c => c.Mail.EmailDirectory);
		}

		readonly ViewEngineCollection _viewEngines;
		readonly Func<Uri> _getUrl;
		private readonly string _emailViewDirectoryName;

		public string Render(string viewName, object model)
		{
			var controllerContext = CreateControllerContext();
			var view = CreateView(viewName, controllerContext);
			var viewData = new ViewDataDictionary(model);
			var viewOutput = RenderView(view, viewData, controllerContext);
			return viewOutput;
		}

		ControllerContext CreateControllerContext()
		{
			var httpContext = new EmailHttpContext(_getUrl());
			var routeData = GetRouteDataFromHttpContext() ?? new RouteData();
			routeData.Values["controller"] = _emailViewDirectoryName;
			var requestContext = new RequestContext(httpContext, routeData);
			return new ControllerContext(requestContext, new StubController());
		}

		IView CreateView(string viewName, ControllerContext controllerContext)
		{
			var result = _viewEngines.FindView(controllerContext, viewName, null);
			if (result.View != null)
				return result.View;

			throw new Exception(
				"Email view not found for " + viewName +
				". Locations searched:" + Environment.NewLine +
				string.Join(Environment.NewLine, result.SearchedLocations)
			);
		}

		static string RenderView(IView view, ViewDataDictionary viewData, ControllerContext controllerContext)
		{
			using (var writer = new StringWriter())
			{
				var viewContext = new ViewContext(controllerContext, view, viewData, new TempDataDictionary(), writer);
				view.Render(viewContext, writer);
				return writer.GetStringBuilder().ToString();
			}
		}

		static RouteData GetRouteDataFromHttpContext()
		{
			if (HttpContext.Current == null) return null;
			var wrapper = new HttpContextWrapper(HttpContext.Current);
			return RouteTable.Routes.GetRouteData(wrapper);
		}

		static Uri GetUrlFromHttpContext()
		{
			return HttpContext.Current == null ? null : HttpContext.Current.Request.Url;
		}

		static Uri DefaultUrlRatherThanNull()
		{
			return new Uri("http://localhost");
		}

		// StubController so we can create a ControllerContext.
		class StubController : Controller { }
	}
}