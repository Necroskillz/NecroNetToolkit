using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NecroNet.Toolkit.Tests.Fakes
{
	public class FakeLocalDataProvider : ILocalDataProvider
	{
		public object this[object key]
		{
			get { throw new Exception(); }
			set { throw new Exception(); }
		}

		public int Count
		{
			get { throw new Exception(); }
		}

		public void Clear()
		{
			throw new Exception();
		}

		public bool Contains(object key)
		{
			throw new Exception();
		}
	}
}
