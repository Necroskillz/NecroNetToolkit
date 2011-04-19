using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NecroNet.Toolkit.Data
{
	public class MemoryRepository<TEntity> : UltimateRepositoryBase<TEntity> where TEntity: class
	{
		private readonly List<TEntity> _data = new List<TEntity>();

		private IEntityOperator<TEntity> _operator;
		protected override IEntityOperator<TEntity> Operator
		{
			get
			{
				return _operator ?? (_operator = new MemoryEntityOperator<TEntity>(() => _data));
			}
		}

		public override void Clear()
		{
			_data.Clear();
		}
	}
}
