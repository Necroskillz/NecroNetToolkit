namespace NecroNet.Toolkit.Http
{
	public interface IHttpGeneralDataStore : IHttpDataStore
	{
		/// <summary>
		/// Determines whether the data store contains a specific key.
		/// </summary>
		/// <param name="key">The key to locate.</param>
		bool Contains(object key);

		/// <summary>
		/// Retrieves strongly typed value associated with the specified key from the data store. If the value is not present, returns <c>default(T)</c>.
		/// </summary>
		/// <typeparam name="T">The type of the item to get.</typeparam>
		/// <param name="key">The key whose value to retrieve.</param>
		T Get<T>(object key);

		/// <summary>
		/// Retrieves a value associated with the specified key from the data store. If the value is not present, returns <c>null</c>.
		/// </summary>
		/// <param name="key">The key whose value to retrieve.</param>
		object Get(object key);

		/// <summary>
		/// Stores the specified value and associates it with the specified key.
		/// </summary>
		/// <param name="key">The key whose value to store.</param>
		/// <param name="value">The value to store.</param>
		void Set(object key, object value);
	}
}