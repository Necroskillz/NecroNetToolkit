using NecroNet.Toolkit.Data;
using NecroNet.Toolkit.Tests.Fakes;

namespace NecroNet.Toolkit.Tests.Helpers
{
	class FakeObjectContextFactory : IObjectContextFactory
	{
		public IObjectContext CreateObjectContext()
		{
			return new FakeObjectContext();
		}
	}
}
