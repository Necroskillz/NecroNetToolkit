namespace NecroNet.Toolkit
{
	public interface IHttpGeneralDataProvider : IHttpDataProvider
	{
		bool Contains(object key);

		T Get<T>(object key);
		object Get(object key);

		void Set(object key, object value);
	}
}