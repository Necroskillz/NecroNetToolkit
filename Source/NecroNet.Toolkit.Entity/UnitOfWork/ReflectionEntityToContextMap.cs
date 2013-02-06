using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
using System.Collections.Generic;
using System;
using System.Reflection;

namespace NecroNet.Toolkit.Entity
{
	public class ReflectionEntityToContextMap : IEntityToContextMap
	{
		private static readonly Dictionary<Type, Type> EntityToContextMap = new Dictionary<Type, Type>(); 

		static ReflectionEntityToContextMap()
		{
			var dbContextBaseType = typeof(DbContext);
			var sw = new Stopwatch();
			sw.Start();
			var contextTypes = AppDomain.CurrentDomain.GetAssemblies().SelectMany(a => a.GetTypes().Where(dbContextBaseType.IsAssignableFrom));
			sw.Stop();
			Debug.Print(sw.ElapsedMilliseconds.ToString());

			foreach (var contextType in contextTypes)
			{
				var entityTypes = contextType
					.GetProperties(BindingFlags.Instance | BindingFlags.Public)
					.Where(p => p.PropertyType.IsGenericType && p.PropertyType.GetGenericTypeDefinition() == typeof(DbSet<>))
					.Select(p => p.PropertyType.GetGenericArguments().First());

				foreach (var entityType in entityTypes)
				{
					if (!EntityToContextMap.ContainsKey(entityType))
					{
						EntityToContextMap.Add(entityType, contextType);
					}
				}
			}
		}

		public Type ContextFor(Type entityType)
		{
			return EntityToContextMap.ContainsKey(entityType) ? EntityToContextMap[entityType] : null;
		}
	}
}