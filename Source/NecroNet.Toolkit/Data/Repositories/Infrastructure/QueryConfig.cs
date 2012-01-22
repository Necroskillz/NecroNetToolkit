using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;

namespace NecroNet.Toolkit.Data
{
	public class QueryConfig
	{
		public IList<dynamic> Includes { get; private set; }

		public QueryConfig()
		{
			Includes = new List<dynamic>();
		}

		public void Reset()
		{
			Includes.Clear();
		}

		public IQueryable<TEntity> Configure<TEntity>(IQueryable<TEntity> query) where TEntity : class
		{
			query = Includes.Aggregate(query, (current, include) => DbExtensions.Include(current, include));

			Reset();

			return query;
		}
	}
}
