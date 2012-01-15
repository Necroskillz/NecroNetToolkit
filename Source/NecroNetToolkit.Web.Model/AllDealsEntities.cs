using NecroNet.Toolkit.Data;

namespace NecroNetToolkit.Web.Model
{
    public partial class AllDealsEntities : IObjectContext
    {
    }

    public class AllDealsEntitiesFactory : IObjectContextFactory
    {
        public IObjectContext CreateObjectContext()
        {
            return new AllDealsEntities();
        }
    }
}