using System;
using System.Collections;
using System.Collections.Generic;
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

		[Test]
		public void SessionData_Count_ShouldBeZeroWhenFirstUsed()
		{
			Assert.That(Session.Data.Count, Is.EqualTo(0));
		}

		[Test]
		public void SessionData_Count_ShouldIncreaseAfterAddingItems()
		{
			Session.Data[Key] = 1;

			Assert.That(Session.Data.Count, Is.EqualTo(1));

			Session.Data[Key + "2"] = 2;

			Assert.That(Session.Data.Count, Is.EqualTo(2));
		}

		[Test]
		public void SessionData_SetItem_ShouldStoreValueInHttpContextSession()
		{
			Session.Data[Key] = 1;

			var fieldInfo = typeof(Session.SessionDataProvider).GetField("LocalDataHashtableKey", BindingFlags.NonPublic | BindingFlags.Static);
			var hashtableKey = (string)fieldInfo.GetValue(Local.Data);
			var hashtable = HttpContext.Current.Session[hashtableKey] as Hashtable;

			Assert.That((int)hashtable[Key], Is.EqualTo(1));
		}

		[Test]
		public void SessionData_GetItem_ShouldReturnStoredValue()
		{
			Session.Data[Key] = 1;

			var value = (int)Session.Data[Key];

			Assert.That(value, Is.EqualTo(1));
		}

		[Test]
		public void SessionData_GetItem_ShouldReturnDefaultValueForTIfNoValueWithSpecifiedKeyIsStored()
		{
			var value = Session.Data[Key];

			Assert.That(value, Is.Null);
		}

		[Test]
		public void SessionData_Clear_ShouldRemoveAllKeysFromSession()
		{
			Session.Data[Key] = 1;
			Session.Data.Clear();

			Assert.That(Session.Data.Count, Is.EqualTo(0));
		}

		[Test]
		public void LocalData_Contains_ShouldReturnTrueIfHashtableContainsSpecifiedKey()
		{
			Session.Data[Key] = Obj;

			Assert.That(Session.Data.Contains(Key));
		}

		[Test]
		public void SessionData_ChangeContext_ShouldChangeImplementationOfSessionData()
		{
			Session.ChangeContext(new FakeLocalDataProvider());

			Assert.That(() => Session.Data[Key] = 0, Throws.Exception);

			Session.ChangeContext(new Session.SessionDataProvider());
		}
	}
}
