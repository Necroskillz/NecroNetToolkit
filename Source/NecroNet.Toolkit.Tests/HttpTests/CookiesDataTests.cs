using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using NUnit.Framework;
using NecroNet.Toolkit.Http;
using NecroNet.Toolkit.Tests.Fakes;

namespace NecroNet.Toolkit.Tests.UtilityTests
{
	[TestFixture]
	public class CookiesDataTests
	{
		private const string Key = "Key";
		private const string Value = "Val";
		private static readonly DateTime Expires = DateTime.Now.AddDays(1);

		public HttpCookieCollection OutgoingCookies { get { return HttpContext.Current.Response.Cookies; } }
		public HttpCookieCollection IncommingCookies { get { return HttpContext.Current.Request.Cookies; } }

		[SetUp]
		public void Setup()
		{
			var httpRequest = new HttpRequest("", "http://mySomething/", "");
			var stringWriter = new StringWriter();
			var httpResponce = new HttpResponse(stringWriter);
			var httpContext = new HttpContext(httpRequest, httpResponce);

			HttpContext.Current = httpContext;
		}

		[TestFixtureTearDown]
		public void TestTearDown()
		{
			HttpContext.Current = null;
		}

		[Test]
		public void CookiesData_Count_ShouldReturnAmountOfItemsStored()
		{
			Cookies.Data.Set(Key, Value);
			Cookies.Data.Set("Key2", "val2");


			Assert.That(OutgoingCookies, Has.Count.EqualTo(2));

			Cookies.Data.Set("Key3", "val3");

			Assert.That(OutgoingCookies, Has.Count.EqualTo(3));
		}

		[Test]
		public void CookiesData_Set_ShouldCorrectlySetExpiryOnCookie()
		{
			Cookies.Data.Set(Key, Value, Expires);

			var cookie = OutgoingCookies.Get(Key);

			Assert.That(cookie.Expires, Is.EqualTo(Expires));
		}

		[Test]
		public void CookiesData_Set_ShouldCorrectlySetDefaultExpiryOnCookie_365Days()
		{
			Cookies.Data.Set(Key, Value);

			var cookie = OutgoingCookies.Get(Key);

			Assert.That(cookie.Expires, Is.InRange(DateTime.Now.AddDays(364), DateTime.Now.AddDays(365)));
		}

		[Test]
		public void CookiesData_SetWithCookieParameter_ShouldStoreCookie()
		{
			var cookie = new HttpCookie(Key)
			             	{
			             		Value = Value
			             	};

			Cookies.Data.SetCookie(cookie);

			Assert.That(OutgoingCookies.Get(Key).Value, Is.EqualTo(Value));
		}

		[Test]
		public void CookiesData_Contains_ShouldReturnTrueIfRequestCookieCollectionContainsSpecifiedKey()
		{
			IncommingCookies.Set(new HttpCookie(Key){Value = Value});

			Assert.That(Cookies.Data.Contains(Key));
		}

		[Test]
		public void CookiesData_Contains_ShouldReturnFalseIfRequestCookieCollectionDoesntContainSpecifiedKey()
		{
			Assert.That(Cookies.Data.Contains(Key), Is.False);
		}

		[Test]
		public void CookiesData_Get_And_GetWithCookieParameter_ShouldReturnValueFromRequestCookies()
		{
			IncommingCookies.Set(new HttpCookie(Key){Value = Value});

			Assert.That(Cookies.Data.Get(Key), Is.EqualTo(Value));
			Assert.That(Cookies.Data.GetCookie(Key).Value, Is.EqualTo(Value));
		}

		[Test]
		public void CookiesData_Clear_ShouldRemoveAllDataFromTheOutgoingCookieCollection()
		{
			Cookies.Data.Set(Key, Value);
			Cookies.Data.Set("Key2", "val2");

			Cookies.Data.Clear();

			Assert.That(OutgoingCookies, Has.Count.EqualTo(0));
		}

		[Test]
		public void CookiesData_ChangeDataStore_ShouldChangeImplementationOfCookiesData()
		{	
			Cookies.ChangeDataStore(new FakeCookieStore());

			Assert.That(() => Cookies.Data.Set(Key, Value), Throws.Exception);

			Cookies.ResetDataStore();
		}
	}
}
