using System.IO;
using System.Web;
using System.Web.Caching;
using NUnit.Framework;
using NecroNet.Toolkit.Http;
using NecroNet.Toolkit.Tests.Fakes;

namespace NecroNet.Toolkit.Tests.UtilityTests
{
	[TestFixture]
	public class CachedDataTests
	{
		private const string Key = "Key";
		private static readonly object Obj = new object();

		public Cache HttpCache { get { return HttpContext.Current.Cache; }}

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
		public void CachedData_ShouldStoreDataInHttpCache()
		{
			Cached.Data.Set(Key, Obj);

			Assert.That(HttpCache, Is.Not.Null);
			Assert.That(HttpCache[Key], Is.EqualTo(Obj));
		}

		[Test]
		public void CachedData_Count_ShouldReturnAmountOfItemsStored()
		{
			Cached.Data.Set(Key, Obj);
			Cached.Data.Set("Key2", new object());

			Assert.That(HttpCache, Has.Count.EqualTo(2));

			Cached.Data.Set("Key3", new object());

			Assert.That(HttpCache, Has.Count.EqualTo(3));
		}

		[Test]
		public void CachedData_Contains_ShouldReturnTrueIfHttpCacheContainsSpecifiedKey()
		{
			Cached.Data.Set(Key, Obj);

			Assert.That(Cached.Data.Contains(Key));
		}

		[Test]
		public void CachedData_Contains_ShouldReturnFalseIfHttpCacheDoesntContainSpecifiedKey()
		{
			Assert.That(Cached.Data.Contains(Key), Is.False);
		}

		[Test]
		public void CachedData_Get_ShouldReturnStoredValue()
		{
			Cached.Data.Set(Key, Obj);

			Assert.That(Cached.Data.Get<object>(Key), Is.EqualTo(Obj));
			Assert.That(Cached.Data.Get(Key), Is.EqualTo(Obj));
		}

		[Test]
		public void CachedData_Clear_ShouldRemoveAllDataFromHttpCache()
		{
			Cached.Data.Set(Key, Obj);
			Cached.Data.Set("Key2", new object());

			Cached.Data.Clear();

			Assert.That(HttpCache, Has.Count.EqualTo(0));
		}

		[Test]
		public void CachedData_ChangeDataStore_ShouldChangeImplementationOfCachedData()
		{
			Cached.ChangeDataStore(new FakeCacheStore());

			Assert.That(() => Cached.Data.Set(Key, 1), Throws.Exception);

			Cached.ResetDataStore();
		}
	}
}