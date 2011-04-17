﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace NecroNet.Toolkit
{
	public class SortedPagedList<T> : List<T>, ISortedPagedList<T>
	{
		public SortedPagedList(IQueryable<T> source, int index, int pageSize, string sortKey, string sortDirection)
		{
			// set source to blank list if source is null to prevent exceptions
			if(source == null)
				source = new List<T>().AsQueryable();

			// argument checking
			if(index < 0)
				throw new ArgumentOutOfRangeException("index");
			if(pageSize < 1)
				throw new ArgumentOutOfRangeException("pageSize");

			Guard.IsNotNullOrEmpty(sortKey, "sortKey", "Sort key cannot be null");

			if(sortDirection != Toolkit.SortDirection.Asc && sortDirection != Toolkit.SortDirection.Desc)
				throw new ArgumentException("Sort direction must be either SortDir.Asc or SortDir.Desc");

			// set properties
			TotalItemCount = source.Count();
			PageSize = pageSize;
			PageIndex = index;
			PageCount = TotalItemCount > 0 ? (int) Math.Ceiling(TotalItemCount / (double) PageSize) : 0;
			HasPreviousPage = (PageIndex > 0);
			HasNextPage = (PageIndex < (PageCount - 1));
			IsFirstPage = (PageIndex <= 0);
			IsLastPage = (PageIndex >= (PageCount - 1));
			SortDirection = sortDirection;
			SortKey = sortKey;

			if(TotalItemCount > 0)
			{
				AddRange(SortDirection == Toolkit.SortDirection.Asc
				         	? source.OrderBy(sortKey).Skip(index * pageSize).Take(pageSize)
				         	: source.OrderByDescending(sortKey).Skip(index * pageSize).Take(pageSize));
			}
		}

		public int PageCount { get; private set; }
		public int TotalItemCount { get; private set; }
		public int PageIndex { get; private set; }

		public int PageNumber
		{
			get { return PageIndex + 1; }
		}

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
				return PageIndex * PageSize + 1;
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
		public static SortedPagedList<T> ToSortedPagedList<T>(this IQueryable<T> source, int index, int pageSize,
		                                                      string sortKey, string sortDirection)
		{
			return new SortedPagedList<T>(source, index, pageSize, sortKey, sortDirection);
		}
	}
}