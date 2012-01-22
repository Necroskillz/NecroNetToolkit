using System.Data.Common;
using NecroNet.Toolkit.Data;

namespace NecroNetToolkit.Web.Model
{
    public partial class AllDealsEntities : IObjectContext
    {
    	public DbConnection Connection
    	{
			get { return Database.Connection; }
    	}
    }

    public class AllDealsEntitiesFactory : IObjectContextFactory
    {
        public IObjectContext CreateObjectContext()
        {
            return new AllDealsEntities();
        }
    }
}