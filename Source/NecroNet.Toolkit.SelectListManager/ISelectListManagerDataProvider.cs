using System;
using System.Collections.Generic;
using System.Linq;

namespace NecroNet.Toolkit.Mvc
{
	public interface ISelectListManagerDataProvider
	{
		IEnumerable<T> GetData<T>() where T: class;
	}
}