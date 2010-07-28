using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
			var list = new SortedPagedList<Movie>(null, 0, 10, "Name", SortDirection.Asc);

			Assert.That(list, Is.Not.Null);
			Assert.That(list, Is.Empty);
		}

		[Test]
		public void SortedPagedList_ShouldThrowIfIndexIsLessThanZero()
		{
			Assert.That(() => new SortedPagedList<Movie>(_movieQuery, -1, 10, "Name", SortDirection.Asc), Throws.InstanceOf<ArgumentOutOfRangeException>());
		}

		[Test]
		public void SortedPagedList_ShouldThrowIfPageSizeIsLessThanOne()
		{
			Assert.That(() => new SortedPagedList<Movie>(_movieQuery, 0, 0, "Name", SortDirection.Asc), Throws.InstanceOf<ArgumentOutOfRangeException>());
		}

		[Test]
		public void SortedPagedList_ShouldThrowIfSortDirectionIsNotAscOrDesc()
		{
			Assert.That(() => new SortedPagedList<Movie>(_movieQuery, 0, 10, "Name", "XXX"), Throws.ArgumentException);
		}

		[Test]
		public void SortedPagedList_PageNumberShouldBeGreaterByOneThanIndex()
		{
			var list = new SortedPagedList<Movie>(_movieQuery, 0, 5, "Name", SortDirection.Asc);

			Assert.That(list.PageIndex + 1, Is.EqualTo(list.PageNumber));
		}
	}
}
