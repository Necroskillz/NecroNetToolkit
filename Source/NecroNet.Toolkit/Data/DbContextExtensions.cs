using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Objects;

namespace NecroNet.Toolkit.Data
{
	public static class DbContextExtensions
	{
		public static ObjectContext GetUnderlyingObjectContext(this DbContext context)
		{
			return ((IObjectContextAdapter) context).ObjectContext;
		}
	}
}