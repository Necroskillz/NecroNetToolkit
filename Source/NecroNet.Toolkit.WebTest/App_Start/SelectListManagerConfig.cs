using System;
using System.Linq;
using System.Collections.Generic;
using NecroNet.Toolkit.Mvc;
using NecroNet.Toolkit.WebTest.Helpers;
using NecroNet.Toolkit.WebTest.Models;

namespace NecroNet.Toolkit.WebTest
{
	public class SelectListManagerConfig
	{
		public static void ConfigureSelectListManager()
		{
			SelectListManager.Initialize(c =>
			{
				c.SetDataProviderFactory(() => new SelectListDataProvider());

				c.Registry.Add<Test1>(i => i.Name, i => i.Id);
			});

			SelectListManager.Registry.Add<Test2>(i => i.Test1.Name, i => i.Id);
		}
	}
}