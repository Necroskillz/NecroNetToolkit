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
		/// <summary>
		/// The name of the property so sort by.
		/// </summary>
		string SortKey { get; }

		/// <summary>
		/// The direction to sort by. One of constants from <see cref="SortDirection"/>.
		/// </summary>
		string SortDirection { get; }
	}
}