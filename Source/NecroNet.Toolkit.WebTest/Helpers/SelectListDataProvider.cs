using System;
using System.Data.Metadata.Edm;
using System.Linq;
using System.Collections.Generic;
using NecroNet.Toolkit.Mvc;
using NecroNet.Toolkit.WebTest.Models;

namespace NecroNet.Toolkit.WebTest.Helpers
{
	public class SelectListDataProvider : ISelectListManagerDataProvider
	{
		public IEnumerable<T> GetData<T>() where T : class
		{
			using (var context = new TestContext())
			{
				return context.Set<T>().ToList();
			}
		}
	}
}