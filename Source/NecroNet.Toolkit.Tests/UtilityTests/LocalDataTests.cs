using System;
using System.Collections;
using System.Collections.Specialized;
using System.IO;
using System.Reflection;
using System.Web;
using NecroNet.Toolkit.Tests.Fakes;
using NUnit.Framework;

namespace NecroNet.Toolkit.Tests.UtilityTests
{
	[TestFixture]
	public class LocalDataTests
	{
		private const string Key = "Key";
		private static readonly object Obj = new object();

		private static HybridDictionary GetWebDictionary()
		{
			var fieldInfo = typeof(Local.LocalDataProvider).GetField("StoreKey",
																	  BindingFlags.NonPublic | BindingFlags.Static);
			var storeKey = fieldInfo.GetValue(Local.Data);
			var dictionary = HttpContext.Current.Items[storeKey] as HybridDictionary;
			return dictionary;
		}

		private static HybridDictionary GetLocalDictionary()
		{
			var fieldInfo = typeof(Local.LocalDataProvider).GetField("_localStore",
																	  BindingFlags.NonPublic | BindingFlags.Static);
			var dictionary = fieldInfo.GetValue(null) as HybridDictionary;
			return dictionary;
		}

		[Test]
		public void LocalData_ShouldStoreDataInLocalDictionaryWhenNotRunningInWeb()
		{
			Local.Data.Set(Key, Obj);

			var dictionary = GetLocalDictionary();

			Assert.That(dictionary, Is.Not.Null);
			Assert.That(dictionary[Key], Is.EqualTo(Obj));
		}

		[Test]
		public void LocalData_ShouldStoreDataInHttpContextWhenRunninInWeb()
		{
			var httpRequest = new HttpRequest("", "http://mySomething/", "");
			var stringWriter = new StringWriter();
			var httpResponce = new HttpResponse(stringWriter);
			var httpContext = new HttpContext(httpRequest, httpResponce);

			HttpContext.Current = httpContext;

			Local.Data.Set(Key, Obj);

			var dictionary = GetWebDictionary();

			Assert.That(dictionary, Is.Not.Null);
			Assert.That(dictionary[Key], Is.EqualTo(Obj));
		}

		[Test]
		public void LocalData_Count_ShouldReturnAmountOfItemsStored()
		{
			Local.Data.Set(Key, Obj);
			Local.Data.Set("Key2", new object());

			var dictionary = GetLocalDictionary();

			Assert.That(dictionary, Has.Count.EqualTo(2));

			Local.Data.Set("Key3", new object());

			Assert.That(dictionary, Has.Count.EqualTo(3));
		}

		[Test]
		public void LocalData_Contains_ShouldReturnTrueIfHashtableContainsSpecifiedKey()
		{
			Local.Data.Set(Key, Obj);

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
			Local.Data.Set(Key, Obj);
			Local.Data.Set("Key2", new object());

			Local.Data.Clear();

			Assert.That(GetLocalDictionary(), Has.Count.EqualTo(0));
		}

		[Test]
		public void LocalData_ChangeContext_ShouldChangeImplementationOfLocalData()
		{
			Local.ChangeContext(new FakeLocalDataProvider());

			Assert.That(() => Local.Data.Set(Key, 1), Throws.Exception);

			Local.ChangeContext(new Local.LocalDataProvider());
		}
	}
}
