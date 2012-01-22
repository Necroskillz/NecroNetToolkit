// This code is courtesy of https://github.com/andrewdavey/postal

using System;
using System.Collections.Specialized;
using System.Web;

namespace NecroNet.Toolkit.Mail
{
	internal class EmailHttpRequest : HttpRequestBase
	{
		readonly Uri _url;
		readonly NameValueCollection _serverVariables = new NameValueCollection();
		readonly Lazy<HttpBrowserCapabilitiesBase> _browser = new Lazy<HttpBrowserCapabilitiesBase>(() => new HttpBrowserCapabilitiesWrapper(new HttpBrowserCapabilities()));

		public EmailHttpRequest(Uri url)
		{
			_url = url;
		}

		public override string ApplicationPath
		{
			get { return HttpRuntime.AppDomainAppVirtualPath; }
		}

		public override NameValueCollection ServerVariables
		{
			get { return _serverVariables; }
		}

		public override Uri Url
		{
			get { return _url; }
		}

		public override bool IsLocal
		{
			get
			{
				return !_url.IsAbsoluteUri;
			}
		}

		public override HttpBrowserCapabilitiesBase Browser
		{
			get
			{
				return _browser.Value;
			}
		}
	}
}