using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Web;
using System.Web.SessionState;
using Moq;
using NUnit.Framework;

namespace NecroNet.Toolkit.Tests.UtilityTests
{
	[TestFixture]
	public class SessionUtilityTest
	{
		public const string Key = "Key";

		private class FakeSessionUtility : ISessionUtility
		{
			public T GetValue<T>(string key)
			{
				throw new Exception();
			}

			public T GetValue<T>(string key, T defaultValue)
			{
				throw new Exception();
			}

			public void SetValue(string key, object value)
			{
				throw new Exception();
			}

			public void ClearValue(string key)
			{
				throw new Exception();
			}
		}

		[SetUp]
		public void SetUp()
		{
			var httpRequest = new HttpRequest("", "http://mySomething/", "");
			var stringWriter = new StringWriter();
			var httpResponce = new HttpResponse(stringWriter);
			var httpContext = new HttpContext(httpRequest, httpResponce);

			var sessionContainer = new HttpSessionStateContainer("id", new SessionStateItemCollection(),
																	new HttpStaticObjectsCollection(), 10, true,
																	HttpCookieMode.AutoDetect, SessionStateMode.InProc, false);
			httpContext.Items["AspSession"] =
				typeof (HttpSessionState).GetConstructor(BindingFlags.NonPublic | BindingFlags.Instance, null, CallingConventions.Standard,
															new[] {typeof (HttpSessionStateContainer)}, null).Invoke(new object[]{ sessionContainer});

			HttpContext.Current = httpContext;
		}

		[Test]
		public void SessionUtility_SetValue_ShouldStoreValueInHttpContextSession()
		{
			Session.Utility.SetValue(Key, 1);

			Assert.That(HttpContext.Current.Session[Key], Is.EqualTo(1));
		}

		[Test]
		public void SessionUtility_GetValue_ShouldReturnStoredValue()
		{
			Session.Utility.SetValue(Key, 1);

			var value = Session.Utility.GetValue<int>(Key);

			Assert.That(value, Is.EqualTo(1));
		}

		[Test]
		public void SessionUtility_GetValue_ShouldReturnDefaultValueForTIfNoValueWithSpecifiedKeyIsStored()
		{
			var value = Session.Utility.GetValue<object>(Key);

			Assert.That(value, Is.Null);
		}

		[Test]
		public void SessionUtility_GetValueWithDefaultValueParameter_ShouldReturnSpecifiedDefaultValueIfNoValueWithSpecifiedKeyIsStored()
		{
			var defaultValue = new object();
			var value = Session.Utility.GetValue(Key, defaultValue);

			Assert.That(value, Is.SameAs(defaultValue));
		}

		[Test]
		public void SessionUtility_ClearValue_ShouldRemoveStoredKeyFromSession()
		{
			Session.Utility.SetValue(Key, 1);
			Session.Utility.ClearValue(Key);

			var value = Session.Utility.GetValue<int>(Key);

			Assert.That(value, Is.EqualTo(0));
		}

		[Test]
		public void SessionUtility_ChangeContext_ShouldChangeImplementationOfSessionUtility()
		{
			Session.ChangeContext(new FakeSessionUtility());

			Assert.That(() => Session.Utility.SetValue(Key, 0), Throws.Exception);

			Session.ChangeContext(new Session.SessionUtility());
		}
	}
}
