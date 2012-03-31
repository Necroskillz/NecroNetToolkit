using System.Linq;

namespace NecroNet.Toolkit.Data
{
	/// <summary>
	/// Acts like a provider of <see cref="IQueryable{T}"/> for select list manager.
	/// </summary>
	public interface ISelectListManagerQueryProvider
	{
		/// <summary>
		/// Gets an <see cref="IQueryable"/> of the specified entity type.
		/// </summary>
		/// <typeparam name="TEntity">The type of entitie to get query for.</typeparam>
		IQueryable<TEntity> GetQuery<TEntity>() where TEntity : class;
	}
}