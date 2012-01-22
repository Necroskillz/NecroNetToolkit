using System.Linq;

namespace NecroNet.Toolkit.Data
{
	/// <summary>
	/// Acts like a provider of <see cref="IQueryable{T}"/> for other parts of the toolkit.
	/// </summary>
	public interface IQueryFactory
	{
		/// <summary>
		/// Gets an <see cref="IQueryable"/> of the specified entity type.
		/// </summary>
		/// <typeparam name="TEntity">The type of entitie to get query for.</typeparam>
		IQueryable<TEntity> GetQuery<TEntity>() where TEntity : class;
	}
}