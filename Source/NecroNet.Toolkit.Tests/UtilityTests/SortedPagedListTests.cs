using System;
using System.Collections.Generic;
using System.Linq;
using NecroNet.Toolkit.Tests.Helpers;
using NUnit.Framework;

namespace NecroNet.Toolkit.Tests.UtilityTests
{
	[TestFixture]
	public class SortedPagedListTests
	{
		private List<Movie> _movies;
		private IQueryable<Movie> _movieQuery;

		[TestFixtureSetUp]
		public void FixtureSetUp()
		{
			_movies = MovieTestDataProvider.GetTestData();
			_movieQuery = _movies.AsQueryable();
		}

		[Test]
		public void SortedPagedList_ShouldCreateEmptyListIfSourceQueryIsNull()
		{
			var list = new SortedPagedList<Movie>(null, 1, 10, "Name", SortDirection.Asc);

			Assert.That(list, Is.Not.Null);
			Assert.That(list, Is.Empty);
		}

		[Test]
		public void SortedPagedList_ShouldThrowIfPageNumberIsLessThanOne()
		{
			Assert.That(() => new SortedPagedList<Movie>(_movieQuery, 0, 10, "Name", SortDirection.Asc), Throws.InstanceOf<ArgumentOutOfRangeException>());
		}

		[Test]
		public void SortedPagedList_ShouldThrowIfPageSizeIsLessThanOne()
		{
			Assert.That(() => new SortedPagedList<Movie>(_movieQuery, 1, 0, "Name", SortDirection.Asc), Throws.InstanceOf<ArgumentOutOfRangeException>());
		}

		[Test]
		public void SortedPagedList_ShouldThrowIfSortDirectionIsNotAscOrDesc()
		{
			Assert.That(() => new SortedPagedList<Movie>(_movieQuery, 1, 10, "Name", "XXX"), Throws.InstanceOf<ArgumentOutOfRangeException>());
		}

		[Test]
		public void SortedPagedList_ShouldThrowIfSortKeyIsNullOrEmpty()
		{
			Assert.That(() => new SortedPagedList<Movie>(_movieQuery, 1, 10, null, SortDirection.Asc), Throws.InstanceOf<ArgumentException>());
			Assert.That(() => new SortedPagedList<Movie>(_movieQuery, 1, 10, string.Empty, SortDirection.Asc), Throws.InstanceOf<ArgumentException>());
		}

		[Test]
		public void SortedPagedList_FirstItemOnPage_ShouldReturnOneBaseIndexOfFirstItem()
		{
			var list = new SortedPagedList<Movie>(_movieQuery, 2, 3, "Name", SortDirection.Asc);

			Assert.That(list.FirstItemOnPage, Is.EqualTo(4));
		}

		[Test]
		public void SortedPagedList_LastItemOnPage_ShouldReturnOneBaseIndexOfLastItem()
		{
			var list = new SortedPagedList<Movie>(_movieQuery, 2, 3, "Name", SortDirection.Asc);

			Assert.That(list.LastItemOnPage, Is.EqualTo(5));
		}
	}
}
