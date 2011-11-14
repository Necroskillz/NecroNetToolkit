using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NecroNet.Toolkit.Tests.Helpers;
using NUnit.Framework;
using NecroNet.Toolkit;

namespace NecroNet.Toolkit.Tests.UtilityTests
{
    //public class DynamicLinqTests
    //{
    //    private List<Movie> _movies;
    //    private IQueryable<Movie> _movieQuery;

    //    [SetUp]
    //    public void SetUp()
    //    {
    //        _movies = MovieTestDataProvider.GetTestData();
    //        _movieQuery = _movies.AsQueryable();
    //    }

    //    [Test]
    //    public void OrderBy_ShouldOrderCollectionBySpecifiedPropertyAscending()
    //    {
    //        var ordered = _movieQuery.OrderBy("Name");

    //        Assert.That(ordered, Is.Ordered.By("Name"));
    //    }

    //    [Test]
    //    public void OrderByDescending_ShouldOrderCollectionBySpecifiedPropertyDescending()
    //    {
    //        var ordered = _movieQuery.OrderByDescending("Name");

    //        Assert.That(ordered, Is.Ordered.By("Name").Descending);
    //    }

    //    [Test]
    //    public void OrderBy_ThenBy_ShouldOrderCollectionFirstByFirstPropertyThenBySecondPropertyAscending()
    //    {
    //        var ordered = _movieQuery.OrderBy("Director").ThenBy("Name");

    //        var order = new[] {1, 4, 5, 2, 3};

    //        Assert.That(ordered.Select(m => m.Id), Is.EquivalentTo(order));
    //    }

    //    [Test]
    //    public void OrderBy_ThenByDescending_ShouldOrderCollectionFirstByFirstPropertyAscendingThenBySecondPropertyDescending()
    //    {
    //        var ordered = _movieQuery.OrderBy("Director").ThenByDescending("Name");

    //        var order = new[] { 1, 4, 5, 3, 2 };

    //        Assert.That(ordered.Select(m => m.Id), Is.EquivalentTo(order));
    //    }
    //}
}
