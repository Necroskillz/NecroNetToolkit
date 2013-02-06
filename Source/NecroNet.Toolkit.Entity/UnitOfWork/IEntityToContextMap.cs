using System.Linq;
using System.Collections.Generic;
using System;

namespace NecroNet.Toolkit.Entity
{
	public interface IEntityToContextMap
	{
		Type ContextFor(Type entityType);
	}
}