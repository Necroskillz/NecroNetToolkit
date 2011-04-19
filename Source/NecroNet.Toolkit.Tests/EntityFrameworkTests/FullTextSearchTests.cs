using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NecroNet.Toolkit.Tests.Helpers;
using NUnit.Framework;
using NecroNet.Toolkit.Data;

namespace NecroNet.Toolkit.Tests.EntityFrameworkTests
{
	[TestFixture]
	public class FullTextSearchTests
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
		public void FullTextSearch_ShouldSearchInAllStringPropertiesAndReturnMatches()
		{
			var search = _movieQuery.FullTextSearch("The Bourne Identity");

			Assert.That(search.Count(), Is.EqualTo(4));
			Assert.That(search.FirstOrDefault(m => m.Id == 5), Is.Null);
		}

		[Test]
		public void FullTextSearch_WithExactMatchOption_ShouldNotReturnPartialMatches()
		{
			var search = _movieQuery.FullTextSearch("The Bourne Identity", exactMatch: true);

			Assert.That(search.Count(), Is.EqualTo(1));
		}

		[Test]
		public void FullTextSearch_WithSpecifiedSearchProperties_ShouldOnlySearchInTheseProperties()
		{
			var search = _movieQuery.FullTextSearch("Jason Bourne", false, "Director");

			Assert.That(search.Count(), Is.EqualTo(1));
		}

		[Test]
		public void FullTextSearch_ShouldReturnWhenThereAreNoStringProperties()
		{
			var search = _movieQuery.FullTextSearch("Bourne", true, "XXX");

			Assert.That(search, Is.Null);
		}
	}
}
