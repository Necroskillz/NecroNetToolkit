using System;
using System.Collections.Generic;
using System.Linq;
using PagedList;

namespace NecroNet.Toolkit
{
	public class SortedPagedList<T> : List<T>, ISortedPagedList<T>
	{
		public SortedPagedList(IQueryable<T> source, int pageNumber, int pageSize, string sortKey, string sortDirection)
		{
			// set source to blank list if source is null to prevent exceptions
			if(source == null)
				source = new List<T>().AsQueryable();

			// argument checking
			if(pageNumber < 1)
				throw new ArgumentOutOfRangeException("pageNumber");
			if(pageSize < 1)
				throw new ArgumentOutOfRangeException("pageSize");

			Guard.IsNotNullOrEmpty(sortKey, "sortKey", "Sort key cannot be null");

			if(sortDirection != Toolkit.SortDirection.Asc && sortDirection != Toolkit.SortDirection.Desc)
				throw new ArgumentException("Sort direction must be either SortDir.Asc or SortDir.Desc");

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
		/// Static sorted paged list constructor
		/// </summary>
		public SortedPagedList(IEnumerable<T> subset, int pageNumber, int pageSize, int totalItemCount, string sortKey, string sortDirection)
		{
			// set source to blank list if source is null to prevent exceptions
			if (subset == null)
				subset = new List<T>();

			// argument checking
			if (pageNumber < 1)
				throw new ArgumentOutOfRangeException("pageNumber");
			if (pageSize < 1)
				throw new ArgumentOutOfRangeException("pageSize");

			Guard.IsNotNullOrEmpty(sortKey, "sortKey", "Sort key cannot be null");

			if (sortDirection != Toolkit.SortDirection.Asc && sortDirection != Toolkit.SortDirection.Desc)
				throw new ArgumentException("Sort direction must be either SortDir.Asc or SortDir.Desc");

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