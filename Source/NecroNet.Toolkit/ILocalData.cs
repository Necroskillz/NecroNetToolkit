namespace NecroNet.Toolkit
{
	public interface ILocalData
	{
		object this[object key] { get; set; }
		int Count { get; }
		void Clear();
		bool Contains(object key);
	}
}