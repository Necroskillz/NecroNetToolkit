using System;
using System.Collections;
using System.IO;
using System.Reflection;
using System.Web;
using NUnit.Framework;

namespace NecroNet.Toolkit.Tests.UtilityTests
{
	[TestFixture]
	public class LocalDataTests
	{
		private const string Key = "Key";
		private static readonly object Obj = new object();

		private class FakeLocalData : ILocalData
		{
			public object this[object key]
			{
				get { throw new Exception(); }
				set { throw new Exception(); }
			}

			public int Count
			{
				get { throw new Exception(); }
			}

			public void Clear()
			{
				throw new Exception();
			}

			public bool Contains(object key)
			{
				throw new Exception();
			}
		}

		[Test]
		public void LocalData_ShouldStoreDataInLocalHashtableWhenNotRunningInWeb()
		{
			Local.Data[Key] = Obj;

			var fieldInfo = typeof (Local.LocalData).GetField("_localData", BindingFlags.Static | BindingFlags.NonPublic);
			var hashtable = fieldInfo.GetValue(Local.Data) as Hashtable;

			Assert.That(hashtable, Is.Not.Null);
			Assert.That(hashtable[Key], Is.EqualTo(Obj));
		}

		[Test]
		public void LocalData_ShouldStoreDataInHttpContextWhenRunninInWeb()
		{
			var httpRequest = new HttpRequest("", "http://mySomething/", "");
			var stringWriter = new StringWriter();
			var httpResponce = new HttpResponse(stringWriter);
			var httpContext = new HttpContext(httpRequest, httpResponce);

			HttpContext.Current = httpContext;

			Local.Data[Key] = Obj;

			var fieldInfo = typeof (Local.LocalData).GetField("LocalDataHashtableKey", BindingFlags.NonPublic | BindingFlags.Static);
			var hashtableKey = fieldInfo.GetValue(Local.Data);
			var hashtable = HttpContext.Current.Items[hashtableKey] as Hashtable;

			Assert.That(hashtable, Is.Not.Null);
			Assert.That(hashtable[Key], Is.EqualTo(Obj));
		}

		[Test]
		public void LocalData_Count_ShouldReturnAmountOfItemsStored()
		{
			Local.Data[Key] = Obj;
			Local.Data["Key2"] = new object();

			Assert.That(Local.Data, Has.Count.EqualTo(2));

			Local.Data["Key3"] = new object();

			Assert.That(Local.Data, Has.Count.EqualTo(3));
		}

		[Test]
		public void LocalData_Contains_ShouldReturnTrueIfHashtableContainsSpecifiedKey()
		{
			Local.Data[Key] = Obj;

			Assert.That(Local.Data.Contains(Key));
		}

		[Test]
		public void LocalData_Contains_ShouldReturnFalseIfHashtableDoesntContainSpecifiedKey()
		{
			Assert.That(Local.Data.Contains(Key), Is.False);
		}

		[Test]
		public void LocalData_Clear_ShouldRemoveAllDataFromTheHashtable()
		{
			Local.Data[Key] = Obj;
			Local.Data["Key2"] = new object();

			Local.Data.Clear();

			Assert.That(Local.Data, Has.Count.EqualTo(0));
		}

		[Test]
		public void LocalData_ChangeContext_ShouldChangeImplementationOfLocalData()
		{
			Local.ChangeContext(new FakeLocalData());

			Assert.That(() => Local.Data.Count, Throws.Exception);

			Local.ChangeContext(new Local.LocalData());
		}
	}
}
