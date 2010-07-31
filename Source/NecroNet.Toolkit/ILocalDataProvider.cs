namespace NecroNet.Toolkit
{
	public interface ILocalDataProvider
	{
		object this[object key] { get; set; }
		int Count { get; }
		void Clear();
		bool Contains(object key);
	}
}