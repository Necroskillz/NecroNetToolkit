using System.Collections.Generic;
using PagedList;

namespace NecroNet.Toolkit
{
	/// <summary>
	/// Represent a paged list, that is sorted.
	/// </summary>
	/// <typeparam name="T">Type of list items.</typeparam>
	public interface ISortedPagedList<out T> : IPagedList, IEnumerable<T>
	{
		string SortDirection { get; }
		string SortKey { get; }
	}
}