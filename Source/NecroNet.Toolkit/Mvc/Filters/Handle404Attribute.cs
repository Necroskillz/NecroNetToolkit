﻿using System;
using System.Reflection;
using System.Web.Mvc;

namespace NecroNet.Toolkit.Mvc
{
	/// <summary>
	/// Represents an attribute that is used to handle a <see cref="Http404Exception"/> thrown by an action method. Returns view named 404 by default, and sets the reponse status code to 404.
	/// </summary>
	public class Handle404Attribute : HandleErrorAttribute
	{
		public Handle404Attribute()
		{
			View = "404";
		}

		public override void OnException(ExceptionContext filterContext)
		{
			var controller = filterContext.Controller as Controller;
			if(controller == null || filterContext.ExceptionHandled)
				return;

			var exception = filterContext.Exception;
			if(exception == null)
				return;

			// Action method exceptions will be wrapped in a
			// TargetInvocationException since they're invoked using
			// reflection, so we have to unwrap it.
			if(exception is TargetInvocationException)
				exception = exception.InnerException;

			if(!(exception is Http404Exception))
				return;

			filterContext.Result = new ViewResult
			                       	{
			                       		TempData = controller.TempData,
			                       		ViewName = View
			                       	};

			filterContext.ExceptionHandled = true;
			filterContext.HttpContext.Response.Clear();
			filterContext.HttpContext.Response.StatusCode = 404;
			filterContext.HttpContext.Response.TrySkipIisCustomErrors = true;
		}
	}
}