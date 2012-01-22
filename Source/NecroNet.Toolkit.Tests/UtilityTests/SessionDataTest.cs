using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Web;
using System.Web.SessionState;
using Moq;
using NecroNet.Toolkit.Tests.Fakes;
using NUnit.Framework;

namespace NecroNet.Toolkit.Tests.UtilityTests
{
	[TestFixture]
	public class SessionDataTest
	{
		public const string Key = "Key";
		private static readonly object Obj = new object();

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

		private static HybridDictionary GetSessionDictionary()
		{
			var fieldInfo = typeof(Session.SessionDataProvider).GetField("StoreKey",
																		  BindingFlags.NonPublic | BindingFlags.Static);
			var storeKey = (string)fieldInfo.GetValue(Local.Data);
			var store = HttpContext.Current.Session[storeKey] as HybridDictionary;
			return store;
		}

		[Test]
		public void SessionData_SetItem_ShouldStoreValueInHttpContextSession()
		{
			Session.Data.Set(Key, 1);

			var store = GetSessionDictionary();

			Assert.That((int)store[Key], Is.EqualTo(1));
		}

		[Test]
		public void SessionData_GetItem_ShouldReturnStoredValue()
		{
			Session.Data.Set(Key, 1);

			var value = Session.Data.Get<int>(Key);

			Assert.That(value, Is.EqualTo(1));
		}

		[Test]
		public void SessionData_GetItem_ShouldReturnDefaultValueForTIfNoValueWithSpecifiedKeyIsStored()
		{
			var value = Session.Data.Get(Key);

			Assert.That(value, Is.Null);
		}

		[Test]
		public void SessionData_Clear_ShouldRemoveAllKeysFromSession()
		{
			Session.Data.Set(Key, 1);
			Session.Data.Clear();

			Assert.That(GetSessionDictionary().Count, Is.EqualTo(0));
		}

		[Test]
		public void LocalData_Contains_ShouldReturnTrueIfHashtableContainsSpecifiedKey()
		{
			Session.Data.Set(Key, Obj);

			Assert.That(Session.Data.Contains(Key));
		}

		[Test]
		public void SessionData_ChangeContext_ShouldChangeImplementationOfSessionData()
		{
			Session.ChangeContext(new FakeLocalDataProvider());

			Assert.That(() => Session.Data.Set(Key, 0), Throws.Exception);

			Session.ChangeContext(new Session.SessionDataProvider());
		}

		[Test]
		public void SessionData_ShouldReturnTheSameValueForSameStringKeyThatAreDifferentObjects()
		{
			const string key1 = "KEY";
			const string key2 = "KEY";

			Session.Data.Set(key1, 1);

			Assert.That(Session.Data.Get<int>(key2), Is.EqualTo(1));
		}
	}
}
