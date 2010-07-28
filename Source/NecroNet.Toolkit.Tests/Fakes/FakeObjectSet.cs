using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Objects;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace NecroNet.Toolkit.Tests.Fakes
{
	class FakeObjectSet<TEntity> : IObjectSet<TEntity> where  TEntity : class
	{
		readonly HashSet<TEntity> _data;
		readonly IQueryable _query;

		public FakeObjectSet() : this(new List<TEntity>()) { }

		public FakeObjectSet(IEnumerable<TEntity> testData)
		{
			_data = new HashSet<TEntity>(testData);
			_query = _data.AsQueryable();
		}

		public void AddObject(TEntity item)
		{
			_data.Add(item);
		}

		public void DeleteObject(TEntity item)
		{
			_data.Remove(item);
		}

		public void Detach(TEntity item)
		{
			_data.Remove(item);
		}

		public void Attach(TEntity item)
		{
			_data.Add(item);
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return _data.GetEnumerator();
		}

		IEnumerator<TEntity> IEnumerable<TEntity>.GetEnumerator()
		{
			return _data.GetEnumerator();
		}

		Type IQueryable.ElementType
		{
			get { return _query.ElementType; }
		}

		Expression IQueryable.Expression
		{
			get { return _query.Expression; }
		}

		IQueryProvider IQueryable.Provider
		{
			get { return _query.Provider; }
		}
	}
}
