using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NecroNet.Toolkit.EntityFramework;
using NecroNet.Toolkit.Tests.Fakes;

namespace NecroNet.Toolkit.Tests.Helpers
{
	public interface ITestPersonRepository: IRepository<Person>
	{
	}
}
