using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NecroNet.Toolkit.EntityFramework
{
	internal class QueryConfig
	{
		public IList<string> Includes { get; set; }

		public QueryConfig()
		{
			Includes = new List<string>();
		}

		public void Reset()
		{
			Includes.Clear();
		}
	}
}
