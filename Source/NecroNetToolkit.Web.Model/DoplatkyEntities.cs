using NecroNet.Toolkit.Data;

namespace NecroNetToolkit.Web.Model
{
    public partial class DoplatkyEntities : IObjectContext
    {  
    }

    public class DoplatkyEntitiesFactory : IObjectContextFactory
    {
        public IObjectContext CreateObjectContext()
        {
            return new DoplatkyEntities();
        }
    }
}