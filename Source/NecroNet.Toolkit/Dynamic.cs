using System;
using System.Linq;
using System.Linq.Expressions;

namespace NecroNet.Toolkit
{
	internal static class Dynamic
	{
		public static IOrderedQueryable<T> OrderBy<T>(this IQueryable<T> source, string property)
		{
			return ApplyOrder(source, property, "OrderBy");
		}

		public static IOrderedQueryable<T> OrderByDescending<T>(this IQueryable<T> source, string property)
		{
			return ApplyOrder(source, property, "OrderByDescending");
		}

		public static IOrderedQueryable<T> ThenBy<T>(this IOrderedQueryable<T> source, string property)
		{
			return ApplyOrder(source, property, "ThenBy");
		}

		public static IOrderedQueryable<T> ThenByDescending<T>(this IOrderedQueryable<T> source, string property)
		{
			return ApplyOrder(source, property, "ThenByDescending");
		}

		private static IOrderedQueryable<T> ApplyOrder<T>(IQueryable<T> source, string property, string methodName)
		{
			var props = property.Split('.');
			var type = typeof(T);
			var arg = Expression.Parameter(type, "x");
			Expression expr = arg;

			foreach(var pi in props.Select(prop => type.GetProperty(prop)))
			{
				expr = Expression.Property(expr, pi);
				type = pi.PropertyType;
			}

			var delegateType = typeof(Func<,>).MakeGenericType(typeof(T), type);
			var lambda = Expression.Lambda(delegateType, expr, arg);

			var result = typeof(Queryable).GetMethods().Single(
				method => method.Name == methodName
				          && method.IsGenericMethodDefinition
				          && method.GetGenericArguments().Length == 2
				          && method.GetParameters().Length == 2)
				.MakeGenericMethod(typeof(T), type)
				.Invoke(null, new object[] {source, lambda});

			return (IOrderedQueryable<T>) result;
		}
	}
}