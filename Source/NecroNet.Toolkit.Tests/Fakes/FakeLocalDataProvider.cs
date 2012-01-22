using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NecroNet.Toolkit.Tests.Fakes
{
	public class FakeLocalDataProvider : IHttpGeneralDataProvider
	{
		public void Clear()
		{
			throw new Exception();
		}

		public bool Contains(object key)
		{
			throw new Exception();
		}

		public T Get<T>(object key)
		{
			throw new Exception();
		}

		public object Get(object key)
		{
			throw new Exception();
		}

		public void Set(object key, object value)
		{
			throw new Exception();
		}
	}
}
