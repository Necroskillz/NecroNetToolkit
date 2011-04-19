using System.Data.Objects;
using NecroNet.Toolkit.Data;

namespace NecroNet.Toolkit.Tests.Fakes
{
	public class FakeObjectContext : IObjectContext
	{
		private FakeObjectSet<Person> _people;

		public IObjectSet<Person> People
		{
			get { return _people ?? (_people = new FakeObjectSet<Person>(null)); }
		}

		public void Dispose()
		{
		}

		public int SaveChanges()
		{
			return 0;
		}
	}
}
