using System.Linq;
using System.Collections.Generic;
using System;
using System.Web.Caching;

namespace NecroNet.Toolkit.Http
{
	public interface IHttpCacheDataStore : IHttpDataStore
	{
		/// <summary>
		/// Determines whether the data store contains a specific key.
		/// </summary>
		/// <param name="key">The key to locate.</param>
		bool Contains(string key);

		/// <summary>
		/// Retrieves strongly typed value associated with the specified key from the data store. If the value is not present, returns <c>default(T)</c>.
		/// </summary>
		/// <typeparam name="T">The type of the item to get.</typeparam>
		/// <param name="key">The key whose value to retrieve.</param>
		T Get<T>(string key);

		/// <summary>
		/// Retrieves a value associated with the specified key from the data store. If the value is not present, returns <c>null</c>.
		/// </summary>
		/// <param name="key">The key whose value to retrieve.</param>
		object Get(string key);

		/// <summary>
		/// Stores the specified value and associates it with the specified key. Allows to specify absolute or sliding expiration, cache priority and remove callback.
		/// </summary>
		/// <param name="key">The key whose value to store.</param>
		/// <param name="value">The value to store.</param>
		/// <param name="absoluteExpiration">The time at which the inserted object expires and is removed from the cache. To avoid possible issues with local time such as changes from standard time to daylight saving time, use <see cref="P:System.DateTime.UtcNow"/> rather than <see cref="P:System.DateTime.Now"/> for this parameter value. If you are using absolute expiration, the <paramref name="slidingExpiration"/> parameter must be <see cref="F:System.Web.Caching.Cache.NoSlidingExpiration"/>.</param>
		/// <param name="slidingExpiration">The interval between the time the inserted object was last accessed and the time at which that object expires. If this value is the equivalent of 20 minutes, the object will expire and be removed from the cache 20 minutes after it was last accessed. If you are using sliding expiration, the <paramref name="absoluteExpiration"/> parameter must be <see cref="F:System.Web.Caching.Cache.NoAbsoluteExpiration"/>.</param>
		/// <param name="cacheItemPriority">The cost of the object relative to other items stored in the cache, as expressed by the <see cref="T:System.Web.Caching.CacheItemPriority"/> enumeration. This value is used by the cache when it evicts objects; objects with a lower cost are removed from the cache before objects with a higher cost.</param>
		/// <param name="removedCallback">A delegate that, if provided, will be called when an object is removed from the cache. You can use this to notify applications when their objects are deleted from the cache.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="key"/> or <paramref name="value"/> parameter is null.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">You set the <paramref name="slidingExpiration"/> parameter to less than TimeSpan.Zero or the equivalent of more than one year.</exception>
		/// <exception cref="T:System.ArgumentException">The <paramref name="absoluteExpiration"/> and <paramref name="slidingExpiration"/> parameters are both set for the item you are trying to add to the Cache.</exception>
		void Set(string key, object value, DateTime? absoluteExpiration = null, TimeSpan? slidingExpiration = null,
		         CacheItemPriority cacheItemPriority = CacheItemPriority.Default, CacheItemRemovedCallback removedCallback = null);
	}
}