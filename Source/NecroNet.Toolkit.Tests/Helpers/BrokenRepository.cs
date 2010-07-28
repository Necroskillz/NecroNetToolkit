using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NecroNet.Toolkit.EntityFramework;
using NecroNet.Toolkit.Tests.Fakes;

namespace NecroNet.Toolkit.Tests.Helpers
{
	public class BrokenRepositoryNoSetNameAttribute : UltimateEntityRepositoryBase<FakeObjectContext, EntityWithId>
	{
		private FakeObjectContext _objectContext;

		protected override FakeObjectContext ObjectContext
		{
			get
			{
				return _objectContext ?? (_objectContext = new FakeObjectContext());
			}
		}
	}

	[EntitySetName("Xyz")]
	public class BrokenRepositoryInvalidEntitySetName : UltimateEntityRepositoryBase<FakeObjectContext, EntityWithId>
	{
		private FakeObjectContext _objectContext;

		protected override FakeObjectContext ObjectContext
		{
			get
			{
				return _objectContext ?? (_objectContext = new FakeObjectContext());
			}
		}
	}
}
