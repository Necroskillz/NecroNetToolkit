﻿using System;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace NecroNet.Toolkit.EntityFramework
{
	public static class ObjectContextExtensions
	{
		/// <summary>
		/// Searches in all string properties for the specifed search key.
		/// It is also able to search for several words. If the searchKey is for example 'John Travolta' then
		/// with exactMatch set to false all records which contain either 'John' or 'Travolta' in some string property
		/// are returned.
		/// </summary>
		/// <param name="query">Base query for the search.</param>
		/// <param name="searchKey">Search term.</param>
		/// <param name="exactMatch">Specifies if only the whole word or every single word should be searched.</param>
		/// <param name="searchProperties">Properties to check for matches.</param>
		/// <returns>Query that matches specified search parameters.</returns>
		public static IQueryable<T> FullTextSearch<T>(this IQueryable<T> query, string searchKey, bool exactMatch = false, params string[] searchProperties)
		{
			var parameter = Expression.Parameter(typeof(T), "c");

			var containsMethod = typeof(string).GetMethod("Contains", new[] { typeof(string) });

			var publicProperties = searchProperties.Length == 0
									? typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly)
										.Where(p => p.PropertyType == typeof(string))
									: typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly)
										.Where(p => p.PropertyType == typeof(string) && searchProperties.Contains(p.Name));

			Expression orExpressions = null;

			var searchKeyParts = exactMatch ? new[] { searchKey } : searchKey.Split(' ');

			foreach(var property in publicProperties)
			{
				Expression nameProperty = Expression.Property(parameter, property);
				foreach(var searchKeyPart in searchKeyParts)
				{
					Expression searchKeyExpression = Expression.Constant(searchKeyPart);
					Expression callContainsMethod = Expression.Call(nameProperty, containsMethod, searchKeyExpression);

					orExpressions = orExpressions == null ? callContainsMethod : Expression.Or(orExpressions, callContainsMethod);
				}
			}

			if(orExpressions == null) return null;

			var whereCallExpression = Expression.Call(
				typeof(Queryable),
				"Where",
				new[] { query.ElementType },
				query.Expression,
				Expression.Lambda<Func<T, bool>>(orExpressions, new[] { parameter }));

			return query.Provider.CreateQuery<T>(whereCallExpression);
		}
	}
}
