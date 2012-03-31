using System;
using System.Collections.Generic;

namespace NecroNet.Toolkit
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

		public static void Each<T>(this IEnumerable<T> collection, Func<T, LoopContinuation> action)
		{
			foreach (var item in collection)
			{
				var continuation = action(item);

				if(continuation == LoopContinuation.Break)
				{
					break;
				}
			}
		}
	}
}