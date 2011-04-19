namespace NecroNet.Toolkit.Data
{
	/// <summary>
	/// Defines a method for creating object context.
	/// </summary>
	public interface IObjectContextFactory
	{
		/// <summary>
		/// Creates a object context.
		/// </summary>
		IObjectContext CreateObjectContext();
	}
}