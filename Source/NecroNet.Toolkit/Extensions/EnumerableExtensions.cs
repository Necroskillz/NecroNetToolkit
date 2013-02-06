﻿using System;
using System.Collections.Generic;

namespace NecroNet.Toolkit.Core
{
	public static class EnumerableExtensions
	{
		public static void Each<T>(this IEnumerable<T> collection, Action<T> action)
		{
			foreach (var item in collection)
			{
				action(item);
			}
		}
	}
}