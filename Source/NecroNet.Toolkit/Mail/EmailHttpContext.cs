// This code is courtesy of https://github.com/andrewdavey/postal

using System;
using System.Collections;
using System.Web;
using System.Web.Caching;

namespace NecroNet.Toolkit.Mail
{
	public class EmailHttpContext : HttpContextBase
	{
		public EmailHttpContext(Uri url)
		{
			_items = new Hashtable();
			_request = new EmailHttpRequest(url);
			_response = new EmailHttpResponse();
		}

		readonly Hashtable _items;
		readonly HttpRequestBase _request;
		readonly HttpResponseBase _response;

		public override IDictionary Items { get { return _items; } }
		public override HttpRequestBase Request { get { return _request; } }
		public override HttpResponseBase Response { get { return _response; } }
		public override Cache Cache { get { return HttpRuntime.Cache; } }
		public override HttpServerUtilityBase Server { get { return new HttpServerUtilityWrapper(HttpContext.Current.Server); } }
	}
}