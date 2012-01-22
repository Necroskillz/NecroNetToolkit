using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using NecroNet.Toolkit.Internals;
using NecroNet.Toolkit.Resources;
using PagedList;

namespace NecroNet.Toolkit
{
	public class SortedPagedList<T> : List<T>, ISortedPagedList<T>
	{
		/// <summary>
		/// Initializes a new instance of <see cref="SortedPagedList{T}"/>.
		/// </summary>
		/// <param name="source">The source query items from.</param>
		/// <param name="pageNumber">The number of current page.</param>
		/// <param name="pageSize">The number of items on one page.</param>
		/// <param name="sortKey">The name of property to sort by.</param>
		/// <param name="sortDirection">The direction to sort by. (Possible values are SortDirection.Asc and SortDirection.Desc).</param>
		public SortedPagedList(IQueryable<T> source, int pageNumber, int pageSize, string sortKey, string sortDirection)
		{
			if (pageNumber < 1)
			{
				Throw.NewArgumentOutOfRangeException(() => pageNumber, Res.ExceptionMessage_SortedPagedList_PageNumber);
			}

			if (pageSize < 1)
			{
				Throw.NewArgumentOutOfRangeException(() => pageSize, Res.ExceptionMessage_SortedPagedList_PageSize);
			}

			if(string.IsNullOrEmpty(sortKey))
			{
				Throw.NewArgumentException(() => sortKey, Res.ExceptionMessage_SortedPagedList_SortKey);
			}

			if (sortDirection != Toolkit.SortDirection.Asc && sortDirection != Toolkit.SortDirection.Desc)
			{
				Throw.NewArgumentOutOfRangeException(() => sortDirection, Res.ExceptionMessage_SortedPagedList_SortDirection);
			}

			// set source to blank list if source is null to prevent exceptions
			if(source == null)
				source = new List<T>().AsQueryable();

			// set properties
			var index = pageNumber - 1;
			TotalItemCount = source.Count();
			PageSize = pageSize;
			PageNumber = pageNumber;
			PageCount = TotalItemCount > 0 ? (int) Math.Ceiling(TotalItemCount / (double) PageSize) : 0;
			HasPreviousPage = (index > 0);
			HasNextPage = (index < (PageCount - 1));
			IsFirstPage = (index <= 0);
			IsLastPage = (index >= (PageCount - 1));
			SortDirection = sortDirection;
			SortKey = sortKey;

			if(TotalItemCount > 0)
			{
				AddRange(SortDirection == Toolkit.SortDirection.Asc
							? source.OrderBy(sortKey).Skip(index * pageSize).Take(pageSize)
							: source.OrderByDescending(sortKey).Skip(index * pageSize).Take(pageSize));
			}
		}


		/// <summary>
		/// Initializes a new instance of <see cref="SortedPagedList{T}"/> class with already divided subset and information about it's superset. This is usefull for mapping a <see cref="SortedPagedList{T}"/> to another <see cref="SortedPagedList{T}"/>.
		/// </summary>
		/// <param name="subset">A single subset this instance should represent.</param>
		/// <param name="pageNumber">The number of current page.</param>
		/// <param name="pageSize">The number of items on one page.</param>
		/// <param name="totalItemCount">The total number of items in the superset.</param>
		/// <param name="sortKey">The name of property to sort by.</param>
		/// <param name="sortDirection">The direction to sort by. (Possible values are SortDirection.Asc and SortDirection.Desc).</param>
		public SortedPagedList(IEnumerable<T> subset, int pageNumber, int pageSize, int totalItemCount, string sortKey, string sortDirection)
		{
			if(subset == null)
			{
				Throw.NewArgumentNullException(() => subset);
			}

			if (pageNumber < 1)
			{
				Throw.NewArgumentOutOfRangeException(() => pageNumber, Res.ExceptionMessage_SortedPagedList_PageNumber);
			}

			if (pageSize < 1)
			{
				Throw.NewArgumentOutOfRangeException(() => pageSize, Res.ExceptionMessage_SortedPagedList_PageSize);
			}

			if (string.IsNullOrEmpty(sortKey))
			{
				Throw.NewArgumentException(() => sortKey, Res.ExceptionMessage_SortedPagedList_SortKey);
			}

			if (sortDirection != Toolkit.SortDirection.Asc && sortDirection != Toolkit.SortDirection.Desc)
			{
				Throw.NewArgumentOutOfRangeException(() => sortDirection, Res.ExceptionMessage_SortedPagedList_SortDirection);
			}

			// set properties
			var index = pageNumber - 1;
			TotalItemCount = totalItemCount;
			PageSize = pageSize;
			PageNumber = pageNumber;
			PageCount = TotalItemCount > 0 ? (int)Math.Ceiling(TotalItemCount / (double)PageSize) : 0;
			HasPreviousPage = (index > 0);
			HasNextPage = (index < (PageCount - 1));
			IsFirstPage = (index <= 0);
			IsLastPage = (index >= (PageCount - 1));
			SortDirection = sortDirection;
			SortKey = sortKey;

			if (TotalItemCount > 0)
			{
				AddRange(SortDirection == Toolkit.SortDirection.Asc
							? subset.AsQueryable().OrderBy(sortKey).Skip(index * pageSize).Take(pageSize)
							: subset.AsQueryable().OrderByDescending(sortKey).Skip(index * pageSize).Take(pageSize));
			}
		}

		public int PageCount { get; private set; }
		public int TotalItemCount { get; private set; }

		public int PageNumber { get; private set; }

		public int PageSize { get; private set; }
		public bool HasPreviousPage { get; private set; }
		public bool HasNextPage { get; private set; }
		public bool IsFirstPage { get; private set; }
		public bool IsLastPage { get; private set; }

		public string SortDirection { get; private set; }
		public string SortKey { get; private set; }

		public int FirstItemOnPage
		{
			get
			{
				return (PageNumber - 1) * PageSize + 1;
			}
		}

		public int LastItemOnPage
		{
			get
			{
				int numberOfLastItemOnPage = FirstItemOnPage + PageSize - 1;
				if (numberOfLastItemOnPage > TotalItemCount)
				{
					numberOfLastItemOnPage = TotalItemCount;
				}
				return numberOfLastItemOnPage;
			}
		}
	}

	public static class Pagination
	{
		public static SortedPagedList<T> ToSortedPagedList<T>(this IQueryable<T> source, int pageNumber, int pageSize,
		                                                      string sortKey, string sortDirection)
		{
			return new SortedPagedList<T>(source, pageNumber, pageSize, sortKey, sortDirection);
		}
	}
}