using System.Linq;

namespace NecroNet.Toolkit.Data
{
	public interface IQueryFactory
	{
		IQueryable<TEntity> GetQuery<TEntity>() where TEntity : class;
	}
}