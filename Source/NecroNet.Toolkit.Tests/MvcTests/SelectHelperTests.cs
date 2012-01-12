using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using NecroNet.Toolkit.Mvc;
using NecroNet.Toolkit.Tests.Helpers;

namespace NecroNet.Toolkit.Tests.MvcTests
{
	[TestFixture]
	public class SelectHelperTests
	{
		private List<Movie> _movies;

		[TestFixtureSetUp]
		public void FixtureSetUp()
		{
			_movies = MovieTestDataProvider.GetTestData();
		}

		[Test]
		public void ToSelectItemList_ShouldCreateSelectListItemsFromObjects()
		{
			var items = _movies.ToSelectItemList(m => m.Name, m => m.Id);

			Assert.That(items, Has.Count.EqualTo(5));

			for (int i = 0; i < 5; i++)
			{
				var movie = _movies[i];
				var item = items[i];

				Assert.That(item.Text, Is.EqualTo(movie.Name));
				Assert.That(item.Value, Is.EqualTo(movie.Id.ToString()));
			}
		}

		[Test]
		public void ToSelectItemList_WithNulllineParameter_ShouldAddEmptyLineWithDefaultTextToTheBeginingOfTheList()
		{
			var items = _movies.ToSelectItemList(m => m.Name, m => m.Id, nullLine: true);

			Assert.That(items, Has.Count.EqualTo(6));

			var first = items.First();

			Assert.That(first.Value, Is.Empty);
			Assert.That(first.Text, Is.EqualTo("--"));
		}

		[Test]
		public void ToSelectItemList_WithNulllineAndNullLineTextParameters_ShouldAddEmptyLineWithSpecifiedTextToTheBeginingOfTheList()
		{
			var items = _movies.ToSelectItemList(m => m.Name, m => m.Id, nullLine: true, nullLineText: "X");

			Assert.That(items, Has.Count.EqualTo(6));

			var first = items.First();

			Assert.That(first.Value, Is.Empty);
			Assert.That(first.Text, Is.EqualTo("X"));
		}

		[Test]
		public void ToSelectItemList_WithSelectedValueParameter_ShouldSetTheSpecifiedValueAsSelected()
		{
			var items = _movies.ToSelectItemList(m => m.Name, m => m.Id, 4);

			Assert.That(items, Has.Count.EqualTo(5));
			
			Assert.That(items.First(i => i.Value == "4").Selected);
		}

		[Test]
		public void ToSelectItemList_WithSelectedValueParameter_ShouldSetTheSpecifiedValueAsSelectedEvenIfValueHasDifferentType()
		{
			const short selected = 4;
			var items = _movies.ToSelectItemList(m => m.Name, m => m.Id, selected);

			Assert.That(items, Has.Count.EqualTo(5));

			Assert.That(items.First(i => i.Value == "4").Selected);
		}

		[Test]
		public void ToSelectItemList_WithNulllineAndSelectedValueParameters_ShouldAddEmptyLineWithDefaultTextToTheBeginingOfTheListAndSetTheSpecifiedValueAsSelected()
		{
			var items = _movies.ToSelectItemList(m => m.Name, m => m.Id, 4, nullLine: true);

			Assert.That(items, Has.Count.EqualTo(6));

			var first = items.First();

			Assert.That(first.Value, Is.Empty);
			Assert.That(first.Text, Is.EqualTo("--"));
			Assert.That(items.First(i => i.Value == "4").Selected);
		}

		[Test]
		public void ToSelectItemList_WithNulllineAndNullLineTextAndSelectedValueParameters_ShouldAddEmptyLineWithSpecifiedTextToTheBeginingOfTheListAndSetTheSpecifiedValueAsSelected()
		{
			var items = _movies.ToSelectItemList(m => m.Name, m => m.Id, 4, nullLine: true, nullLineText: "X");

			Assert.That(items, Has.Count.EqualTo(6));

			var first = items.First();

			Assert.That(first.Value, Is.Empty);
			Assert.That(first.Text, Is.EqualTo("X"));
			Assert.That(items.First(i => i.Value == "4").Selected);
		}
	}
}