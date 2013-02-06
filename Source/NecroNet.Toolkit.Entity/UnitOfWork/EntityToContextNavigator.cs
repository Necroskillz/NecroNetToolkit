using System.Linq;
using System.Collections.Generic;
using System;
using NecroNet.Toolkit.Core.Resources;

namespace NecroNet.Toolkit.Entity
{
	public class EntityToContextNavigator
	{
		private static EntityToContextNavigator _current;
		public static EntityToContextNavigator Current
		{
			get
			{
				return _current ?? (_current = new EntityToContextNavigator());
			}
		}

		private IEntityToContextMap _map;

		public IEntityToContextMap Map
		{
			get
			{
				return _map ?? new ReflectionEntityToContextMap();
			}
		}

		public void SetMap(IEntityToContextMap map)
		{
			if (map == null)
			{
				Throw.NewArgumentNullException(() => map);
			}

			_map = map;
		}


		public void SetMap(Type mapType)
		{
			if (mapType == null)
			{
				Throw.NewArgumentNullException(() => mapType);
			}

			if (!typeof(IEntityToContextMap).IsAssignableFrom(mapType))
			{
				Throw.NewArgumentException(() => mapType, Res.EntityToContextNavigator_TypeIsNotIEntityToContextMap);
			}

			_map = (IEntityToContextMap)Activator.CreateInstance(mapType);
		}
	}
}